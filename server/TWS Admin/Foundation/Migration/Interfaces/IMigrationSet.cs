﻿namespace Foundation.Migrations.Interfaces;
public interface IMigrationSet {
    public int Id { get; set; }

    public void EvaluateRead();
    public void EvaluateWrite();
    public Exception[] EvaluateDefinition();
}