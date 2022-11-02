using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Terra
{
    internal class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Backgrounds
        public Texture2D blank;

        //Non-Button Icons
        public Texture2D bell;
        public Texture2D gear;

        //Button Icons
        public Texture2D home;
        public Texture2D plan;
        public Texture2D cart;
        public Texture2D profile;

        //User Input
        public KeyboardState kb;
        public KeyboardState oldKb;
        public MouseState ms;
        public MouseState oldMs;
        public Rectangle mouseRect;

        //Fonts
        public SpriteFont header;
        public SpriteFont prompt;
        public SpriteFont small;

        //User Interface
        public List<Button> buttons;
        public List<Image> images;
        public List<Output> outputs;

        public Game1() : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 360;
            graphics.PreferredBackBufferHeight = 740;
            graphics.ApplyChanges();

            oldKb = Keyboard.GetState();
            oldMs = Mouse.GetState();

            mouseRect = new Rectangle(oldMs.X, oldMs.Y, 1, 1);
            
            buttons = new List<Button>();
            images = new List<Image>();
            outputs = new List<Output>();

            header = this.Content.Load<SpriteFont>("font"); //Header Style
            prompt = this.Content.Load<SpriteFont>("prompt"); //Prompt Style
            small = this.Content.Load<SpriteFont>("small"); //Small Style

            blank = this.Content.Load<Texture2D>("blank");
            bell = this.Content.Load<Texture2D>("bell");
            gear = this.Content.Load<Texture2D>("gear");

            home = this.Content.Load<Texture2D>("home");
            plan = this.Content.Load<Texture2D>("plan");
            cart = this.Content.Load<Texture2D>("cart");
            profile = this.Content.Load<Texture2D>("profile");

            buttons.Add(new Button(
                new Image(blank, new Rectangle(1, 651, 88, 88), Color.DarkGreen), 
                new Image(home, new Rectangle(30, 666, 30, 30), Color.White), 
                new Output(small, new Vector2(24, 706), "Home", Color.White)));
            buttons.Add(new Button(
                new Image(blank, new Rectangle(91, 651, 88, 88), Color.Green), 
                new Image(plan, new Rectangle(110, 656, 50, 50), Color.Black),
                new Output(small, new Vector2(120, 706), "Plan", Color.Black)));
            buttons.Add(new Button(
                new Image(blank, new Rectangle(181, 651, 88, 88), Color.Green), 
                new Image(cart, new Rectangle(210, 666, 30, 30), Color.Black),
                new Output(small, new Vector2(210, 706), "Cart", Color.Black)));
            buttons.Add(new Button(
                new Image(blank, new Rectangle(271, 651, 88, 88), Color.Green),
                new Image(profile, new Rectangle(300, 666, 30, 30), Color.Black),
                new Output(small, new Vector2(294, 706), "Profile", Color.Black)));

            images.Add(new Image(bell, new Rectangle(270, 15, 30, 30), Color.Black));
            images.Add(new Image(gear, new Rectangle(315, 15, 30, 30), Color.Black));

            outputs.Add(new Output(header, new Vector2(35, 20), "Ticket Maestro", Color.White));
            outputs.Add(new Output(prompt, new Vector2(35, 100), "Sign In", Color.Black));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.GraphicsDevice);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            kb = Keyboard.GetState();
            ms = Mouse.GetState();
            mouseRect = new Rectangle(ms.X, ms.Y, 1, 1);

            for (int a = 0; a < buttons.Count; a++)
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed && mouseRect.Intersects(buttons[a].btn.rect))
                {
                    buttons[a].btn.color = Color.DarkGreen;
                    buttons[a].png.color = Color.White;
                    buttons[a].op.color = Color.White;

                    for (int b = 0; b < buttons.Count; b++)
                        if (b != a)
                        {
                            buttons[b].btn.color = Color.Green;
                            buttons[b].png.color = Color.Black;
                            buttons[b].op.color = Color.Black;
                        }

                    break;
                }

            oldKb = Keyboard.GetState();
            oldMs = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.White);
            this.spriteBatch.Begin();

            spriteBatch.Draw(blank, new Rectangle(0, 0, 360, 60), Color.DarkGreen);

            for (int a = 0; a < buttons.Count; a++)
            {
                spriteBatch.Draw(buttons[a].btn.text, buttons[a].btn.rect, buttons[a].btn.color);
                spriteBatch.Draw(buttons[a].png.text, buttons[a].png.rect, buttons[a].png.color);
                spriteBatch.DrawString(buttons[a].op.font, buttons[a].op.str, buttons[a].op.vec, buttons[a].op.color);
            }

            for (int a = 0; a < images.Count; a++)
                spriteBatch.Draw(images[a].text, images[a].rect, images[a].color);

            for (int a = 0; a < outputs.Count; a++)
                spriteBatch.DrawString(outputs[a].font, outputs[a].str, outputs[a].vec, outputs[a].color);


            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public class Button
    {
        public Image btn;
        public Image png;
        public Output op;

        public Button(Image b, Image p, Output o)
        {
            this.btn = b;
            this.png = p;
            this.op = o;
        }
    }

    public class Image
    {
        public Texture2D text;
        public Rectangle rect;
        public Color color;

        public Image(Texture2D t, Rectangle r, Color c)
        {
            this.text = t;
            this.rect = r;
            this.color = c;
        }
    }

    public class Output
    {
        public SpriteFont font;
        public Vector2 vec;
        public string str;
        public Color color;

        public Output(SpriteFont f, Vector2 v, string s, Color c)
        {
            this.font = f;
            this.vec = v;
            this.str = s;
            this.color = c;
        }
    }

    enum AppState
    {
        Home,
        Cart,
        Plan,
        Profile,
        Ticket, //Home
        Search, //Plan
        AddFunds, //Cart
        Confirmation, //Cart
        Payment, //Cart
        Login, //Profile
        Settings, //Profile
        Support //Profile
    }
}
