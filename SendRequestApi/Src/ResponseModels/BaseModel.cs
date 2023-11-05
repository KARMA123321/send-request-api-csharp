using System.Collections;

namespace SendRequestApi.ResponseModels;

public class BaseModel
{
    protected virtual string[]? GetFaultyChecks() => null;
    
    public bool CompareTo(BaseModel? other, bool isFaultyChecks = true, Dictionary<Type, string[]>? excludedChecks = null)
    {
        if (other is null)
        {
            throw new Exception($"The second comparison object '{other}' is null");
        }
        
        var props = GetType().GetProperties();
        
        foreach (var prop in props)
        {
            var propName = prop.Name;
            var propValue = prop.GetValue(this);
            var otherPropValue = prop.GetValue(other);
            var key = $"{GetType().Name}.{propName}";
            
            var faultyChecks = GetFaultyChecks();

            if (isFaultyChecks == false && faultyChecks is not null && faultyChecks.Length > 0 && faultyChecks.Contains(propName)) continue;
            
            if (excludedChecks != null)
            {
                var type = GetType();
                if (excludedChecks.TryGetValue(type, out var value) && value.Contains(propName)) continue;
            }
            
            if (propValue is null || otherPropValue is null)
            {
                if ((propValue is null && otherPropValue is not null) || (propValue is not null && otherPropValue is null)) 
                    throw new Exception($"{key}: value '{propValue}' is not equal to '{otherPropValue}'");
            }
            else switch (propValue)
            {
                case IComparable thisPrimitive when otherPropValue is IComparable otherPrimitive:
                    Compare(thisPrimitive, otherPrimitive, $"{key}");
                    break;
                case BaseModel thisObject when otherPropValue is BaseModel otherObject:
                    thisObject.CompareTo(otherObject, isFaultyChecks, excludedChecks);
                    break;
                case IList thisArr when otherPropValue is IList otherArr:
                {
                    if (thisArr.Count != otherArr.Count) throw new Exception(
                        $"{key}: number of elements '{thisArr.Count}' of the first array {thisArr.GetType().Name} " +
                        $"is not equal to the number of elements '{otherArr.Count}' of the second array");
                    for (var i = 0; i < thisArr.Count; i++)
                    {
                        switch (thisArr[i])
                        {
                            case IComparable thisPrim when otherArr[i] is IComparable otherPrim:
                                Compare(thisPrim, otherPrim, $"{key}");
                                break;
                            case BaseModel thisObj when otherArr[i] is BaseModel otherObj:
                                thisObj.CompareTo(otherObj, isFaultyChecks, excludedChecks);
                                break;
                        }
                    }

                    break;
                }
            }
        }
        
        return true;
    }
    
    private static void Compare<T>(T? actual, T? other, string key) where T: IComparable
    {
        var comparison = 0;
        
        if (actual is null || other is null)
        {
            if ((actual is null && other is not null) || (actual is not null && other is null)) comparison = 1;
        }
        else
        {
            if (actual is string str)
            {
                comparison = string.Compare(str, other.ToString(), StringComparison.Ordinal);
            }
            else
            {
                comparison = actual.CompareTo(other);
            }
        }
        
        if (comparison != 0) throw new Exception($"{key}: value '{actual}' is not equal to '{other}'");
    }
}