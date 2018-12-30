using System;
using System.IO;
using System.Collections.Generic;

using Asteroids.Common;

using static Asteroids.Utilities;

namespace Asteroids {

    public class ContentManager {

        /// <summary>
        /// The game the manager is operating on.
        /// </summary>
        public Game1 Game { get; set; }

        #region Private fields


        /// <summary>
        /// Directores to search for content.
        /// </summary>
        private IList<string> contentDirectories;


        /// <summary>
        /// The loaded EnemyFormations, by key.
        /// </summary>
        private Dictionary<string, EnemyFormation> formations;


        /// <summary>
        /// The loaded SpriteSheets, by key.
        /// </summary>
        private Dictionary<string, SpriteSheet> spriteSheets;


        /// <summary>
        /// The loaded Animations, by key.
        /// </summary>
        private Dictionary<string, SpriteAnimation> animations;


        #endregion

        public ContentManager(Game1 game) {
            Game = game;
            contentDirectories = new List<string>();
            formations = new Dictionary<string, EnemyFormation>();
            spriteSheets = new Dictionary<string, SpriteSheet>();
            animations = new Dictionary<string, SpriteAnimation>();
        }


        /// <summary>
        /// Add a new directory to search for content files.
        /// </summary>
        /// <param name="uri">The uri for the directory</param>
        public void AddContentDirectory(string uri) {
            contentDirectories.Add(uri);
        }


        /// <summary>
        /// Load the content
        /// </summary>
        public void LoadContent() {
            LoadFormations();
            LoadSpriteSheets();
            LoadAnimations();
        }

        #region Load Formations

        /// <summary>
        /// Load formations from file.
        /// </summary>
        private void LoadFormations() {
            Game.Log.Load("Loading formations");
            foreach (var directory in contentDirectories) {
                LoadFormations(directory);
            }
        }


        /// <summary>
        /// Load formations from file.
        /// </summary>
        /// <param name="uri">The directory to search for files</param>
        private void LoadFormations(string uri) {
            Game.Log.Load(" - Searching: " + uri);
            foreach (var fileUri in Directory.GetFiles(uri, "*.fdat", SearchOption.AllDirectories)) {
                using (var file = File.OpenRead(fileUri)) {
                    try {
                        var formation = JsonFormation.FromStream(file);
                        if (formation == null) {
                            Game.Log.Error("Failed to load enemy formation from file: " + fileUri);
                        } else if (formations.ContainsKey(formation.Key)) {
                            Game.Log.Error(" - Attempted to add formation with existing key: " + formation.Key);
                            Game.Log.Error(" -  - When loading file: " + fileUri);
                        } else {
                            Game.Log.Debug(String.Format("Loaded '{0}' from {1}", formation.Key, fileUri));
                            formations[formation.Key] = formation;
                        }
                    } catch (Exception ex) {
                        Game.Log.Error("Exception loading formation: " + ExceptionToS(ex));
                    }
                }
            }
        }

        #endregion

        #region Load SpriteSheets

        /// <summary>
        /// Load sprite sheets from file
        /// </summary>
        private void LoadSpriteSheets() {
            Game.Log.Load("Loading sprite sheets");
            foreach (var directory in contentDirectories) {
                LoadSpriteSheets(directory);
            }
        }


        /// <summary>
        /// Load sprite sheets from file
        /// </summary>
        /// <param name="uri">The directory to search for files</param>
        private void LoadSpriteSheets(string uri) {
            Game.Log.Load(" - Searching: " + uri);
            foreach (var fileUri in Directory.GetFiles(uri, "*.ssdat", SearchOption.AllDirectories)) {
                using (var file = File.OpenRead(fileUri)) {
                    try {
                        var spriteSheet = SpriteSheet.FromStream(file, Game.Content);
                        if (spriteSheet == null) {
                            Game.Log.Error("Failed to load sprite sheet from file: " + fileUri);
                        } else if (spriteSheets.ContainsKey(spriteSheet.Key)) {
                            Game.Log.Error(" - Attempted to add sprite sheet with existing key: " + spriteSheet.Key);
                            Game.Log.Error(" -  - When loading file: " + fileUri);
                        } else {
                            Game.Log.Debug(String.Format("Loaded '{0}' from {1}", spriteSheet.Key, fileUri));
                            spriteSheets[spriteSheet.Key] = spriteSheet;
                        }
                    } catch (Exception ex) {
                        Game.Log.Error("Exception loading sprite sheet: " + ExceptionToS(ex));
                    }
                }
            }
        }

        #endregion

        #region Load Animations

        /// <summary>
        /// Load animations from file.
        /// </summary>
        private void LoadAnimations() {
            Game.Log.Load("Loading animations");
            foreach (var directory in contentDirectories) {
                LoadAnimations(directory);
            }
        }


        /// <summary>
        /// Load animations from file.
        /// </summary>
        /// <param name="uri">The directory to search for files</param>
        private void LoadAnimations(string uri) {
            Game.Log.Load(" - Searching: " + uri);
            foreach (var fileUri in Directory.GetFiles(uri, "*.adat", SearchOption.AllDirectories)) {
                using (var file = File.OpenRead(fileUri)) {
                    try {
                        var animation = JsonSpriteAnimation.FromStream(file);
                        if (animation == null) {
                            Game.Log.Error("Failed to load sprite sheet from file: " + fileUri);
                        } else if (animations.ContainsKey(animation.Key)) {
                            Game.Log.Error(" - Attempted to add animation with existing key: " + animation.Key);
                            Game.Log.Error(" -  - When loading file: " + fileUri);
                        } else {
                            Game.Log.Debug(String.Format("Loaded '{0}' from {1}", animation.Key, fileUri));
                            animations[animation.Key] = animation;
                        }
                    } catch (Exception ex) {
                        Game.Log.Error("Exception loading animation: " + ExceptionToS(ex));
                    }
                }
            }
        }

        #endregion

    }
}