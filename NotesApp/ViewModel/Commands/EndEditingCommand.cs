using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NotesApp.Model;

namespace NotesApp.ViewModel.Commands
{
    public class EndEditingCommand : ICommand
    {
        private NotesViewModel NotesViewModel { get; set; }

        public EndEditingCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Notebook notebook = parameter as Notebook;
            if (notebook != null)
            {
                NotesViewModel.StopEditing(notebook);
            }
        }

        public event EventHandler? CanExecuteChanged;
    }
}
