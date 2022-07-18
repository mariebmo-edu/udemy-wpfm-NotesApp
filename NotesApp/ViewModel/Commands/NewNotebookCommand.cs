using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    internal class NewNotebookCommand : ICommand
    {
        public NotesViewModel NotesViewModel { get; set; }

        public NewNotebookCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            NotesViewModel.CreateNotebook();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
