using System.ComponentModel.DataAnnotations;

namespace Eds.Shared.Helper.Enums;

public enum ReferenceContentTypes
{
    [Display(Name = "APP_REQUEST_CONTENT")]
    APP_REQUEST_CONTENT = 0,

    [Display(Name = "ANALYSIS_CONTENT")]
    ANALYSIS_CONTENT = 1
}