﻿using Shared.Dto.CreateGroupDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGroupService
    {
        Task<int> CreateGroup(CreateGroupDto model);
        Task DeleteGroupWithAssignments(int groupId);
    }
}