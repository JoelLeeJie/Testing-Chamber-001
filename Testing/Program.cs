using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Testing
{


    public class Solution
    {
        public int RepeatedNTimes(int[] nums)
        {
            int counter = 0;
            foreach(int number in nums)
            {
                counter = nums.Count<int>((i) => i == number);
                if (counter > 1) return number;            
            }
            return 0;
        }
    }
    
}
