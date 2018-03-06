using Android.App;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using Android.Media;
using Android.Runtime;
using static Android.Widget.SeekBar;

namespace lab4
{
    [Activity(Label = "lab4", MainLauncher = true)]
    public class MainActivity : Activity, ISensorEventListener, IOnSeekBarChangeListener
    {
        SensorManager sensorManager;
        Android.Media.MediaPlayer mp;
        SeekBar seekBar;
        TextView text;
        float volume;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            //инициализируем сенсорный менеджер
            sensorManager = (SensorManager)GetSystemService(SensorService);
            //инициализируем медиа проигрыватель

            mp = MediaPlayer.Create(this, Resource.Raw.music);
            seekBar = FindViewById<SeekBar>(Resource.Id.seekBarForVolume);
            text = (TextView)FindViewById<TextView>(Resource.Id.textStatus);

            volume = 0.5f;
            seekBar.SetOnSeekBarChangeListener(this);
            seekBar.Progress = (int)(volume * 100);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            volume = seekBar.Progress / 100f;
            mp.SetVolume(volume, volume);
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (!mp.IsPlaying)
            {
                mp.Start();
                text.Text = "on";
            }
            else
            {
                mp.Pause();
                text.Text = "off";
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.Light), SensorDelay.Ui);
        }

        protected override void OnPause()
        {
            base.OnPause();
            sensorManager.UnregisterListener(this);
        }

        public void OnStartTrackingTouch(SeekBar seekBar)
        {
        }

        public void OnStopTrackingTouch(SeekBar seekBar)
        {
        }
    }
}

