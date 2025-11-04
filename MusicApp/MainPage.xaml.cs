using CommunityToolkit.Maui.Media;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Windows.Media.Core;

namespace MusicApp
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<string> _songs = new();
        private MediaElement _player;
        private int _currentIndex = -1;

        public MainPage()
        {
            InitializeComponent();

            _player = new MediaElement
            {
                ShouldShowPlaybackControls = false,
                IsVisible = false
            };

            _songs.Add("song1.mp3");
            _songs.Add("song2.mp3");
            _songs.Add("song3.mp3");

            SongsCollection.ItemsSource = _songs;
            ContentLayout.Children.Add(_player); // jeśli masz StackLayout o nazwie ContentLayout
        }

        private void OnAddSongClicked(object sender, EventArgs e)
        {
            // Dodawanie pliku z pamięci urządzenia (prosty przykład)
            _songs.Add($"utwor_{_songs.Count + 1}.mp3");
        }

        private void OnSongSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is string selectedSong)
            {
                _currentIndex = _songs.IndexOf(selectedSong);
                PlayCurrentSong();
            }
        }

        private void PlayCurrentSong()
        {
            if (_currentIndex < 0 || _currentIndex >= _songs.Count)
                return;

            string songName = _songs[_currentIndex];

            // Plik powinien być w Resources/Raw/
            _player.Source = MediaSource.FromFile(songName);
            _player.Play();
        }

        private void OnPlayClicked(object sender, EventArgs e)
        {
            _player.Play();
        }

        private void OnPauseClicked(object sender, EventArgs e)
        {
            _player.Pause();
        }

        private void OnPreviousClicked(object sender, EventArgs e)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                PlayCurrentSong();
            }
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            if (_currentIndex < _songs.Count - 1)
            {
                _currentIndex++;
                PlayCurrentSong();
            }
        }
    }
}
