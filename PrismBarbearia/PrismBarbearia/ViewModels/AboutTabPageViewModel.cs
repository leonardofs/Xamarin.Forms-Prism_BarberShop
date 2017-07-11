using Prism.Navigation;
using Prism.Services;

namespace PrismBarbearia.ViewModels
{
    public class AboutTabPageViewModel : BaseViewModel
    {
        public string ConceptText { get; set; }
        public AboutTabPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "A BARBEARIA";
            ConceptText = "Toalha quente, navalha e cordialidade, a barbearia Eight Ball tem uma inovadora proposta em estética," +
                " exclusivamente para o público masculino, com estrutura ampla, moderna e bem decorada; ambiente acolhedor," +
                " climatizado e muito agradável, atentando para o retorno da clássica elegância masculina e os cuidados cada vez maiores com a aparência. " +
                "A barbearia traz serviços como os classicos barba, cabelo e bigode, a tratamentos modernos e tinturas, " +
                "Aproveite nosso ambiente super agradável com bar e mesa de sinuca, WIFI, programação esportiva, revistas especializadas, café espresso, " +
                "cervejas especiais e uma linha de produtos formuladas para as necessidades específicas do homem.";
        }
    }
}