﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;

namespace Business.Services
{
    public class FileService : IFileService
    {
        private readonly string _directoryPath;
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public FileService(string directoryPath = "Data", string fileName = "list.json")
        {
            _directoryPath = directoryPath;
            _filePath = Path.Combine(_directoryPath, fileName);
            _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
        }

        public void SaveListToFile(List<ContactModel> list)
        {
            try
            {
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }

                var json = JsonSerializer.Serialize(list, _jsonSerializerOptions);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public List<ContactModel> GetListFromFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return [];
                }
                var json = File.ReadAllText(_filePath);
                var list = JsonSerializer.Deserialize<List<ContactModel>>(
                    json,
                    _jsonSerializerOptions
                );
                return list ?? [];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return [];
            }
        }
    }
}
