﻿using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winterdom.Viasfora.Text {
  public class PresentationMode {
    private IWpfTextView theView;
    public PresentationMode(IWpfTextView textView) {
      this.theView = textView;
      VsfPackage.PresentationModeChanged += OnPresentationModeChanged;
      textView.Closed += OnTextViewClosed;
      textView.ViewportWidthChanged += OnViewportWidthChanged;
    }

    private void OnPresentationModeChanged(object sender, EventArgs e) {
      if ( theView != null ) {
        SetZoomLevel(theView);
      }
    }

    void OnViewportWidthChanged(object sender, EventArgs e) {
      SetZoomLevel(theView);
      theView.ViewportWidthChanged -= OnViewportWidthChanged;
    }

    void OnTextViewClosed(object sender, EventArgs e) {
      if ( theView != null ) {
        VsfPackage.PresentationModeChanged -= OnPresentationModeChanged;
        theView.Closed -= OnTextViewClosed;
        theView.ViewportWidthChanged -= OnViewportWidthChanged;
        theView = null;
      }
    }

    private void SetZoomLevel(IWpfTextView textView) {
      int zoomLevel = VsfPackage.GetPresentationModeZoomLevel();
      textView.ZoomLevel = zoomLevel;
    }
  }
}
