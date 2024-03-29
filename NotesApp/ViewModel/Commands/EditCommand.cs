﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class EditCommand : ICommand
    {
        public NotesViewModel ViewModel { get; set; }

        public EditCommand(NotesViewModel notesViewModel)
        {
            ViewModel = notesViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ViewModel.StartEditing();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
