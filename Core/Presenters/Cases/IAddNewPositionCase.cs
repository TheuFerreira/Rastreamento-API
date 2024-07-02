﻿using Core.Presenters.Requests;

namespace Core.Presenters.Cases
{
    public interface IAddNewPositionCase
    {
        void Execute(IList<NewPositionRequest> newPositionRequest);
    }
}
