﻿using JsonEncryptor.Application.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonEncryptor.Application.Services.Json;

public class JsonService : IJsonService
{
    /// <summary>
    /// Checks if the provided string is a valid JSON.
    /// </summary>
    /// <param name="input">The input string to check.</param>
    /// <returns>true if the input is valid JSON; otherwise, false.</returns>
    public bool IsJsonValid(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        input = input.Trim();

        if (input.StartsWith("{") && input.EndsWith("}") || // For objects
            input.StartsWith("[") && input.EndsWith("]"))   // For arrays
        {
            try
            {
                // Attempt to parse the input as a JToken
                JToken.Parse(input);
                return true;
            }
            catch (JsonReaderException)
            {
                // JSON parsing error
                return false;
            }
        }

        return false;
    }

    /// <summary>
    /// Formats a JSON string to be more human-readable with indented formatting.
    /// </summary>
    /// <param name="input">The JSON string to prettify. If the string is null or empty, the method returns an empty string.</param>
    /// <returns>A prettified JSON string with indented formatting. If the input is null or empty, returns an empty string.</returns>
    public string PrettifyJson(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return string.Empty;
        }

        if (IsJsonValid(json) is false)
        {
            return json;
        }

        string value = json.Trim();

        if (value.StartsWith("{") && value.EndsWith("}"))
        {
            return JObject.Parse(json)
                          .ToString(Formatting.Indented);
        }
        else if (value.StartsWith("[") && value.EndsWith("]"))
        {
            return JArray.Parse(json)
                         .ToString(Formatting.Indented);
        }

        return json;

    }

    public T? GetValueFromKey<T>(string json, string key)
    {
        if (string.IsNullOrEmpty(json) || string.IsNullOrEmpty(key))
        {
            return default;
        }

        JObject jsonObject = JObject.Parse(json);
        string[] keys = key.Split('.');

        JToken? currentToken = jsonObject;
        foreach (var k in keys)
        {
            if (k.Contains("[") && k.Contains("]"))
            {
                var arrayName = k.Substring(0, k.IndexOf("["));
                var indexPart = k.Substring(k.IndexOf("[")).Trim('[', ']');
                if (int.TryParse(indexPart, out int index))
                {
                    if (currentToken is JObject obj && obj.TryGetValue(arrayName, out JToken? arrayToken))
                    {
                        if (arrayToken is JArray array && index < array.Count)
                        {
                            currentToken = array[index];
                        }
                        else
                        {
                            return default;
                        }
                    }
                    else
                    {
                        return default;
                    }
                }
                else
                {
                    return default;
                }
            }
            else
            {
                if (currentToken is JObject obj)
                {
                    if (!obj.TryGetValue(k, out currentToken))
                    {
                        return default;
                    }
                }
                else
                {
                    return default;
                }
            }
        }

        return currentToken.Value<T>();
    }


    //public T? GetValueFromKey<T>(string json, string key)
    //{
    //    if (string.IsNullOrEmpty(json) || string.IsNullOrEmpty(key))
    //    {
    //        return default;
    //    }

    //    JObject jsonObject = JObject.Parse(json);

    //    string[] keys = key.Split('.');

    //    JToken? currentToken = jsonObject;
    //    foreach (var k in keys)
    //    {
    //        if (currentToken is JObject obj)
    //        {
    //            if (!obj.TryGetValue(k, out currentToken))
    //            {
    //                return default;
    //            }
    //        }
    //        else
    //        {
    //            return default;
    //        }
    //    }

    //    return currentToken.Value<T>();
    //}

    public string? ReplaceValue(string json, string key, string newValue, Formatting formatting = Formatting.None)
    {
        if (string.IsNullOrEmpty(json))
        {
            return string.Empty;
        }

        JObject jObject = JObject.Parse(json);
        JToken? token = jObject.SelectToken($"$..{key}");

        if (token != null)
        {
            token.Replace(newValue);

            return jObject.ToString(formatting);
        }
        else
        {
            return json;
        }
    }
}
