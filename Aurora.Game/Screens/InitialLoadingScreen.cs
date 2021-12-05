using Aurora.Game.Graphics.Containers;
using Aurora.Game.Graphics.Utilities;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace Aurora.Game.Screens
{
    public class InitialLoadingScreen : Screen
    {
        public Screen ScreenToExitTo { get; init; }

        protected LinkTextFlowContainer SupporterText { get; private set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                SupporterText = new LinkTextFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    TextAnchor = Anchor.BottomCentre,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Padding = new MarginPadding(20f),
                    Alpha = 0,
                    Spacing = new Vector2(0f, 2f)
                }
            };

            void creationParameters(SpriteText x) => x.Font = AuroraFont.TorusFont;

            SupporterText.AddText("Hi, click ", creationParameters);
            SupporterText.AddLink("here", "Opens Google", "https://google.com", null, creationParameters, Color4.White, Color4.Orange);
            SupporterText.AddText(" to open Google.", creationParameters);
            SupporterText.FadeInFromZero(500D);
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            LoadComponentAsync(ScreenToExitTo);

            CheckIfLoaded();
        }

        protected void CheckIfLoaded()
        {
            if (ScreenToExitTo.LoadState != LoadState.Ready)
            {
                Schedule(CheckIfLoaded);
                return;
            }

            // this.Push(ScreenToExitTo);
        }
    }
}
