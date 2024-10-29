part of '../yardlog_page.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter extends TWSArticleTableAdapter<YardLog> {
  const _TableAdapter();

  @override
  Future<SetViewOut<YardLog>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    final SetViewOptions<YardLog> options = SetViewOptions<YardLog>(false, range, page, null, orderings, <SetViewFilterNodeInterface<YardLog>>[]);
    String auth = _sessionStorage.session!.token;
    MainResolver<SetViewOut<YardLog>> resolver = await Sources.foundationSource.yardLogs.view(options, auth);

    SetViewOut<YardLog> view = await resolver.act((Map<String, dynamic> json) => SetViewOut<YardLog>.des(json, YardLog.des)).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('yard-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
  
  @override
  TWSArticleTableEditor? composeEditor(YardLog set, void Function() closeReinvoke, BuildContext context) {
    return TWSArticleTableEditor(
      form: const SizedBox.shrink(), 
      onCancel: (){}
    );
  }
  
  Uint8List? _getBytes(String base64){
    try{
     return base64Decode(base64);
    }catch(ex){
      return null;
    }
  }

  String getTruckPlates(YardLog item){
    if(item.truckNavigation != null){
      if(item.truckNavigation!.plates.isNotEmpty) return item.truckNavigation!.plates.first.identifier;
    }else if(item.truckExternalNavigation != null){
      if(item.truckExternalNavigation!.mxPlate != null) return item.truckExternalNavigation!.mxPlate!;
      if(item.truckExternalNavigation!.usaPlate != null) return item.truckExternalNavigation!.usaPlate!;
    }
    return 'Placa no encontrada';
  }

  String getTrailerPlates(YardLog item){
    if(item.trailerNavigation != null){
      if(item.trailerNavigation!.plates.isNotEmpty) return item.trailerNavigation!.plates.first.identifier;
    }else if(item.trailerExternalNavigation != null){
      if(item.trailerExternalNavigation!.mxPlate != null) return item.trailerExternalNavigation!.mxPlate!;
      if(item.trailerExternalNavigation!.usaPlate != null) return item.trailerExternalNavigation!.usaPlate!;
    }
    return 'Placa no encontrada';
  }
 @override
  Widget composeViewer(YardLog set, BuildContext context) {
    Uint8List? truckPicture;
    Uint8List? dmgPicture;
    
    truckPicture = _getBytes(set.ttPicture);
    if(set.dmgEvidence != null){
      dmgPicture = _getBytes(set.dmgEvidence!);
    }
  
    return SizedBox.expand(
      child: SingleChildScrollView(
        child: CSMSpacingColumn(
          spacing: 12,
          crossAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            TWSPropertyViewer(
              label: 'Tipo de registro',
              value: set.entry? 'Entrada' : 'Salida',
            ),
            TWSPropertyViewer(
              label: 'Tipo de carga',
              value: set.loadTypeNavigation!.name,
            ),
            TWSPropertyViewer(
              label: 'Nombre del conductor',
              value: _getDriverName(set)
            ),
            TWSPropertyViewer(
              label: 'Licencia del conductor',
              value: set.driverNavigation != null? set.driverNavigation?.driverCommonNavigation?.license
              : set.driverExternalNavigation != null? set.driverExternalNavigation?.driverCommonNavigation?.license : "License not found.",
            ),
            TWSPropertyViewer(
              label: 'No. Camión',
              value: set.truckNavigation != null? set.truckNavigation?.truckCommonNavigation?.economic
                : set.truckExternalNavigation != null? set.truckExternalNavigation?.truckCommonNavigation?.economic
                : 'Number not found.',
            ),
            TWSPropertyViewer(
              label: 'Placa de Camión',
              value: getTruckPlates(set),
            ),
            TWSPropertyViewer(
              label: 'No. Remolque',
              value: set.trailerNavigation != null? set.trailerNavigation?.trailerCommonNavigation?.economic
                : set.trailerExternalNavigation != null? set.trailerExternalNavigation?.trailerCommonNavigation?.economic
                : 'Numero economico no encontrado.',
            ),
            TWSPropertyViewer(
              label: 'Placa de remolque',
              value: getTrailerPlates(set)
            ),
            TWSPropertyViewer(
              label: 'Numero de sello',
              value: set.seal,
            ),
            TWSPropertyViewer(
              label: 'Desde/Hacia',
              value: set.fromTo,
            ),
            TWSPropertyViewer(
              label: 'Daño',
              value: set.damage? "Dañado" : "Ninguno",
            ),
            TWSPropertyViewer(
              label: 'Section',
              value: "${set.sectionNavigation?.locationNavigation?.name} - ${set.sectionNavigation?.name}",
            ),
        
            //Images sections
        
            //Truck Picture
            if(truckPicture == null)
            const TWSPropertyViewer(
              label: 'Foto del Camión/Remolque',
              value: "Error al cargar la imagen.",
            ),
        
            if(truckPicture != null)
            TWSImageViewer(
              align: TextAlign.left,
              height: 100,
              width: 300,
              title: "Foto del camión/remolque:",
              img: truckPicture
            ),
        
            //dmg Picture
            if(set.dmgEvidence == null)
            const TWSPropertyViewer(
              label: 'Foto del daño',
              value: "N/A",
            ),
        
            if(dmgPicture == null && set.dmgEvidence != null)
            const TWSPropertyViewer(
              label: 'Foto del daño',
              value: "Error al cargar la imagen.",
            ),
        
            if(dmgPicture != null)
            TWSImageViewer(
              align: TextAlign.left,
              height: 100,
              width: 300,
              title: "Foto del daño:",
              img: dmgPicture
            )
          ],
        ),
      ),
    );
  }
}

