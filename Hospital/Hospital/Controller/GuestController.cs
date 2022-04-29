﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Model;
using Hospital.Service;

namespace Hospital.Controller
{
    public class GuestController
    {
        private readonly GuestService _service;

        public GuestController(GuestService service)
        {
            _service = service;
        }

        public Guest ReadById(int id)
        {
            return _service.ReadById(id);
        }

        public void Create(Guest newGuest)
        {
            _service.Create(newGuest);
        }

        public void Edit(Guest editGuest)
        {
            _service.Edit(editGuest);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }

        public ObservableCollection<Guest> ReadAll()
        {
            return _service.ReadAll();
        }
    }
}
