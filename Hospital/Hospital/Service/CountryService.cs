﻿using System.Collections.Generic;
using System.Linq;
using Hospital.Model;
using Hospital.Repository;
using Hospital.Repository.CountryRepo;

namespace Hospital.Service
{
    public class CountryService
    {
        private int _id;
        private readonly ICountryRepository _repository;

        public CountryService(ICountryRepository countryRepository)
        {
            _repository = countryRepository;
            List<Country> countries = Read();
            if (countries.Count == 0)
            {
                _id = 0;
            }
            else
            {
                _id = countries.Last().Id;
            }
        }

        public Country ReadById(int id)
        {
            return _repository.ReadById(id);
        }

        public void Create(Country newCountry)
        {
            newCountry.Id = GenerateId();
            _repository.Create(newCountry);
        }

        public void Edit(Country editCountry)
        {
            _repository.Edit(editCountry);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<Country> Read()
        {
            return _repository.Read();
        }

        private int GenerateId()
        {
            return ++_id;
        }
    }
}
