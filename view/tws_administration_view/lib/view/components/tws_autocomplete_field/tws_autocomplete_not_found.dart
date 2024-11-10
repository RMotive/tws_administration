part of 'tws_autocomplete_field.dart';

class _TWSAutocompleteNotfound extends StatelessWidget {
  final double height;
  final Color color;
  const _TWSAutocompleteNotfound({
    required this.height,
    required this.color
  });

  @override
  Widget build(BuildContext context) {
    return  SizedBox(
      height: height,
      child: Row(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          Icon(
            Icons.error_outline,
            color: color,
          ),
          const SizedBox(width: 5), 
          Flexible(
            child: Text(
              "Not matches found",
              style: TextStyle(
                overflow: TextOverflow.ellipsis,
                color: color
              ),
            ),
          ),
        ],
      ),
    );
  }
}