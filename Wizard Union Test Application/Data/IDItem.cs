using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WU_Test;

public struct IDItem<T>
{
    public int ID;
    public T Item;

    public IDItem(T _item, int _ID)
    {
        (Item, ID) = (_item, _ID);
    }

    public readonly T Get() => Item;
    public readonly int GetID() => ID;

    public static implicit operator T(IDItem<T> _item) => _item.Item;
}
