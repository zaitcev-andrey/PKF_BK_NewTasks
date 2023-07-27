﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerWpf.Model;

namespace ServerWpf.ViewModels
{
    internal class AllTestsViewModel
    {
        private AllTestsModel _model;
        
        public AllTestsViewModel()
        {
            _model = new AllTestsModel();
        }

        public AllTestsModel Model { get { return _model; } }

        public string GetTestByIndex(object selectedItem)
        {
            return Model.GetTestData(selectedItem);
        }
    }
}
