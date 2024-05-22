
/// [CollectorOptions] class that has the common properties for the designed input options in [CollectorFrameTable]
class CollectorOptions {
  final bool required;
  final String label;
  final double? width;
  final double? height;
  final String? errorMessage;
  const CollectorOptions({
    this.required = false,
    required this.label,
    this.width,
    this.height,
    this.errorMessage
  });
}