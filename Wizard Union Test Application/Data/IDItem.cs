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
    public readonly IDTypeItem GetTypeAndID()
    {
        if (Item == null) throw new ArgumentNullException(nameof(Item));
        return new IDTypeItem(Item.GetType(), ID);
    }


    public static implicit operator T(IDItem<T> _item) => _item.Item;
}

public struct IDTypeItem
{
    public int ID;
    public Type Type;

    internal IDTypeItem(Type _type, int _ID)
    {
        (Type, ID) = (_type, _ID);
    }
}
