import 'package:intl/intl.dart';

///
extension DateTimeExtension on DateTime{
  /// [dateOnlyString] set a 'DateOnly' format.
  String get dateOnlyString  => "$year-${month.toString().padLeft(2, '0')}-${day.toString().padLeft(2, '0')}";
  
  ///
  String get display => DateFormat('yyyy-MM-dd HH:mm').format(this);
}