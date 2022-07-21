using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NotesApp.Annotations;
using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using NotesApp.ViewModel.Helpers;

namespace NotesApp.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }


        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public EndEditingCommand EndEditingCommand { get; set; }

        private Notebook selectedNotebook;
        private Note selectedNote;
        public Visibility editNotebookNameVisibility { get; set; }

        public event EventHandler SelectedNoteChanged;
        public event PropertyChangedEventHandler? PropertyChanged;




        public NotesViewModel()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            EditNotebookNameVisibility = Visibility.Collapsed;

            GetNotebooks();
        }

        public void CreateNote(int notebookId)
        {
            Note newNote = new()
            {
                NotebookId = notebookId,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Title = "New Note"
            };

            DatabaseHelper.Insert(newNote);
            GetNotes();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new()
            {
                Name = "New Notebook"
            };

            DatabaseHelper.Insert(newNotebook);
            GetNotebooks();
        }

        public Notebook SelectedNotebook
        {
            get => selectedNotebook;
            set
            {
                selectedNotebook = value;
                OnPropertyChanged(nameof(SelectedNotebook));
                GetNotes();
            }
        }

        public Note SelectedNote
        {
            get => selectedNote;
            set
            {
                selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }

        public Visibility EditNotebookNameVisibility
        {
            get { return editNotebookNameVisibility;}
            set
            {
                editNotebookNameVisibility = value;
                OnPropertyChanged(nameof(EditNotebookNameVisibility));
            }
        }

        private void GetNotebooks()
        {
            var notebooks = DatabaseHelper.Read<Notebook>();

            Notebooks.Clear();
            foreach (Notebook notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private void GetNotes()
        {
            if (SelectedNotebook is not null)
            {
                var notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

                Notes.Clear();
                foreach (Note note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartEditing()
        {
            EditNotebookNameVisibility = Visibility.Visible;
        }

        public void StopEditing(Notebook notebook)
        {
            EditNotebookNameVisibility = Visibility.Collapsed;
            DatabaseHelper.Update(notebook);
            GetNotebooks();
        }
    }
}
