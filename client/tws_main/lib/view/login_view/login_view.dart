import 'package:flutter/material.dart';
import 'package:tws_main/widgets/themed_widget.dart';

part './widgets/connection_health_card.dart';

class LoginScreen extends StatelessWidget {
  const LoginScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const _ConnectionHealthCard();
  }
}
