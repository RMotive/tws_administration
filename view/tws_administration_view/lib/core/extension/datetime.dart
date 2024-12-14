extension DateTimeExtension on DateTime{
  /// [dateOnlyString] set a 'DateOnly' format.
  String get dateOnlyString  => "$year-${month.toString().padLeft(2, '0')}-${day.toString().padLeft(2, '0')}";
  /// [dateOnlyString] get only date and time format.
  String get fullDateString  => "$year-${month.toString().padLeft(2, '0')}-${day.toString().padLeft(2, '0')} ${hour.toString().padLeft(2,'0')}:${minute.toString().padLeft(2,'0')}";

  
}