
class CollectorData{
  Type type;
  String title;
  dynamic value;
  String? error;
  CollectorData({
    required this.type,
    required this.title,
    this.value,
    this.error
  });

}

