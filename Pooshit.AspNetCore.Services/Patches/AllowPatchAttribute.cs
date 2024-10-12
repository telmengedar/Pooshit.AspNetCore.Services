using System;

namespace Pooshit.AspNetCore.Services.Patches;

/// <summary>
/// attribute used to indicate that a property can get patched using public endpoints
/// </summary>
public class AllowPatchAttribute : Attribute;