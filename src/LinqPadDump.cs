#region Copyright (c) 2018 Atif Aziz. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Choices
{
    partial class Choice<T1, T2>
    {
        internal object ToDump() =>
            Match(a => new { Chosen = 1, Value = (object) a },
                  b => new { Chosen = 2, Value = (object) b });
    }

    partial class Choice<T1, T2, T3>
    {
        internal object ToDump() =>
            Match(a => new { Chosen = 1, Value = (object) a },
                  b => new { Chosen = 2, Value = (object) b },
                  c => new { Chosen = 3, Value = (object) c });
    }

    partial class Choice<T1, T2, T3, T4>
    {
        internal object ToDump() =>
            Match(a => new { Chosen = 1, Value = (object) a },
                  b => new { Chosen = 2, Value = (object) b },
                  c => new { Chosen = 3, Value = (object) c },
                  d => new { Chosen = 4, Value = (object) d });
    }
}
