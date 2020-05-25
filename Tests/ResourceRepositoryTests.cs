using Repositories;
using System;
using Xunit;

namespace Tests
{
    public class ResourceRepositoryTests
    {
        [Fact]
        public async void CreateResource_Success()
        {
            var resourceRepo = new ResourceRepository();
            try
            {
                var response = await resourceRepo.CreateResource("somecooldata", "https://fred.stlouisfed.org/graph/fredgraph.csv?bgcolor=%23e1e9f0&chart_type=line&drp=0&fo=open%20sans&graph_bgcolor=%23ffffff&height=450&mode=fred&recession_bars=on&txtcolor=%23444444&ts=12&tts=12&width=1168&nt=0&thu=0&trc=0&show_legend=yes&show_axis_titles=yes&show_tooltip=yes&id=DGS10&scale=left&cosd=2014-10-11&coed=2019-10-11&line_color=%234572a7&link_values=false&line_style=solid&mark_type=none&mw=3&lw=2&ost=-99999&oet=99999&mma=0&fml=a&fq=Daily&fam=avg&fgst=lin&fgsnd=2009-06-01&line_index=1&transformation=lin&vintage_date=2019-10-15&revision_date=2019-10-15&nd=1962-01-02");
                Assert.NotNull(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
