using System;
using System.Collections.Generic;
using ch.ph.FileSystem;

namespace ch.ph.FileManager
{
    public sealed class Clipboard
    {
        private IElement[] _data;
        private bool       _cut;

        public Clipboard()
        {
        }

        public void SetContent(List<IElement> data, bool cut)
        {
            _data = data != null ? data.ToArray() : null;
            _cut  = cut;

            ClipboardContentChanged(null, null);
        }

        public int GetContentCount()
        {
            return _data == null ? 0 : _data.Length;
        }

        public List<IElement> GetContent() 
        {
            return new List<IElement>(_data);
        }

        public bool GetContentCut()
        {
            return _cut;
        }

        public event EventHandler ClipboardContentChanged;
    }
}