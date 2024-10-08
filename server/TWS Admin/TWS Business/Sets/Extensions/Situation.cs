﻿using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Validators;

namespace TWS_Business.Sets;

public partial class Situation
    : BMigrationSet
{

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container)
    {
        RequiredValidator Required = new();

        Container = [
                .. Container,
                (nameof(Name), [Required, new LengthValidator(1, 25)]),
                (nameof(Description), [new LengthValidator(1, 100)]),

        ];

        return Container;
    }
    public virtual ICollection<Truck>? Trucks { get; set; } = [];

}
