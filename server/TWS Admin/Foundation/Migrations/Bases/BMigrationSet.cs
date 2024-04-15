﻿using System.Reflection;

using Foundation.Contracts.Modelling.Bases;
using Foundation.Migrations.Exceptions;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

using Microsoft.IdentityModel.Tokens;

namespace Foundation.Migrations.Bases;
public abstract class BMigrationSet
    : BObject<IMigrationSet>, IMigrationSet {

    private (string Property, IValidator[] Validators)[]? Validators { get; set; }
    private bool Defined { get; set; } = false;

    protected abstract (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container);
    public void Evaluate() {
        Validators ??= Validations([
            (nameof(Id), [ new PointerValidator(), ])        
        ]);
        (string property, XIValidator_Evaluate[] faults)[] unvalidations = []; 

        int quantity = Validators.Length;
        for (int i = 0; i < quantity; i++) {
            (string property, IValidator[] validations) = Validators[i];
            PropertyInfo pi = GetProperty(property);
            object? value = pi.GetValue(this);


            XIValidator_Evaluate[] faults = [];
            foreach (IValidator validator in validations) {
                try {
                    validator.Evaluate(pi, value);
                } catch (XIValidator_Evaluate x) {
                    faults = [.. faults, x];
                }
            }
            
            unvalidations = [..unvalidations, (property, faults)];
        }

        if (unvalidations.IsNullOrEmpty()) return;
        throw new XBMigrationSet_Evaluate(GetType(), unvalidations);
    }

    public Exception[] EvaluateDefinition() {
        if (Defined) return [];

        Validators ??= Validations([]);
        IEnumerable<string> toEvaluate = Validators
            .Select(i => i.Property);
        Exception[] faults = [];

        // Checking if the validations defintions aren't duplicated.
        IEnumerable<string> duplicated = toEvaluate
            .GroupBy(i => i)
            .Where(g => g.Count() > 1)
            .Select(i => i.Key);
        if (duplicated.Any()) {
            faults = [
                .. faults,
                new XBMigrationSet_EvaluateDefinition(duplicated, XBMigrationSet_EvaluateDefinition.Reasons.Duplication, GetType(), null),
            ];
        }

        // Checking if all the validations defined properties exist in the class definition.
        PropertyInfo[] properties = GetType().GetProperties();
        IEnumerable<string> existProperties = properties
            .Select(i => i.Name);
        IEnumerable<string> Unexist = toEvaluate.Except(existProperties);
        if (Unexist.Any()) {
            faults = [
                    .. faults,
                new XBMigrationSet_EvaluateDefinition(Unexist, XBMigrationSet_EvaluateDefinition.Reasons.Unexist, GetType(), null),
            ];
        }

        /// Checking if properties types satisfies Validators typing boundries.
        foreach ((string property, IValidator[] validators) in Validators) {
            try {
                PropertyInfo targetProperty = GetProperty(property);

                Type propertyType = targetProperty.PropertyType;
                foreach (IValidator validator in validators) {
                    if (validator.Satisfy(propertyType)) continue;

                    faults = [.. faults, new XBMigrationSet_EvaluateDefinition([property], XBMigrationSet_EvaluateDefinition.Reasons.Unsatisfies, GetType(), validator)];
                }
            } catch (XGetProperty) {
                faults = [.. faults, new XBMigrationSet_EvaluateDefinition([property], XBMigrationSet_EvaluateDefinition.Reasons.Unreflected, GetType(), null)];
            }
        }

        if (faults.Length > 0) return faults;

        Defined = true;
        return [];
    }

    public abstract int Id { get; set; }
}
