using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Asteroids {

    public partial class Game1 {
        List<EnemyWave> waves;
        List<EnemyWave> wavesDeadpool;
        List<EnemyWave> wavesBirthpool;

        bool inWavesUpdate;

        public void WavesPreInit() {
            waves = new List<EnemyWave>();
            wavesDeadpool = new List<EnemyWave>();
            wavesBirthpool = new List<EnemyWave>();
        }


        public void WavesPostInit() {
        }


        public void UpdateWaves(GameTime delta) {
            inWavesUpdate = true;
            foreach (var wave in waves) {
                if (wavesDeadpool.Contains(wave)) continue;
                wave.Update(this, delta);
            }
            inWavesUpdate = false;

            foreach (var doomed in wavesDeadpool)
                waves.Remove(doomed);
            foreach (var birth in wavesBirthpool)
                waves.Add(birth);
            wavesDeadpool.Clear();
            wavesBirthpool.Clear();
        }


        public void AddWave(EnemyWave newWave) {
            if (inWavesUpdate) wavesBirthpool.Add(newWave);
            else waves.Add(newWave);
        }


        public void KillWave(EnemyWave dedWave) {
            if (inWavesUpdate) wavesDeadpool.Add(dedWave);
            else waves.Remove(dedWave);
        }

    }
}