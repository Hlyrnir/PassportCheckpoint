using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using PassportCheckpoint.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassportCheckpoint
{
    public class PassportRestrictedView : ComponentBase
    {
        private PassportState? ppState;
        private bool? bIsAuthorized;

        [CascadingParameter(Name = CascadingParameterName.PassportState)]
        private Task<PassportState>? PassportState { get; set; }

        [Parameter]
        public required IEnumerable<IPassportVisa> RequiredVisa { get; init; }

        [Parameter]
        public RenderFragment<PassportState>? NotAuthorized { get; set; }

        [Parameter]
        public RenderFragment? Authorizing { get; set; }

        [Parameter]
        public RenderFragment<PassportState>? Authorized { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder rtBuilder)
        {
            if (bIsAuthorized is null)
            {
                rtBuilder.AddContent(0, Authorizing);
            }
            else if (bIsAuthorized == true)
            {
                rtBuilder.AddContent(0, Authorized?.Invoke(ppState!));
            }
            else
            {
                rtBuilder.AddContent(0, NotAuthorized?.Invoke(ppState!));
            }

            base.BuildRenderTree(rtBuilder);
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            bIsAuthorized = null;

            if (PassportState is not null)
                ppState = await PassportState;

            bIsAuthorized = IsAuthorized();
        }

        private bool IsAuthorized()
        {
            if (ppState is null)
                return false;

            if (ppState.IsAuthenticated == false)
                return false;

            using (IEnumerator<IPassportVisa> enumEnumerator = RequiredVisa.GetEnumerator())
            {
                if (enumEnumerator.MoveNext() == false)
                    return true;
            };

            bool bIsAuthorized = false;

            foreach (IPassportVisa ppActualVisa in RequiredVisa)
            {
                if (ppState.RequiredVisaExists(ppActualVisa) == true)
                    bIsAuthorized = true;
            }

            return bIsAuthorized;
        }
    }
}