﻿// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Tests.Setups.Models
{
    public class EqualityOperatorModel
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public static bool operator ==(EqualityOperatorModel first, EqualityOperatorModel second)
        {
            if (ReferenceEquals(first, null) && ReferenceEquals(second, null))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return first.Integer == second.Integer;
        }

        public static bool operator !=(EqualityOperatorModel first, EqualityOperatorModel second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj) || ReferenceEquals(this, obj) || obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((EqualityOperatorModel)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Integer * 397) ^ (this.String != null ? this.String.GetHashCode() : 0);
            }
        }

        protected bool Equals(EqualityOperatorModel other)
        {
            return this.Integer == other.Integer;
        }
    }
}
