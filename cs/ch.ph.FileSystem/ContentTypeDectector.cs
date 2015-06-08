using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace ch.ph.FileSystem
{
    public sealed class ContentTypeDectector
    {
        // ***

        private readonly IDictionary<string, string> _map;

        // ***

        private ContentTypeDectector()
        {
            _map = new Dictionary<string, string>();
        }

        public string GetContentTypeForFile(string name)
        {
            lock(this) {
                string contenttype = null;
                int idxdot = (name != null) ? name.LastIndexOf('.') : -1;
                if(idxdot != -1) {
                    string fileext = name.Substring(idxdot).ToLower();
                    
                    if(!_map.TryGetValue(fileext, out contenttype)) {
                        string clsname = null;
                        string clsdesc = null;

                        if(contenttype == null) {
                            using(RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(fileext)) {
                                if(regkey != null) {
                                    clsname      = regkey.GetValue(null)           as string;    
                                    contenttype  = regkey.GetValue("Content Type") as string;
                                }
                            }
                        }

                        if(contenttype == null && clsname != null) {
                            using(RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(clsname)) {
                                if(regkey != null) {
                                    clsdesc     = regkey.GetValue(null)           as string;    
                                    contenttype = regkey.GetValue("Content Type") as string;
                                }
                            }
                        }

                        if(contenttype != null) {
                            contenttype = contenttype.ToLower();
                        }
                        
                        _map.Add(fileext, contenttype);
                    }
                }
                return contenttype;
            }
        }

        // ***

        private static ContentTypeDectector _instance;

        public static ContentTypeDectector Instance {
            get {
                lock(typeof(ContentTypeDectector)) {
                    if(_instance == null) {
                        _instance = new ContentTypeDectector();
                    }
                    return _instance;
                }
            }
        }
    }
}
