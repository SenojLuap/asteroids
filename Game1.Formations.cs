using System.Collections.Generic;
using System.IO;

using Asteroids.Common;

namespace Asteroids {
    public partial class Game1 {
        public IDictionary<string, EnemyFormation> formations;

        public void FormationsPreInit() {
            formations = new Dictionary<string, EnemyFormation>();
            
            string formationsPath = Directory.GetCurrentDirectory() + "/assets/formations/";
            foreach (var fileUri in Directory.GetFiles(formationsPath)) {
                var newFormation = JsonFormation.FromFile(fileUri);
                if (newFormation != null)
                    formations[newFormation.Key] = newFormation;
            }
        }


        public void FormationsPostInit() {
        }
    }
}