﻿using Foundation.Migrations.Exceptions;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Quality.Records;

using Xunit;

namespace Foundation.Migrations.Quality.Bases;
/// <summary>
///     Base Quality for [Q_Entity].
///     
///     Defines what quality operations must be performed by a [Q_Entity].
///     
///     [Q_Entity] concept: determines a quality implementation to qualify 
///     a [MigrationSource] [Entity] implementation.
/// </summary>
public abstract class BQ_MigrationSet<TSet>
    where TSet : IMigrationSet, new() {

    protected abstract Q_MigrationSet_EvaluateRecord<TSet>[] EvaluateFactory(Q_MigrationSet_EvaluateRecord<TSet>[] Container);

    [Fact]
    public void EvaluateDefinition() {
        TSet mock = new();
        mock.EvaluateDefinition();
    }

    [Fact]
    public void Evaluate() {
        Q_MigrationSet_EvaluateRecord<TSet>[] checks = EvaluateFactory([]);


        foreach (Q_MigrationSet_EvaluateRecord<TSet> qualityCheck in checks) {
            TSet mock = qualityCheck.Mock;
            (string property, (IValidator validator, int code)[] reasons)[] asserts = qualityCheck.Expectations;

            // --> The set is expected to not throw exceptions
            if (asserts.Length == 0) {
                continue;
            }

            // --> Here are asserts to perform
            try {
                mock.EvaluateRead();
                if (asserts.Length == 0) continue;
                Assert.Fail("Asserts expected but none caught");
            } catch (XBMigrationSet_Evaluate x) {
                (string property, XIValidator_Evaluate[] faults)[] unvalidations = x.Unvalidations;

                Assert.Equal(asserts.Length, unvalidations.Length);


                unvalidations = [.. unvalidations.OrderBy(x => x.property)];
                asserts = [.. asserts.OrderBy(x => x.property)];

                for (int i = 0; i < unvalidations.Length; i++) {
                    (string Property, XIValidator_Evaluate[] Faults) unvalidation = unvalidations[i];
                    (string Property, (IValidator Validator, int Code)[] Reasons) assert = asserts[i];

                    Assert.Equal(assert.Property, unvalidation.Property);
                    Assert.Equal(assert.Reasons.Length, unvalidation.Faults.Length);

                    XIValidator_Evaluate[] faults = unvalidation.Faults;
                    (IValidator Validator, int Code)[] reasons = assert.Reasons;

                    faults = [.. faults.OrderBy(i => i.Code)];
                    reasons = [.. reasons.OrderBy(i => i.Code)];

                    for (int j = 0; j < faults.Length; j++) {
                        XIValidator_Evaluate fault = faults[j];
                        (IValidator Validator, int Code) reason = reasons[j];


                        Assert.Equal(reason.Code, fault.Code);
                        Assert.IsType(reason.Validator.GetType(), fault.Validator);
                    }
                }
            }
        }
    }
}
