using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NotesApp.Model;

namespace NotesApp.ViewModel.Commands
{
    internal class NewNoteCommand : ICommand
    {
        public NotesViewModel NotesViewModel { get; set; }

        public NewNoteCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }
        public bool CanExecute(object? parameter)
        {
            var selectedNotebook = parameter as Notebook;

            return selectedNotebook is not null;
        }

        public void Execute(object? parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;
            NotesViewModel.CreateNote(selectedNotebook.Id);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value;}
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
