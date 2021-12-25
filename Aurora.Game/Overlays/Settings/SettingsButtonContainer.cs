using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Aurora.Game.Overlays.Settings
{
    public class SettingsButtonContainer : Container
    {
        public const float SIZE = 60f;

        public SettingsButtonContainer()
        {
            RelativeSizeAxes = Axes.X;
            Size = new Vector2(1f, SIZE);
            AlwaysPresent = true;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new UtilityBarBackground(new Colour4(0.1f, 0.1f, 0.1f, 1f)),
                new FillFlowContainer
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        new DummySettingsButton(),
                        new Dummy2SettingsButton()
                    }
                }
            };
        }
    }
}
