using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Plugin.Maui.Audio;

namespace MusicApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<SongItem> Playlist { get; set; } = new ObservableCollection<SongItem>();

        private int currentIndex = -1;
        private IAudioPlayer player;

        public MainPage()
        {
            InitializeComponent();
            PlaylistView.ItemsSource = Playlist;
        }

        public class SongItem
        {
            public string FilePath { get; set; }
            public string FileName { get; set; }
        }

        private async void AddSongButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var customFileType = new FilePickerFileType(new System.Collections.Generic.Dictionary<DevicePlatform, System.Collections.Generic.IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "audio/*" } },
                    { DevicePlatform.iOS, new[] { "public.audio" } },
                    { DevicePlatform.WinUI, new[] { ".mp3", ".wav", ".m4a" } },
                    { DevicePlatform.MacCatalyst, new[] { "public.audio" } }
                });

                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Wybierz plik audio",
                    FileTypes = customFileType
                });

                if (result != null)
                {
                    var song = new SongItem
                    {
                        FilePath = result.FullPath,
                        FileName = System.IO.Path.GetFileName(result.FullPath)
                    };

                    Playlist.Add(song);
                    currentIndex = Playlist.Count - 1;

                    CurrentSongLabel.Text = song.FileName;

                    player?.Stop();
                    player = AudioManager.Current.CreatePlayer(song.FilePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", $"Nie udało się dodać pliku: {ex.Message}", "OK");
            }
        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            if (player != null && !player.IsPlaying) player.Play();
        }

        private void PauseButton_Clicked(object sender, EventArgs e)
        {
            if (player != null && player.IsPlaying) player.Pause();
        }

        private void StopButton_Clicked(object sender, EventArgs e)
        {
            if (player != null) player.Stop();
        }

        private void PreviousButton_Clicked(object sender, EventArgs e)
        {
            if (Playlist.Count == 0) return;

            currentIndex--;
            if (currentIndex < 0) currentIndex = Playlist.Count - 1;

            PlayCurrentSong();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            if (Playlist.Count == 0) return;

            currentIndex++;
            if (currentIndex >= Playlist.Count) currentIndex = 0;

            PlayCurrentSong();
        }

        private void PlayCurrentSong()
        {
            if (currentIndex < 0 || currentIndex >= Playlist.Count) return;

            var song = Playlist[currentIndex];
            CurrentSongLabel.Text = song.FileName;

            player?.Stop();
            player = AudioManager.Current.CreatePlayer(song.FilePath);
            player.Play();
        }

        private void PlaylistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSong = e.CurrentSelection.Count > 0 ? e.CurrentSelection[0] as SongItem : null;
            if (selectedSong != null)
            {
                currentIndex = Playlist.IndexOf(selectedSong);
                PlayCurrentSong();
            }
        }
    }
}
