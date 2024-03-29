﻿using Fluxor;

namespace BlazorShowcase.RazorLib.Store.WellKnownModelKindCase;

public partial class WellKnownModelKindState
{
    private class Reducer
    {
        [ReducerMethod]
        public static WellKnownModelKindState ReduceSetWellKnownModelKindAction(
            WellKnownModelKindState inWellKnownModelKindState,
            SetWellKnownModelKindAction setWellKnownModelKindAction)
        {
            return new WellKnownModelKindState(setWellKnownModelKindAction.WellKnownModelKind);
        }
    }
}