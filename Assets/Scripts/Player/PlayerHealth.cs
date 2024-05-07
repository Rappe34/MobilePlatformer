using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class PlayerHealth : Health
    {
        private void Die()
        {
            Destroy(gameObject);

        }
    }
}
