import 'package:flutter/material.dart';

class MaintenanceGroupOptions {
  final VoidCallback? onRemove;
  final VoidCallback? onCreate;
  final VoidCallback? onEdit;

  const MaintenanceGroupOptions({
    this.onRemove,
    this.onCreate,
    this.onEdit,
  }) : assert(onRemove != onCreate || onRemove != onCreate, 'At least an action callback must be provided');
}
