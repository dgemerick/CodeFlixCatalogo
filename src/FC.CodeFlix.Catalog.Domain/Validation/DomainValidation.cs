﻿using FC.CodeFlix.Catalog.Domain.Exceptions;

namespace FC.CodeFlix.Catalog.Domain.Validation;
public class DomainValidation
{
    public static void NotNull(object? target, string fieldName)
    {
        if(target is null)
            throw new EntityValidationException($"{fieldName} should not be null");
    }

    public static void NotNullOrEmpty(string target, string fieldName)
    {
        if (String.IsNullOrWhiteSpace(target))
            throw new EntityValidationException($"{fieldName} should not be null or empty");
    }
}
