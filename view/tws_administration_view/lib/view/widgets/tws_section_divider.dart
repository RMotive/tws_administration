import 'package:flutter/material.dart';

/// [TWSSectionDivider] a custom divider component to divide the creation sections in 
/// Trucks, Trailers and drivers sections.
/// This component shows horizontal line with a centered section name, ideal for dividing sections in a column.
class TWSSectionDivider extends StatelessWidget {
  final Color color;
  final String text;
  const TWSSectionDivider({
    super.key,
    required this.color,
    required this.text,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(5.0),
      child: Row(
        children: <Widget>[
          Expanded(
            child: Divider(
              color: color,
            )
          ),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 8.0),
            child: Text(
              text,
              style: TextStyle(
                fontWeight: FontWeight.w100,
                fontStyle: FontStyle.italic,
                color: color
              ),
            ),
          ),
          Expanded(
            child: Divider(
              color: color,
            )
          )
        ],
      ),
    );
  }
}