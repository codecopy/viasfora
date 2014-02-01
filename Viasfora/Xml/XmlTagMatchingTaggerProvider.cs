﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Winterdom.Viasfora.Xml {
  [Export(typeof(IViewTaggerProvider))]
  [ContentType(Constants.CT_XML)]
  [TagType(typeof(TextMarkerTag))]
  public class XmlTagMatchingTaggerProvider : IViewTaggerProvider {
    [Import]
    internal IBufferTagAggregatorFactoryService Aggregator = null;

    public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag {
      if ( textView == null ) return null;
      if ( textView.TextBuffer != buffer ) return null;
      return new XmlTagMatchingTagger(
          textView, buffer,
          Aggregator.CreateTagAggregator<IClassificationTag>(buffer)
        ) as ITagger<T>;
    }
  }
}