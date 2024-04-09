using Xunit;

namespace TWS_Security.Quality;
public class Q_TWSSecuritySource {
    private readonly TWSSecuritySource Source;


    public Q_TWSSecuritySource() {
        Source = new TWSSecuritySource();
    }

    [Fact]
    public async void SourceLoad() {
        bool SourceCanConnect = await Source.Database.CanConnectAsync();
        Assert.True(SourceCanConnect);
    }
}
