﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface_Layer;
using Factory_Layer;
using Interface_Layer.dto;

namespace Logic_Layer
{
    public class JourneyCollection
    {
        IJourneyCollectionDAL journeyCollectionDAL = Factory.CreateIJourneyCollectionDAL();

        public int Add(Journey journey)
        {
            return journeyCollectionDAL.AddJourney((DTOJourney)journey);
        }
    }
}