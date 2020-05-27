using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using Avalonia.Collections;
using System.Runtime.InteropServices;

namespace UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
         [DllImport("libdl.so")]
        static extern IntPtr dlopen(string filename, int flags);

        private TimeSpan _actualTime;
        private string _actualTrack;
        public MainWindowViewModel()
        {
            Tracks.Add(new Track(1,"Michael Jackson", "Beat it!"));
            Tracks.Add(new Track(2,"Michael Jackson", "Jam!"));
            Tracks.Add(new Track(3,"Michael Jackson", "Who is it?"));
            Tracks.Add(new Track(4,"Michael Jackson", "Closet"));
            Tracks.Add(new Track(5,"Michael Jackson", "Black or white"));

            Play_Pause = ReactiveCommand.Create<object>(RunPlayPause);
            Back = ReactiveCommand.Create<object>(RunBack);
            Forward = ReactiveCommand.Create<object>(RunForward);

            try
            {
 
                Console.WriteLine("Linux Load Library");
                // libbass.so must be the hardfp version of the .so file
                Console.WriteLine("BASSLib: {0:X}", dlopen("libbass.so", 0x101)); // 0x101 = RTLD_GLOBAL+RTLD_LAZY
                //Console.WriteLine("BASSLibMix: {0:X}", dlopen("libbassmix.so", 0x101));
                //Console.WriteLine("BASSLibEnc: {0:X}", dlopen("libbassenc.so", 0x101));
                //Console.WriteLine("BASSLibFx: {0:X}", dlopen("libbass_fx.so", 0x101));
            
                ManagedBass.Bass.Init();    
            }
            catch (System.DllNotFoundException e)
            {
                ActualTrack = e.Message;
            }
            
        }

        public AvaloniaList<Track> Tracks {get;} = new AvaloniaList<Track>();
        public string ActualTrack {
            get => _actualTrack;
            set => this.RaiseAndSetIfChanged(ref _actualTrack, value);
        }
        public TimeSpan ActualTime {
            get => _actualTime;
            set => this.RaiseAndSetIfChanged(ref _actualTime, value);
        }
        
        public ReactiveCommand<object, System.Reactive.Unit> Play_Pause { get; }
        public ReactiveCommand<object, System.Reactive.Unit> Back { get; }
        public ReactiveCommand<object, System.Reactive.Unit> Forward { get; }

        private void RunPlayPause(object o)
        {
            Console.WriteLine("Play!");
        }
        private void RunBack(object o)
        {
            Console.WriteLine("Back!");
        }
        private void RunForward(object o)
        {
            Console.WriteLine("Forward!");
        }
    }

    public class Track : ReactiveUI.ReactiveObject
    {
        private uint _number;
        private string _artist;
        private string _track;
        public Track(uint number, string artist, string track)
        {
            Number = number;
            ArtistName = artist;
            TrackName = track;
        }

        public uint Number 
        {
            get => _number;
            set => this.RaiseAndSetIfChanged(ref _number, value);
        }
        public string TrackName 
        {
            get => _track;
            set => this.RaiseAndSetIfChanged(ref _track, value);
        }
        public string ArtistName
        {
            get => _artist;
            set => this.RaiseAndSetIfChanged(ref _artist, value);
        }
    }
}
