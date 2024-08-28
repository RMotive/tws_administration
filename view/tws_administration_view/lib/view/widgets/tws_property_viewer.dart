import 'package:flutter/cupertino.dart';

final class TWSPropertyViewer extends StatelessWidget {
  final String label;
  final String? value;

  const TWSPropertyViewer({
    super.key,
    required this.label,
    required this.value,
  });

  @override
  Widget build(BuildContext context) {
    return Wrap(
      spacing: 8,
      runSpacing: 8,
      children: <Widget>[
        Text(
          '$label:',
          style: const TextStyle(
            fontWeight: FontWeight.w600,
          ),
        ),
        Text(value ?? '---'),
      ],
    );
  }
}
