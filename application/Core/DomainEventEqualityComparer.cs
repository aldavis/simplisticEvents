﻿using System;
using System.Collections.Generic;
using System.Text;

namespace application.Core
{
	public class DomainEventEqualityComparer : IEqualityComparer<IDomainEvent>
	{
		public static readonly DomainEventEqualityComparer Instance = new DomainEventEqualityComparer();

		public bool Equals(IDomainEvent x, IDomainEvent y)
		{
			return x.Id == y.Id;
		}

		public int GetHashCode(IDomainEvent obj)
		{
			return obj.Id.GetHashCode();
		}
	}
}
