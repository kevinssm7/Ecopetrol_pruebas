�
?D:\proyectos\asalud ecopetrol\Facede\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str !
)! "
]" #
[		 
assembly		 	
:			 

AssemblyDescription		 
(		 
$str		 !
)		! "
]		" #
[

 
assembly

 	
:

	 
!
AssemblyConfiguration

  
(

  !
$str

! #
)

# $
]

$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str #
)# $
]$ %
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
[## 
assembly## 	
:##	 

AssemblyVersion## 
(## 
$str## $
)##$ %
]##% &
[$$ 
assembly$$ 	
:$$	 

AssemblyFileVersion$$ 
($$ 
$str$$ (
)$$( )
]$$) *֑
/D:\proyectos\asalud ecopetrol\Facede\IFacade.cs
	namespace		 	
Facede		
 
{

 
public 

	interface 
IFacade 
{ 
void  
ActualizaContraseña  
(  !
sis_usuario! ,

ObjUsuario- 7
,7 8
ref9 <
MessageResponseOBJ= O
MsgResP V
)V W
;W X
List 
< 
sis_usuario 
> 
ValidaIngreso '
(' (
sis_usuario( 3

ObjUsuario4 >
,> ?
ref@ C
MessageResponseOBJD V
MsgResW ]
)] ^
;^ _
List 
< 
ManagmentMenuResult  
>  !
ManagmentMenu" /
(/ 0
String0 6

Strusuario7 A
,A B
refC F
MessageResponseOBJG Y
MsgResZ `
)` a
;a b
List 
< 
sis_usuario 
> 
BuscaAuditorUsu )
() *
String* 0

strUsuario1 ;
,; <
ref= @
MessageResponseOBJA S
MsgResT Z
)Z [
;[ \
List 
< 
sis_usuario 
> 
BuscaAuditorNom )
() *
String* 0
	strNombre1 :
,: ;
ref< ?
MessageResponseOBJ@ R
MsgResS Y
)Y Z
;Z [
void 
GestionUsuarios 
( 
sis_usuario (

ObjUsuario) 3
,3 4
ref5 8
MessageResponseOBJ9 K
MsgResL R
)R S
;S T
void 
CrearUsuairo 
( 
sis_usuario %

ObjUsuario& 0
,0 1
ref2 5
MessageResponseOBJ6 H
MsgResI O
)O P
;P Q
DateTime 
ManagmentHora 
( 
)  
;  !
List%% 
<%% 
vw_datos_censo%% 
>%% 
CensoDocumento%% +
(%%+ ,
String%%, 2
	Documento%%3 <
,%%< =
ref%%> A
MessageResponseOBJ%%B T
MsgRes%%U [
)%%[ \
;%%\ ]
List'' 
<'' 
vw_datos_censo'' 
>'' 
CensoFacturas'' *
(''* +
ref''+ .
MessageResponseOBJ''/ A
MsgRes''B H
)''H I
;''I J
Int32)) 
InsertarCenso)) 
()) 

ecop_censo)) &
OBJ))' *
,))* +
ref)), /
MessageResponseOBJ))0 B
MsgRes))C I
)))I J
;))J K
List++ 
<++ 
vw_datos_censo++ 
>++ 
CensoId++ $
(++$ %
Int32++% *
Id+++ -
,++- .
ref++/ 2
MessageResponseOBJ++3 E
MsgRes++F L
)++L M
;++M N
List-- 
<-- 

ecop_censo-- 
>-- 

GetCensoId-- #
(--# $
Int32--$ )
Id--* ,
,--, -
ref--. 1
MessageResponseOBJ--2 D
MsgRes--E K
)--K L
;--L M
void// 
ActualizarCenso// 
(// 

ecop_censo// '
ActualizaSiniestro//( :
,//: ;
ref//< ?
MessageResponseOBJ//@ R
MsgRes//S Y
)//Y Z
;//Z [
void11 !
ActualizarCensoEgreso11 "
(11" #

ecop_censo11# -
ActualizaSiniestro11. @
,11@ A
ref11B E
MessageResponseOBJ11F X
MsgRes11Y _
)11_ `
;11` a
void33 #
ActualizarCensoEgresoOK33 $
(33$ %

ecop_censo33% /
ActualizaSiniestro330 B
,33B C
ref33D G
MessageResponseOBJ33H Z
MsgRes33[ a
)33a b
;33b c
void55 )
ActualizaEgresoConcurrenciaOk55 *
(55* +
ecop_concurrencia55+ <
ObjConcurrencia55= L
,55L M
ref55N Q
MessageResponseOBJ55R d
MsgRes55e k
)55k l
;55l m
List77 
<77 
base_beneficiarios77 
>77  "
BeneficiariosDocumento77! 7
(777 8
Decimal778 ?
	Documento77@ I
,77I J
ref77K N
MessageResponseOBJ77O a
MsgRes77b h
)77h i
;77i j
List99 
<99 .
"vw_concurrencia_evolucion_Contrato99 /
>99/ 0*
ConsultaIdConcurreniaEvolucion991 O
(99O P.
"vw_concurrencia_evolucion_Contrato99P r
ObjAfiliado99s ~
,99~ 
ref
99� � 
MessageResponseOBJ
99� �
MsgRes
99� �
)
99� �
;
99� �
List;; 
<;; 
vw_consulta_censo;; 
>;; 
ConsultaCenso;;  -
(;;- .
ref;;. 1
MessageResponseOBJ;;2 D
MsgRes;;E K
);;K L
;;;L M
void== 
CensoEgreso== 
(== 

ecop_censo== #
ActualizaSiniestro==$ 6
,==6 7
ref==8 ;
MessageResponseOBJ==< N
MsgRes==O U
)==U V
;==V W
ListDD 
<DD 
Ref_tipo_documentalDD  
>DD  !
GetTipoDocumnetalDD" 3
(DD3 4
)DD4 5
;DD5 6
ListFF 
<FF 
vw_ips_ciudadFF 
>FF 
GetIPSFF "
(FF" #
)FF# $
;FF$ %
ListHH 
<HH 
Ref_ips_cuentasHH 
>HH 
GetPrstadorCuentasHH 0
(HH0 1
)HH1 2
;HH2 3
ListJJ 
<JJ 
Ref_ips_cuentasJJ 
>JJ 
GetPrstadorJJ )
(JJ) *
)JJ* +
;JJ+ ,
ListLL 
<LL 
vw_ciudad_auditorLL 
>LL 
GetCiudad_auditorLL  1
(LL1 2
)LL2 3
;LL3 4
ListNN 
<NN 

vw_auditorNN 
>NN 
Get_auditorNN $
(NN$ %
)NN% &
;NN& '
ListPP 
<PP 
Ref_origen_eventoPP 
>PP 
GetOrigenEventoPP  /
(PP/ 0
)PP0 1
;PP1 2
ListRR 
<RR 
Ref_regionalRR 
>RR 
GetRefRegionRR '
(RR' (
)RR( )
;RR) *
ListTT 
<TT 
Ref_tipo_habitacionTT  
>TT  !
GetTipoHabitacionTT" 3
(TT3 4
)TT4 5
;TT5 6
ListVV 
<VV 
Ref_tipo_ingresoVV 
>VV 
GetTipoIngresoVV -
(VV- .
)VV. /
;VV/ 0
ListXX 
<XX $
Ref_condicion_alta_censoXX %
>XX% &
GetCondicionAltaXX' 7
(XX7 8
)XX8 9
;XX9 :
ListZZ 
<ZZ 
	Ref_cie10ZZ 
>ZZ 
GetCie10ZZ  
(ZZ  !
)ZZ! "
;ZZ" #
List\\ 
<\\ 
Ref_cups\\ 
>\\ 
GetCups\\ 
(\\ 
)\\  
;\\  !
List^^ 
<^^ 
Ref_cuentas_glosa^^ 
>^^ 
GetCuentaGlosa^^  .
(^^. /
)^^/ 0
;^^0 1
List`` 
<`` !
Ref_responsable_glosa`` "
>``" #
GetResGlosa``$ /
(``/ 0
)``0 1
;``1 2
Listbb 
<bb $
Ref_condicion_del_egresobb %
>bb% &
GetCondicionEgresobb' 9
(bb9 :
)bb: ;
;bb; <
Listdd 
<dd !
Ref_servicio_tratantedd "
>dd" #
GetServiciotratantedd$ 7
(dd7 8
)dd8 9
;dd9 :
Listff 
<ff 
Ref_salud_publicaff 
>ff 
GetSaludPublicaff  /
(ff/ 0
)ff0 1
;ff1 2
Listhh 
<hh -
!Ref_lesiones_severas_y_alto_costohh .
>hh. /
GetAltoCostohh0 <
(hh< =
)hh= >
;hh> ?
Listjj 
<jj 
vw_tablero_censojj 
>jj 
GetTableroCensojj .
(jj. /
)jj/ 0
;jj0 1
Listll 
<ll #
vw_tablero_concurrenciall $
>ll$ %"
GetTableroConcurrenciall& <
(ll< =
)ll= >
;ll> ?
Listnn 
<nn 
Ref_ciudadesnn 
>nn 
GetCiudadesnn &
(nn& '
)nn' (
;nn( )
Listpp 
<pp 
vw_cie10pp 
>pp 
GetCie10Unidopp $
(pp$ %
)pp% &
;pp& '
Listrr 
<rr 
Ref_causal_egresorr 
>rr 
GetCausaEgresorr  .
(rr. /
)rr/ 0
;rr0 1
Listtt 
<tt 
vw_consulta_alertastt  
>tt  !
GetConsultaAlertastt" 4
(tt4 5
)tt5 6
;tt6 7
Listvv 
<vv 
Total_ciudadesvv 
>vv 
GetTotalCiudadesvv -
(vv- .
)vv. /
;vv/ 0
Listxx 
<xx %
Ref_motivo_devolucion_facxx &
>xx& '"
GetMotivoDevolucionFacxx( >
(xx> ?
)xx? @
;xx@ A
Int32zz &
InsertarDevolucionFacturaszz (
(zz( )
factura_devolucionzz) ;
OBJzz< ?
,zz? @
refzzA D
MessageResponseOBJzzE W
MsgReszzX ^
)zz^ _
;zz_ `
Int32|| -
!InsertarDevolucionFacturasDetalle|| /
(||/ 0&
factura_devolucion_detalle||0 J
OBJ||K N
,||N O
ref||P S
MessageResponseOBJ||T f
MsgRes||g m
)||m n
;||n o
List~~ 
<~~ !
vw_sis_auditor_ciudad~~ "
>~~" #
GetCiudadesAuditor~~$ 6
(~~6 7
)~~7 8
;~~8 9
List
�� 
<
��  
Ref_hallazgos_RIPS
�� 
>
��  
GetRefHallazgos
��! 0
(
��0 1
)
��1 2
;
��2 3
List
�� 
<
�� (
Ref_categorias_eventos_adv
�� '
>
��' (!
GetRefCategoriaEvad
��) <
(
��< =
)
��= >
;
��> ?
List
�� 
<
�� "
sis_auditor_regional
�� !
>
��! " 
GetRegionalAuditor
��# 5
(
��5 6
)
��6 7
;
��7 8
List
�� 
<
�� "
Ref_motivo_reingreso
�� !
>
��! " 
GetRefMotivoRiesgo
��# 5
(
��5 6
)
��6 7
;
��7 8
List
�� 
<
�� 3
%Ref_categorias_situaciones_de_calidad
�� 2
>
��2 3&
GetRefCategoriaSituacion
��4 L
(
��L M
)
��M N
;
��N O
List
�� 
<
�� 
vw_cie10_alertas
�� 
>
��  
GetRefcie10Alertas
�� 1
(
��1 2
)
��2 3
;
��3 4
List
�� 
<
�� (
Ref_enfermedades_Huerfanas
�� '
>
��' (
GetRefHuerfanas
��) 8
(
��8 9
)
��9 :
;
��: ;
List
�� 
<
�� 
Ref_tipo_ahorro
�� 
>
�� 
GetRefTipoAhorro
�� .
(
��. /
)
��/ 0
;
��0 1
void
�� #
ActualizaConcurrencia
�� "
(
��" #
ecop_concurrencia
��# 4
ObjConcurrencia
��5 D
,
��D E
String
��F L
User
��M Q
,
��Q R
String
��S Y
	IPAddress
��Z c
,
��c d
ref
��e h 
MessageResponseOBJ
��i {
MsgRes��| �
)��� �
;��� �
void
�� )
ActualizaEgresoConcurrencia
�� (
(
��( )
ecop_concurrencia
��) :
ObjConcurrencia
��; J
,
��J K
String
��L R
User
��S W
,
��W X
String
��Y _
	IPAddress
��` i
,
��i j
ref
��k n!
MessageResponseOBJ��o �
MsgRes��� �
)��� �
;��� �
List
�� 
<
�� 
ecop_concurrencia
�� 
>
�� 0
"ConsultaCriterioIngresoActualizado
��  B
(
��B C
Int32
��C H
IdConcu
��I P
,
��P Q
ref
��R U 
MessageResponseOBJ
��V h
MsgRes
��i o
)
��o p
;
��p q
List
�� 
<
�� 4
&ecop_concurrencia_encuesta_satisfacion
�� 3
>
��3 4%
ConsultaEncuestaCargada
��5 L
(
��L M
Int32
��M R
IdConcu
��S Z
,
��Z [
ref
��\ _ 
MessageResponseOBJ
��` r
MsgRes
��s y
)
��y z
;
��z {
void
�� 
InsertaEgreso
�� 
(
�� +
egreso_auditoria_Hospitalaria
�� 8
Egreso
��9 ?
,
��? @
String
��A G
UserName
��H P
,
��P Q
String
��R X
	IPAddress
��Y b
,
��b c
ref
��d g 
MessageResponseOBJ
��h z
MsgRes��{ �
)��� �
;��� �
List
�� 
<
�� 
vw_ciudad_ips
�� 
>
�� )
ConsultaIdConcurreniaciudad
�� 7
(
��7 8
vw_ciudad_ips
��8 E
ObjAfiliado
��F Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
;
��q r
List
�� 
<
�� &
vw_consulta_concurrencia
�� %
>
��% &"
ConsultaConcurrencia
��' ;
(
��; <
ref
��< ? 
MessageResponseOBJ
��@ R
MsgRes
��S Y
)
��Y Z
;
��Z [
List
�� 
<
��  
vw_consulta_egreso
�� 
>
��  
ConsultaEgreso
��! /
(
��/ 0
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
List
�� 
<
�� *
vw_consulta_eventos_adversos
�� )
>
��) *
ConsultaEventosAd
��+ <
(
��< =
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
;
��[ \
List
�� 
<
�� +
vw_consulta_situacion_calidad
�� *
>
��* +"
ConsultaSituacionCal
��, @
(
��@ A
ref
��A D 
MessageResponseOBJ
��E W
MsgRes
��X ^
)
��^ _
;
��_ `
List
�� 
<
�� 
vw_gestantes
�� 
>
�� 
ConsultaGestantes
�� ,
(
��, -
ref
��- 0 
MessageResponseOBJ
��1 C
MsgRes
��D J
)
��J K
;
��K L
List
�� 
<
�� 
vw_gestantes_sin
�� 
>
�� "
ConsultaGestantesSin
�� 3
(
��3 4
ref
��4 7 
MessageResponseOBJ
��8 J
MsgRes
��K Q
)
��Q R
;
��R S
List
�� 
<
�� 
vw_Mortalidad
�� 
>
��  
ConsultaMortalidad
�� .
(
��. /
ref
��/ 2 
MessageResponseOBJ
��3 E
MsgRes
��F L
)
��L M
;
��M N
List
�� 
<
�� 
vw_Mortalidad_sin
�� 
>
�� #
ConsultaMortalidadSin
��  5
(
��5 6
ref
��6 9 
MessageResponseOBJ
��: L
MsgRes
��M S
)
��S T
;
��T U
List
�� 
<
�� &
vw_tipo_habitacion_censo
�� %
>
��% &$
ConsultaTipoHabitacion
��' =
(
��= >&
vw_tipo_habitacion_censo
��> V
ObjAfiliado
��W b
,
��b c
ref
��d g 
MessageResponseOBJ
��h z
MsgRes��{ �
)��� �
;��� �
void
�� *
InsertarEncuestaConcurrencia
�� )
(
��) *(
ecop_concurrencia_encuesta
��* D
Encuesta
��E M
,
��M N
ref
��O R 
MessageResponseOBJ
��S e
MsgRes
��f l
)
��l m
;
��m n
void
�� *
InsertaConcurrenciaEvolucion
�� )
(
��) *)
ecop_concurrencia_evolucion
��* E
	Evolucion
��F O
,
��O P
String
��Q W
UserName
��X `
,
��` a
String
��b h
	IPAddress
��i r
,
��r s
ref
��t w!
MessageResponseOBJ��x �
MsgRes��� �
)��� �
;��� �
List
�� 
<
�� )
ecop_concurrencia_evolucion
�� (
>
��( )!
ConsultaEvoluciones
��* =
(
��= >)
ecop_concurrencia_evolucion
��> Y
ObjEvolu
��Z b
,
��b c
ref
��d g 
MessageResponseOBJ
��h z
MsgRes��{ �
)��� �
;��� �
void
�� +
EliminarConcurrenciaEvolucion
�� *
(
��* +)
ecop_concurrencia_evolucion
��+ F
ObjEvolucion
��G S
,
��S T
String
��U [
UserName
��\ d
,
��d e
String
��f l
	IPAddress
��m v
,
��v w
ref
��x {!
MessageResponseOBJ��| �
MsgRes��� �
)��� �
;��� �
List
�� 
<
�� 2
$ecop_concurrencia_evolucion_diag_def
�� 1
>
��1 2+
ConsultaDiagnosticoDefinitivo
��3 P
(
��P Q2
$ecop_concurrencia_evolucion_diag_def
��Q u

Objdiagdef��v �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
;��� �
void
�� *
InsertaDiagnosticoDefinitivo
�� )
(
��) *2
$ecop_concurrencia_evolucion_diag_def
��* N4
&Concurrencia_Diagnostico_Definitivo_id
��O u
,
��u v
String
��w }
UserName��~ �
,��� �
String��� �
	IPAddress��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
;��� �
void
�� 
InsertaGlosa
�� 
(
�� %
ecop_concurrencia_glosa
�� 1
ObjGlosa
��2 :
,
��: ;
String
��< B
UserName
��C K
,
��K L
String
��M S
	IPAddress
��T ]
,
��] ^
ref
��_ b 
MessageResponseOBJ
��c u
MsgRes
��v |
)
��| }
;
��} ~
List
�� 
<
�� %
ecop_concurrencia_glosa
�� $
>
��$ %
ConsultaGlosa
��& 3
(
��3 4%
ecop_concurrencia_glosa
��4 K
ObjGlosa
��L T
,
��T U
ref
��V Y 
MessageResponseOBJ
��Z l
MsgRes
��m s
)
��s t
;
��t u
List
�� 
<
�� "
Ref_eventos_adversos
�� !
>
��! " 
GetEventosAdversos
��# 5
(
��5 6
)
��6 7
;
��7 8
List
�� 
<
�� 
Ref_grado_lesion
�� 
>
�� 
GetGradoLesion
�� -
(
��- .
)
��. /
;
��/ 0
List
�� 
<
�� )
Ref_factores_contribuyentes
�� (
>
��( )'
GetFactoresContribuyentes
��* C
(
��C D
)
��D E
;
��E F
List
�� 
<
�� $
Ref_barreras_seguridad
�� #
>
��# $$
GetBarrerasDeSeguridad
��% ;
(
��; <
)
��< =
;
��= >
List
�� 
<
�� $
Ref_acciones_inseguras
�� #
>
��# $"
GetAccionesInseguras
��% 9
(
��9 :
)
��: ;
;
��; <
List
�� 
<
��  
Ref_plan_de_manejo
�� 
>
��  
GetPlanDeManejo
��! 0
(
��0 1
)
��1 2
;
��2 3
void
�� "
InsertaEventoAdverso
�� !
(
��! "0
"ecop_concurrencia_eventos_adversos
��" D
ObjEventoAdv
��E Q
,
��Q R
String
��S Y
UserName
��Z b
,
��b c
String
��d j
	IPAddress
��k t
,
��t u
ref
��v y!
MessageResponseOBJ��z �
MsgRes��� �
)��� �
;��� �
List
�� 
<
�� 0
"ecop_concurrencia_eventos_adversos
�� /
>
��/ 0#
ConsultaEventoAdverso
��1 F
(
��F G0
"ecop_concurrencia_eventos_adversos
��G i
ObjEventoAdverso
��j z
,
��z {
ref
��| "
MessageResponseOBJ��� �
MsgRes��� �
)��� �
;��� �
List
�� 
<
�� (
Ref_situaciones_de_calidad
�� '
>
��' (%
GetSituacionesDeCalidad
��) @
(
��@ A
)
��A B
;
��B C
void
�� '
InsertaSituacionesCalidad
�� &
(
��& '6
(ecop_concurrencia_situaciones_de_calidad
��' O
ObjSituacionCalid
��P a
,
��a b
String
��c i
UserName
��j r
,
��r s
String
��t z
	IPAddress��{ �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
;��� �
List
�� 
<
�� 6
(ecop_concurrencia_situaciones_de_calidad
�� 5
>
��5 6(
ConsultaSituacionesCalidad
��7 Q
(
��Q R6
(ecop_concurrencia_situaciones_de_calidad
��R z
ObjSituCali��{ �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
;��� �
List
�� 
<
�� 2
$Ref_motivo_cancelacion_procedimiento
�� 1
>
��1 2"
GetMotivoCancelacion
��3 G
(
��G H
)
��H I
;
��I J
void
�� 4
&InsertaProcedimientoQuirugicoCancelado
�� 3
(
��3 4E
7ecop_concurrencia_procedimientos_quirurgicos_cancelados
��4 k%
ProcedimientoQuirCance��l �
,��� �
String��� �
UserName��� �
,��� �
String��� �
	IPAddress��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
;��� �
List
�� 
<
�� E
7ecop_concurrencia_procedimientos_quirurgicos_cancelados
�� D
>
��D E*
ConsultaProcQuirurgicosCance
��F b
(
��b cF
7ecop_concurrencia_procedimientos_quirurgicos_cancelados��c �
ObjProcQuir��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
;��� �
void
�� 
InsertarNatalidad
�� 
(
�� (
natalidad_sin_concurrencia
�� 9
	Natalidad
��: C
,
��C D
ref
��E H 
MessageResponseOBJ
��I [
MsgRes
��\ b
)
��b c
;
��c d
void
��  
InsertarMortalidad
�� 
(
��  )
mortalidad_sin_concurrencia
��  ;

Mortalidad
��< F
,
��F G
ref
��H K 
MessageResponseOBJ
��L ^
MsgRes
��_ e
)
��e f
;
��f g
List
�� 
<
�� )
vw_tablero_eventos_adversos
�� (
>
��( )#
ReportesEventoAdverso
��* ?
(
��? @
)
��@ A
;
��A B
void
�� )
InsertarAlertasConcurrencia
�� (
(
��( ),
alertas_generadas_concurrencia
��) G
Alertas
��H O
,
��O P
ref
��Q T 
MessageResponseOBJ
��U g
MsgRes
��h n
)
��n o
;
��o p
void
�� (
InsertarConcurrenciaAhorro
�� '
(
��' (&
ecop_concurrencia_ahorro
��( @
Ahorro
��A G
,
��G H
ref
��I L 
MessageResponseOBJ
��M _
MsgRes
��` f
)
��f g
;
��g h
List
�� 
<
�� &
ecop_concurrencia_ahorro
�� %
>
��% &
ConsultaAhorro
��' 5
(
��5 6&
ecop_concurrencia_ahorro
��6 N
	ObjAhorro
��O X
,
��X Y
ref
��Z ] 
MessageResponseOBJ
��^ p
MsgRes
��q w
)
��w x
;
��x y
List
�� 
<
�� 
Ref_causal_glosa
�� 
>
�� !
ConsultaCausalGlosa
�� 2
(
��2 3
int
��3 6!
id_respnsable_glosa
��7 J
,
��J K
ref
��L O 
MessageResponseOBJ
��P b
MsgRes
��c i
)
��i j
;
��j k
List
�� 
<
�� +
ManagmentAlertasCalidadResult
�� *
>
��* +
CuentaFechaCargue
��, =
(
��= >
Int32
��> C
Opc
��D G
,
��G H
DateTime
��I Q
FechaInicial
��R ^
,
��^ _
DateTime
��` h
FechaFin
��i q
,
��q r
String
��s y
strProveedor��z �
,��� �
String��� �
	strEstado��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
;��� �
List
�� 
<
�� 0
"ManagmentReportDevolucionFacResult
�� /
>
��/ 0*
ConsultaReporteDevolucionFac
��1 M
(
��M N
Int32
��N S#
id_devolucion_factura
��T i
)
��i j
;
��j k
List
�� 
<
�� +
vw_Devoluciones_sin_gestionar
�� *
>
��* +$
DevolucionesSinGestion
��, B
(
��B C
)
��C D
;
��D E
Int32
�� +
InsertarDevolucionGestionadas
�� +
(
��+ ,,
factura_devolucion_gestionadas
��, J
OBJ
��K N
,
��N O
ref
��P S 
MessageResponseOBJ
��T f
MsgRes
��g m
)
��m n
;
��n o
Int32
�� %
InsertarFacturaSinCenso
�� %
(
��% &
factura_sin_censo
��& 7
OBJ
��8 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
;
��[ \
List
�� 
<
�� 
vw_hallazgos_RIPS
�� 
>
�� %
HallazgosRipsSinGestion
��  7
(
��7 8
)
��8 9
;
��9 :
List
�� 
<
�� %
vw_facturas_sin_auditar
�� $
>
��$ % 
FacturasporAuditar
��& 8
(
��8 9
)
��9 :
;
��: ;
List
�� 
<
�� 
vw_costo_evitado
�� 
>
�� 
CostoEvitado
�� +
(
��+ ,
Int32
��, 1
Id
��2 4
,
��4 5
ref
��6 9 
MessageResponseOBJ
��: L
MsgRes
��M S
)
��S T
;
��T U
List
�� 
<
�� &
vw_facturas_diagnosticos
�� %
>
��% &!
DiagnosticosCuentas
��' :
(
��: ;
Int32
��; @
Id
��A C
,
��C D
ref
��E H 
MessageResponseOBJ
��I [
MsgRes
��\ b
)
��b c
;
��c d
List
�� 
<
�� )
vw_ECOPETROL_DEVOLUCION_FAC
�� (
>
��( )
VwDevoluciones
��* 8
(
��8 9
)
��9 :
;
��: ;
List
�� 
<
�� )
vw_ECOPETROL_HALLAZGOS_RIPS
�� (
>
��( )
VwHallazgosRIPS
��* 9
(
��9 :
)
��: ;
;
��; <
List
�� 
<
�� *
ECOPETROL_RECEPCION_FACTURAS
�� )
>
��) *!
VwRecepcionFacturas
��+ >
(
��> ?
)
��? @
;
��@ A
Int32
�� 
InsertarHallazgos
�� 
(
��  
hallazgo_RIPS
��  -
OBJ
��. 1
,
��1 2
ref
��3 6 
MessageResponseOBJ
��7 I
MsgRes
��J P
)
��P Q
;
��Q R
Int32
�� &
InsertarHallazgosDetalle
�� &
(
��& '#
hallazgo_RIPS_detalle
��' <
OBJ
��= @
,
��@ A
ref
��B E 
MessageResponseOBJ
��F X
MsgRes
��Y _
)
��_ `
;
��` a
List
�� 
<
�� /
!ManagmentReportHallazgosRipResult
�� .
>
��. /*
ConsultaReporteHallazgosRips
��0 L
(
��L M
Int32
��M R
id_hallazgo_RIPS
��S c
)
��c d
;
��d e
void
�� $
ActualizaHallazgosRips
�� #
(
��# $
hallazgo_RIPS
��$ 1
Objrips
��2 9
,
��9 :
ref
��; > 
MessageResponseOBJ
��? Q
MsgRes
��R X
)
��X Y
;
��Y Z
List
�� 
<
�� 
factura_sin_censo
�� 
>
�� %
ConsultaFacturasSinAudi
��  7
(
��7 8
Int32
��8 ="
id_factura_sin_censo
��> R
)
��R S
;
��S T
Int32
�� "
InsertarCostoEvitado
�� "
(
��" #+
factura_sin_censo_cos_evitado
��# @
Obj
��A D
,
��D E
ref
��F I 
MessageResponseOBJ
��J \
MsgRes
��] c
)
��c d
;
��d e
Int32
�� (
InsertarDiagnosticoCuentas
�� (
(
��( ),
factura_sin_censo_diagnosticos
��) G
Obj
��H K
,
��K L
ref
��M P 
MessageResponseOBJ
��Q c
MsgRes
��d j
)
��j k
;
��k l
void
�� &
ActualizaFacturaAuditada
�� %
(
��% &
factura_sin_censo
��& 7
ObjAudi
��8 ?
,
��? @
ref
��A D 
MessageResponseOBJ
��E W
MsgRes
��X ^
)
��^ _
;
��_ `
List
�� 
<
��  
factura_devolucion
�� 
>
��  )
ConsultaDevolucionesFactura
��! <
(
��< =
String
��= C
Numero_factura
��D R
)
��R S
;
��S T
List
�� 
<
�� 
factura_sin_censo
�� 
>
�� #
ConsultaFacturaNumero
��  5
(
��5 6
String
��6 <
Numero_factura
��= K
)
��K L
;
��L M
List
�� 
<
��  
factura_devolucion
�� 
>
��  +
ConsultaDevolucionesFacturaId
��! >
(
��> ?
Int32
��? D
Id_devolucion
��E R
)
��R S
;
��S T
List
�� 
<
�� 
hallazgo_RIPS
�� 
>
�� !
ConsultaHallazgosId
�� /
(
��/ 0
Int32
��0 5
Id_rips
��6 =
)
��= >
;
��> ?
Int32
�� 
InsertarRips
�� 
(
�� 
RIPS
�� 
Objrips
��  '
,
��' (
ref
��* - 
MessageResponseOBJ
��. @
MsgRes
��A G
)
��G H
;
��H I
List
�� 
<
�� 
RIPS
�� 
>
�� 
ConsultaRips
�� 
(
��  
Int32
��  %
IdRips
��& ,
,
��, -
ref
��. 1 
MessageResponseOBJ
��2 D
MsgRes
��E K
)
��K L
;
��L M
bool
�� 
ActualizaRips
�� 
(
�� 
RIPS
�� 
ObjRips
��  '
,
��' (
ref
��) , 
MessageResponseOBJ
��- ?
MsgRes
��@ F
)
��F G
;
��G H
Int32
�� 
InsertarRipsAC
�� 
(
�� 
RIPS_AC
�� $
	ObjripsAc
��% .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
List
�� 
<
�� 
RIPS_AC
�� 
>
�� 
ConsultaRipsAC
�� $
(
��$ %
Int32
��% *
IdRips
��+ 1
,
��1 2
ref
��3 6 
MessageResponseOBJ
��7 I
MsgRes
��J P
)
��P Q
;
��Q R
Int32
�� 
InsertarRipsAD
�� 
(
�� 
List
�� !
<
��! "
RIPS_AD
��" )
>
��) *
	ObjripsAD
��+ 4
,
��4 5
ref
��6 9 
MessageResponseOBJ
��: L
MsgRes
��M S
)
��S T
;
��T U
Int32
�� 
InsertarRipsAF
�� 
(
�� 
RIPS_AF
�� $
	ObjripsAF
��% .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
Int32
�� 
InsertarRipsAH
�� 
(
�� 
RIPS_AH
�� $
	ObjripsAH
��% .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
Int32
�� 
InsertarRipsAM
�� 
(
�� 
RIPS_AM
�� $
	ObjripsAM
��% .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
Int32
�� 
InsertarRipsAN
�� 
(
�� 
RIPS_AN
�� $
	ObjripsAN
��% .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
Int32
�� 
InsertarRipsAP
�� 
(
�� 
RIPS_AP
�� $
	ObjripsAP
��% .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
Int32
�� 
InsertarRipsAT
�� 
(
�� 
RIPS_AT
�� $
	ObjripsAT
��% .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
Int32
�� 
InsertarRipsAU
�� 
(
�� 
RIPS_AU
�� $
	ObjripsAU
��% .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
Int32
�� 
InsertarRipsCT
�� 
(
�� 
RIPS_CT
�� $
	ObjripsCT
��% .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
Int32
�� 
InsertarRipsUS
�� 
(
�� 
RIPS_US
�� $
	ObjripsUS
��% .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
;
��N O
List
�� 
<
�� "
Ref_RIPS_Prestadores
�� !
>
��! "!
ConsultaPrestadores
��# 6
(
��6 7
string
��7 =
codhabilitacion
��> M
,
��N O
ref
��P S 
MessageResponseOBJ
��T f
MsgRes
��g m
)
��m n
;
��n o
}
�� 
}�� ��4
.D:\proyectos\asalud ecopetrol\Facede\Facade.cs
	namespace 	
Facede
 
{ 
public 

class 
Facade 
{ 
private 
ConsultasDac 
_DACConsulta )
;) *
public 
ConsultasDac 
DACConsulta '
{ 	
get 
{ 
if 
( 
_DACConsulta  
!=! #
null$ (
)( )
{ 
return 
_DACConsulta '
;' (
} 
else 
{ 
return 
_DACConsulta '
=( )
new* -
ConsultasDac. :
(: ;
); <
;< =
} 
}!! 
set"" 
{"" 
_DACConsulta"" 
=""  
value""! &
;""& '
}""( )
}## 	
private%% 
ActualizarDac%% 
_DACActualiza%% +
;%%+ ,
public&& 
ActualizarDac&& 
DACActualiza&& )
{'' 	
get(( 
{)) 
if** 
(** 
_DACActualiza** !
!=**" $
null**% )
)**) *
{++ 
return,, 
_DACActualiza,, (
;,,( )
}-- 
else.. 
{// 
return00 
_DACActualiza00 (
=00) *
new00+ .
ActualizarDac00/ <
(00< =
)00= >
;00> ?
}11 
}33 
set44 
{44 
_DACActualiza44 
=44  !
value44" '
;44' (
}44) *
}55 	
private77 

ComonClass77 
_DACComonClass77 )
;77) *
public88 

ComonClass88 
DACComonClass88 '
{99 	
get:: 
{;; 
if<< 
(<< 
_DACComonClass<< "
!=<<# %
null<<& *
)<<* +
{== 
return>> 
_DACComonClass>> )
;>>) *
}?? 
else@@ 
{AA 
returnBB 
_DACComonClassBB )
=BB* +
newBB, /

ComonClassBB0 :
(BB: ;
)BB; <
;BB< =
}CC 
}EE 
setFF 
{FF 
_DACComonClassFF  
=FF! "
valueFF# (
;FF( )
}FF* +
}GG 	
privateII 

InsertaDacII 
_DACInsertaII &
;II& '
publicJJ 

InsertaDacJJ 

DACInsertaJJ $
{KK 	
getLL 
{MM 
ifNN 
(NN 
_DACInsertaNN 
!=NN  "
nullNN# '
)NN' (
{OO 
returnPP 
_DACInsertaPP &
;PP& '
}QQ 
elseRR 
{SS 
returnTT 
_DACInsertaTT &
=TT' (
newTT) ,

InsertaDacTT- 7
(TT7 8
)TT8 9
;TT9 :
}UU 
}WW 
setXX 
{XX 
_DACInsertaXX 
=XX 
valueXX  %
;XX% &
}XX' (
}YY 	
private[[ 

EliminaDac[[ 
_DACElimina[[ &
;[[& '
public\\ 

EliminaDac\\ 

DACElimina\\ $
{]] 	
get^^ 
{__ 
if`` 
(`` 
_DACElimina`` 
!=``  "
null``# '
)``' (
{aa 
returnbb 
_DACEliminabb &
;bb& '
}cc 
elsedd 
{ee 
returnff 
_DACEliminaff &
=ff' (
newff) ,

EliminaDacff- 7
(ff7 8
)ff8 9
;ff9 :
}gg 
}ii 
setjj 
{jj 
_DACEliminajj 
=jj 
valuejj  %
;jj% &
}jj' (
}kk 	
publicpp 
Listpp 
<pp 
sis_usuariopp 
>pp  
GetSisUsuariopp! .
(pp. /
)pp/ 0
{qq 	
returnrr 
DACComonClassrr  
.rr  !
GetSisUsuariorr! .
(rr. /
)rr/ 0
;rr0 1
}ss 	
publicuu 
Listuu 
<uu 
sis_usuariouu 
>uu  
GetSisUsuarioactivouu! 4
(uu4 5
)uu5 6
{vv 	
returnww 
DACComonClassww  
.ww  !
GetSisUsuarioactivoww! 4
(ww4 5
)ww5 6
;ww6 7
}xx 	
publiczz 
Listzz 
<zz 
sis_usuariozz 
>zz  
GetSisUsuarioMdzz! 0
(zz0 1
)zz1 2
{{{ 	
return|| 
DACComonClass||  
.||  !
GetSisUsuarioMd||! 0
(||0 1
)||1 2
;||2 3
}}} 	
public 
List 
< 
sis_usuario 
>  
GetSisUsuarioOdont! 3
(3 4
)4 5
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetSisUsuarioOdont
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
void
�� $
ActualizaCodigoIngreso
�� *
(
��* +
string
��+ 1
usuario
��2 9
,
��9 :
string
��; A
codigo
��B H
,
��H I
ref
��J M 
MessageResponseOBJ
��N `
MsgRes
��a g
)
��g h
{
�� 	
DACActualiza
�� 
.
�� $
ActualizaCodigoIngreso
�� /
(
��/ 0
usuario
��0 7
,
��7 8
codigo
��9 ?
,
��? @
ref
��A D
MsgRes
��E K
)
��K L
;
��L M
}
�� 	
public
�� 
List
�� 
<
�� !
Ref_tipo_documental
�� '
>
��' (
GetTipoDocumnetal
��) :
(
��: ;
)
��; <
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetTipoDocumnetal
��! 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� 
vw_ips_ciudad
�� !
>
��! "
GetIPS
��# )
(
��) *
)
��* +
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetIPS
��! '
(
��' (
)
��( )
;
��) *
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ips
�� 
>
�� 
GetPrstador
�� (
(
��( )
)
��) *
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetPrstador
��! ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� 5
'management_censo_tableroDetalladoResult
�� ;
>
��; <
GetCensoDetallado
��= N
(
��N O
DateTime
��O W
?
��W X
fechaInicio
��Y d
,
��d e
DateTime
��f n
?
��n o
fechaFin
��p x
,
��x y
string��z �
	documento��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetCensoDetallado
�� 0
(
��0 1
fechaInicio
��1 <
,
��< =
fechaFin
��> F
,
��F G
	documento
��H Q
)
��Q R
;
��R S
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ips_cuentas
�� #
>
��# $ 
GetPrstadorCuentas
��% 7
(
��7 8
)
��8 9
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetPrstadorCuentas
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� 
vw_ciudad_auditor
�� %
>
��% &
GetCiudad_auditor
��' 8
(
��8 9
)
��9 :
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetCiudad_auditor
��! 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� 

vw_auditor
�� 
>
�� 
Get_auditor
��  +
(
��+ ,
)
��, -
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
Get_auditor
��! ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_origen_evento
�� %
>
��% &
GetOrigenEvento
��' 6
(
��6 7
)
��7 8
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetOrigenEvento
��! 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_regional
��  
>
��  !
GetRefRegion
��" .
(
��. /
)
��/ 0
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetRefRegion
��! -
(
��- .
)
��. /
;
��/ 0
}
�� 	
public
�� 
Ref_regional
�� 
GetRefRegionId
�� *
(
��* +
int
��+ .
id
��/ 1
)
��1 2
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetRefRegionId
��! /
(
��/ 0
id
��0 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� !
Ref_tipo_habitacion
�� '
>
��' (
GetTipoHabitacion
��) :
(
��: ;
)
��; <
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetTipoHabitacion
��! 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_tipo_ingreso
�� $
>
��$ %
GetTipoIngreso
��& 4
(
��4 5
)
��5 6
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetTipoIngreso
��! /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� &
Ref_condicion_alta_censo
�� ,
>
��, -
GetCondicionAlta
��. >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetCondicionAlta
��! 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� 
	Ref_cie10
�� 
>
�� 
GetCie10
�� '
(
��' (
)
��( )
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetCie10
��! )
(
��) *
)
��* +
;
��+ ,
}
�� 	
public
�� 
List
�� 
<
�� 
vw_ref_cups
�� 
>
��  
GetCups
��! (
(
��( )
)
��) *
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetCups
��! (
(
��( )
)
��) *
;
��* +
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_cuentas_glosa
�� %
>
��% &
GetCuentaGlosa
��' 5
(
��5 6
)
��6 7
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetCuentaGlosa
��! /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_causal_glosa
�� $
>
��$ %
GetCausalGlosa
��& 4
(
��4 5
)
��5 6
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetCausalGlosa
��! /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� #
Ref_responsable_glosa
�� )
>
��) *
GetResGlosa
��+ 6
(
��6 7
)
��7 8
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetResGlosa
��! ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� &
Ref_condicion_del_egreso
�� ,
>
��, - 
GetCondicionEgreso
��. @
(
��@ A
)
��A B
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetCondicionEgreso
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� #
Ref_servicio_tratante
�� )
>
��) *!
GetServiciotratante
��+ >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
GetServiciotratante
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_salud_publica
�� %
>
��% &
GetSaludPublica
��' 6
(
��6 7
)
��7 8
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetSaludPublica
��! 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
List
�� 
<
�� /
!Ref_lesiones_severas_y_alto_costo
�� 5
>
��5 6
GetAltoCosto
��7 C
(
��C D
)
��D E
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetAltoCosto
��! -
(
��- .
)
��. /
;
��/ 0
}
�� 	
public
�� 
List
�� 
<
�� 
vw_tablero_censo
�� $
>
��$ %
GetTableroCenso
��& 5
(
��5 6
)
��6 7
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetTableroCenso
��! 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
List
�� 
<
�� /
!management_vw_tablero_censoResult
�� 5
>
��5 6%
GetTableroCensoCompleto
��7 N
(
��N O
)
��O P
{
�� 	
return
�� 
DACComonClass
��  
.
��  !%
GetTableroCensoCompleto
��! 8
(
��8 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
List
�� 
<
�� 
vw_tablero_censo2
�� %
>
��% &
GetTableroCenso2
��' 7
(
��7 8
)
��8 9
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetTableroCenso2
��! 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� %
vw_tablero_concurrencia
�� +
>
��+ ,$
GetTableroConcurrencia
��- C
(
��C D
)
��D E
{
�� 	
return
�� 
DACComonClass
��  
.
��  !$
GetTableroConcurrencia
��! 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� 1
#management_egresosEvolucionesResult
�� 7
>
��7 8
ConsultaEgresoId
��9 I
(
��I J
int
��J M
idEgreso
��N V
)
��V W
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaEgresoId
�� /
(
��/ 0
idEgreso
��0 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� 3
%management_concurrencia_alertasResult
�� 9
>
��9 :2
$ConsultaConcurrenciaAlertasEvolucion
��; _
(
��_ `
)
��` a
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 2
$ConsultaConcurrenciaAlertasEvolucion
�� C
(
��C D
)
��D E
;
��E F
}
�� 	
public
�� 
List
�� 
<
�� :
,management_concurrencia_alerta_ReporteResult
�� @
>
��@ A9
+ConsultaConcurrenciaAlertasEvolucionReporte
��B m
(
��m n
)
��n o
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 9
+ConsultaConcurrenciaAlertasEvolucionReporte
�� J
(
��J K
)
��K L
;
��L M
}
�� 	
public
�� 
int
�� 8
*ConsultaConcurrenciaAlertasEvolucionConteo
�� =
(
��= >
)
��> ?
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 8
*ConsultaConcurrenciaAlertasEvolucionConteo
�� I
(
��I J
)
��J K
;
��K L
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ciudades
��  
>
��  !
GetCiudades
��" -
(
��- .
)
��. /
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetCiudades
��! ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_odont_unis
�� "
>
��" #
unisRegional
��$ 0
(
��0 1
int
��1 4
?
��4 5

idRegional
��6 @
)
��@ A
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
unisRegional
�� +
(
��+ ,

idRegional
��, 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ciudades
��  
>
��  !"
GetCiudadesXRegional
��" 6
(
��6 7
int
��7 :
?
��: ;

idRegional
��< F
)
��F G
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
GetCiudadesXRegional
��! 5
(
��5 6

idRegional
��6 @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ciudades
��  
>
��  !
GetCiudadesXUnis
��" 2
(
��2 3
int
��3 6
?
��6 7
idUnis
��8 >
)
��> ?
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetCiudadesXUnis
��! 1
(
��1 2
idUnis
��2 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� 
vw_cie10
�� 
>
�� 
GetCie10Unido
�� +
(
��+ ,
)
��, -
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetCie10Unido
��! .
(
��. /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
List
�� 
<
�� 
vw_cie10
�� 
>
�� "
GetCie10UnidoDetalle
�� 2
(
��2 3
)
��3 4
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
GetCie10UnidoDetalle
��! 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_causal_egreso
�� %
>
��% &
GetCausaEgreso
��' 5
(
��5 6
)
��6 7
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetCausaEgreso
��! /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� !
vw_consulta_alertas
�� '
>
��' ( 
GetConsultaAlertas
��) ;
(
��; <
)
��< =
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetConsultaAlertas
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� 
Total_ciudades
�� "
>
��" #
GetTotalCiudades
��$ 4
(
��4 5
)
��5 6
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetTotalCiudades
��! 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� '
Ref_motivo_devolucion_fac
�� -
>
��- .$
GetMotivoDevolucionFac
��/ E
(
��E F
)
��F G
{
�� 	
return
�� 
DACComonClass
��  
.
��  !$
GetMotivoDevolucionFac
��! 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� #
vw_sis_auditor_ciudad
�� )
>
��) * 
GetCiudadesAuditor
��+ =
(
��= >
)
��> ?
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetCiudadesAuditor
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� "
sis_auditor_regional
�� (
>
��( ) 
GetRegionalAuditor
��* <
(
��< =
)
��= >
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetRegionalAuditor
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� "
sis_auditor_regional
�� (
>
��( )&
listadoRegionalesUsuario
��* B
(
��B C
int
��C F
	idUsuario
��G P
)
��P Q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
listadoRegionalesUsuario
�� 7
(
��7 8
	idUsuario
��8 A
)
��A B
;
��B C
}
�� 	
public
�� "
sis_auditor_regional
�� # 
GetRegionalAuditor
��$ 6
(
��6 7
int
��7 :
?
��: ;
	idUsuario
��< E
)
��E F
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
GetRegionalAuditor
�� 1
(
��1 2
	idUsuario
��2 ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� %
vw_sis_auditor_regional
�� +
>
��+ ,"
GetVWRegionalAuditor
��- A
(
��A B
)
��B C
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
GetVWRegionalAuditor
��! 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
��  
Ref_hallazgos_RIPS
�� &
>
��& '
GetRefHallazgos
��( 7
(
��7 8
)
��8 9
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetRefHallazgos
��! 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
List
�� 
<
�� (
Ref_categorias_eventos_adv
�� .
>
��. /!
GetRefCategoriaEvad
��0 C
(
��C D
)
��D E
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
GetRefCategoriaEvad
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� "
Ref_motivo_reingreso
�� (
>
��( ) 
GetRefMotivoRiesgo
��* <
(
��< =
)
��= >
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetRefMotivoRiesgo
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� 3
%Ref_categorias_situaciones_de_calidad
�� 9
>
��9 :&
GetRefCategoriaSituacion
��; S
(
��S T
)
��T U
{
�� 	
return
�� 
DACComonClass
��  
.
��  !&
GetRefCategoriaSituacion
��! 9
(
��9 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� 
vw_cie10_alertas
�� $
>
��$ % 
GetRefcie10Alertas
��& 8
(
��8 9
)
��9 :
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetRefcie10Alertas
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� &
vw_cie10_alertas_detalle
�� ,
>
��, -%
GetRefcie10AlertasNuevo
��. E
(
��E F
)
��F G
{
�� 	
return
�� 
DACComonClass
��  
.
��  !%
GetRefcie10AlertasNuevo
��! 8
(
��8 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
List
�� 
<
�� (
Ref_enfermedades_Huerfanas
�� .
>
��. /
GetRefHuerfanas
��0 ?
(
��? @
)
��@ A
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetRefHuerfanas
��! 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_tipo_ahorro
�� #
>
��# $
GetRefTipoAhorro
��% 5
(
��5 6
)
��6 7
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetRefTipoAhorro
��! 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_PQRS_Atributo
�� %
>
��% & 
GetRefPqrsAtributo
��' 9
(
��9 :
)
��: ;
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetRefPqrsAtributo
��! 3
(
��3 4
)
��4 5
;
��5 6
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� %
Ref_PQRS_estado_Gestion
�� +
>
��+ ,
GetRefPqrsGestion
��- >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetRefPqrsGestion
��! 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_PQRS_llamada
�� $
>
��$ %
GetRefPqrsLLamada
��& 7
(
��7 8
)
��8 9
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetRefPqrsLLamada
��! 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� "
Ref_PQRS_Subtematica
�� (
>
��( )#
GetRefPqrsSubtematica
��* ?
(
��? @
)
��@ A
{
�� 	
return
�� 
DACComonClass
��  
.
��  !#
GetRefPqrsSubtematica
��! 6
(
��6 7
)
��7 8
;
��8 9
}
�� 	
public
�� 
List
�� 
<
�� %
Ref_PQRS_tipo_solicitud
�� +
>
��+ ,!
GetRefPqrsSolicitud
��- @
(
��@ A
)
��A B
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
GetRefPqrsSolicitud
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� 
vw_PQRS_usuarios
�� $
>
��$ % 
GetRefPqrsUsuarios
��& 8
(
��8 9
)
��9 :
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetRefPqrsUsuarios
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
void
�� $
EliminarPQRSEnrevision
�� *
(
��* +
int
��+ .
id_ecop_PQRS
��/ ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	

DACElimina
�� 
.
�� $
EliminarPQRSEnrevision
�� -
(
��- .
id_ecop_PQRS
��. :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
List
�� 
<
�� #
vw_md_crono_auditores
�� )
>
��) * 
GetRefCronoAuditor
��+ =
(
��= >
)
��> ?
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetRefCronoAuditor
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� '
Ref_analaisis_caso_ambito
�� -
>
��- .
	Getambito
��/ 8
(
��8 9
)
��9 :
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
	Getambito
��! *
(
��* +
)
��+ ,
;
��, -
}
�� 	
public
�� 
List
�� 
<
�� "
Ref_eventos_decision
�� (
>
��( )
GetEventoDecision
��* ;
(
��; <
)
��< =
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetEventoDecision
��! 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� #
Ref_eventos_habilidad
�� )
>
��) *!
GetEventosHabilidad
��+ >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
GetEventosHabilidad
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� $
Ref_eventos_percepcion
�� *
>
��* +"
GetEventosPercepcion
��, @
(
��@ A
)
��A B
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
GetEventosPercepcion
��! 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� #
Ref_eventos_rutinaria
�� )
>
��) *!
GetEventosRutinaria
��+ >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
GetEventosRutinaria
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� '
Ref_eventos_excepcionales
�� -
>
��- .%
GetEventosexcepcionales
��/ F
(
��F G
)
��G H
{
�� 	
return
�� 
DACComonClass
��  
.
��  !%
GetEventosexcepcionales
��! 8
(
��8 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
List
�� 
<
�� "
Ref_eventos_paciente
�� (
>
��( ) 
GetEventosPaciente
��* <
(
��< =
)
��= >
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetEventosPaciente
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� #
Ref_eventos_tarea_tec
�� )
>
��) *
GetEventostarea
��+ :
(
��: ;
)
��; <
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetEventostarea
��! 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
List
�� 
<
��  
Ref_eventos_equipo
�� &
>
��& '
GetEventosEquipoT
��( 9
(
��9 :
)
��: ;
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetEventosEquipoT
��! 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� #
Ref_eventos_individuo
�� )
>
��) *!
GetEventosIndividuo
��+ >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
GetEventosIndividuo
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� &
Ref_eventos_ambiente_tra
�� ,
>
��, -'
GetEventosambienteTrabajo
��. G
(
��G H
)
��H I
{
�� 	
return
�� 
DACComonClass
��  
.
��  !'
GetEventosambienteTrabajo
��! :
(
��: ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� &
Ref_eventos_organizacion
�� ,
>
��, -$
GetEventosOrganizacion
��. D
(
��D E
)
��E F
{
�� 	
return
�� 
DACComonClass
��  
.
��  !$
GetEventosOrganizacion
��! 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� "
Ref_eventos_contexto
�� (
>
��( ) 
GetEventosContexto
��* <
(
��< =
)
��= >
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetEventosContexto
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� #
Ref_eventos_severidad
�� )
>
��) *!
GetEventosSeveridad
��+ >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
GetEventosSeveridad
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� $
Ref_eventos_repeticion
�� *
>
��* +%
GetEventosProbavilidadR
��, C
(
��C D
)
��D E
{
�� 	
return
�� 
DACComonClass
��  
.
��  !%
GetEventosProbavilidadR
��! 8
(
��8 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
List
�� 
<
�� %
Ref_eventos_tipo_evento
�� +
>
��+ ,"
GetEventosTipoEvento
��- A
(
��A B
)
��B C
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
GetEventosTipoEvento
��! 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� 
vw_md_ref_glosa
�� #
>
��# $
GetMedglosa
��% 0
(
��0 1
)
��1 2
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetMedglosa
��! ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� !
vw_md_Ref_indicador
�� '
>
��' (
GetMDIndicadores
��) 9
(
��9 :
)
��: ;
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetMDIndicadores
��! 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
��  
ref_meses_del_año
�� %
>
��% &
meses
��' ,
(
��, -
)
��- .
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
meses
��! &
(
��& '
)
��' (
;
��( )
}
�� 	
public
�� 
List
�� 
<
�� 
ref_tipo_cohorte
�� $
>
��$ %
tipoCohortes
��& 2
(
��2 3
)
��3 4
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
tipoCohortes
��! -
(
��- .
)
��. /
;
��/ 0
}
�� 	
public
�� 
List
�� 
<
�� (
Ref_origen_hospitalizacion
�� .
>
��. /&
GetOrigenHospitalizacion
��0 H
(
��H I
)
��I J
{
�� 	
return
�� 
DACComonClass
��  
.
��  !&
GetOrigenHospitalizacion
��! 9
(
��9 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� +
vw_ref_enfermedades_huerfanas
�� 1
>
��1 2&
GetEnfermedadesHuerfanas
��3 K
(
��K L
)
��L M
{
�� 	
return
�� 
DACComonClass
��  
.
��  !&
GetEnfermedadesHuerfanas
��! 9
(
��9 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� &
vw_evo_ecop_concurrencia
�� ,
>
��, -&
ConsultaIdConcurreniaEvo
��. F
(
��F G&
vw_evo_ecop_concurrencia
��G _
ObjAfiliado
��` k
,
��k l
ref
��m p!
MessageResponseOBJ��q �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
ConsultaIdConcurreniaEvo
�� 7
(
��7 8
ObjAfiliado
��8 C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
List
�� 
<
�� )
ecop_concurrencia_evolucion
�� /
>
��/ 0'
ConsultaNumeroEvoluciones
��1 J
(
��J K)
ecop_concurrencia_evolucion
��K f
ObjAfiliado
��g r
,
��r s
ref
��t w!
MessageResponseOBJ��x �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
ConsultaNumeroEvoluciones
�� 8
(
��8 9
ObjAfiliado
��9 D
,
��D E
ref
��F I
MsgRes
��J P
)
��P Q
;
��Q R
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_rol_cargo
�� !
>
��! "
RolCargo
��# +
(
��+ ,
)
��, -
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
RolCargo
��! )
(
��) *
)
��* +
;
��+ ,
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_odont_unis
�� "
>
��" #

Odont_unis
��$ .
(
��. /
)
��/ 0
{
�� 	
return
�� 
DACComonClass
��  
.
��  !

Odont_unis
��! +
(
��+ ,
)
��, -
;
��- .
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_odont_unis
�� "
>
��" #"
Odont_unisIdRegional
��$ 8
(
��8 9
int
��9 <
?
��< =

idRegional
��> H
)
��H I
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
Odont_unisIdRegional
��! 5
(
��5 6

idRegional
��6 @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
�� 2
$ref_odontologia_protesisfija_dientes
�� 8
>
��8 9'
OdontoProtesisFijaDientes
��: S
(
��S T
int
��T W
?
��W X
tipo
��Y ]
)
��] ^
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
OdontoProtesisFijaDientes
�� 8
(
��8 9
tipo
��9 =
)
��= >
;
��> ?
}
�� 	
public
�� 
List
�� 
<
�� 2
$ref_odontologia_protesisfija_dientes
�� 8
>
��8 9,
OdontoProtesisFijaDientesTotal
��: X
(
��X Y
)
��Y Z
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
OdontoProtesisFijaDientesTotal
�� =
(
��= >
)
��> ?
;
��? @
}
�� 	
public
�� 2
$ref_odontologia_protesisfija_dientes
�� 3
TraerDienteId
��4 A
(
��A B
int
��B E
?
��E F
id
��G I
)
��I J
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
TraerDienteId
�� ,
(
��, -
id
��- /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
List
�� 
<
�� 
	Ref_si_no
�� 
>
�� 
Ref_sino
�� '
(
��' (
)
��( )
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
Ref_sino
��! )
(
��) *
)
��* +
;
��+ ,
}
�� 	
public
�� 
List
�� 
<
�� #
vw_ref_regiona_ciudad
�� )
>
��) *!
Ref_regional_ciudad
��+ >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
Ref_regional_ciudad
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� .
 management_regional_ciudadResult
�� 4
>
��4 5+
Ref_regional_ciudad_detallado
��6 S
(
��S T
)
��T U
{
�� 	
return
�� 
DACComonClass
��  
.
��  !+
Ref_regional_ciudad_detallado
��! >
(
��> ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
�� *
Ref_plan_mejora_estado_tarea
�� 0
>
��0 1
estadoTarea
��2 =
(
��= >
)
��> ?
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
estadoTarea
��! ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� 5
'ManagementPrestadoresAlertasFechaResult
�� ;
>
��; <
ManagmentAlertas
��= M
(
��M N
ref
��N Q 
MessageResponseOBJ
��R d
MsgRes
��e k
)
��k l
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
ManagmentAlertas
��! 1
(
��1 2
ref
��2 5
MsgRes
��6 <
)
��< =
;
��= >
}
�� 	
public
�� 
List
�� 
<
�� $
ref_consulta_ecopetrol
�� *
>
��* +$
Ref_ConsultasEcopetrol
��, B
(
��B C
)
��C D
{
�� 	
return
�� 
DACComonClass
��  
.
��  !$
Ref_ConsultasEcopetrol
��! 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
void
�� '
InsertarActividadReciente
�� -
(
��- .$
sis_actividad_reciente
��. D
obj
��E H
,
��H I
ref
��J M 
MessageResponseOBJ
��N `
MsgRes
��a g
)
��g h
{
�� 	
DACComonClass
�� 
.
�� '
InsertarActividadReciente
�� 3
(
��3 4
obj
��4 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
List
�� 
<
�� 5
'Management_sis_actividad_recienteResult
�� ;
>
��; <&
GetListActividadReciente
��= U
(
��U V
)
��V W
{
�� 	
return
�� 
DACComonClass
��  
.
��  !&
GetListActividadReciente
��! 9
(
��9 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ffmm_glosas
�� #
>
��# $
FFMM_glosas
��% 0
(
��0 1
)
��1 2
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_glosas
��! ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� !
Ref_ffmm_alto_costo
�� '
>
��' (
FFMM_altocosto
��) 7
(
��7 8
)
��8 9
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_altocosto
��! /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� *
Ref_ffmm_especialidad_remite
�� 0
>
��0 1&
FFMM_especialidad_remite
��2 J
(
��J K
)
��K L
{
�� 	
return
�� 
DACComonClass
��  
.
��  !&
FFMM_especialidad_remite
��! 9
(
��9 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� +
Ref_ffmm_especialidad_remitio
�� 1
>
��1 2'
FFMM_especialidad_remitio
��3 L
(
��L M
)
��M N
{
�� 	
return
�� 
DACComonClass
��  
.
��  !'
FFMM_especialidad_remitio
��! :
(
��: ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ffmm_estado
�� #
>
��# $
FFMM_estado
��% 0
(
��0 1
)
��1 2
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_estado
��! ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ffmm_fuerza
�� #
>
��# $
FFMM_fuerza
��% 0
(
��0 1
)
��1 2
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_fuerza
��! ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� "
Ref_ffmm_imagen_proc
�� (
>
��( )
FFMM_imagen_proc
��* :
(
��: ;
)
��; <
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_imagen_proc
��! 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� %
Ref_ffmm_modalidad_pago
�� +
>
��+ ,!
FFMM_modalidad_pago
��- @
(
��@ A
)
��A B
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
FFMM_modalidad_pago
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� %
Ref_ffmm_nivel_atencion
�� +
>
��+ ,!
FFMM_nivel_atencion
��- @
(
��@ A
)
��A B
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
FFMM_nivel_atencion
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� (
Ref_ffmm_nivel_complejidad
�� .
>
��. /$
FFMM_nivel_complejidad
��0 F
(
��F G
)
��G H
{
�� 	
return
�� 
DACComonClass
��  
.
��  !$
FFMM_nivel_complejidad
��! 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� &
Ref_ffmm_origen_servicio
�� ,
>
��, -"
FFMM_origen_servicio
��. B
(
��B C
)
��C D
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
FFMM_origen_servicio
��! 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� "
Ref_ffmm_prestadores
�� (
>
��( )
FFMM_prestadores
��* :
(
��: ;
)
��; <
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_prestadores
��! 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� 
vw_ffmm_ips
�� 
>
��  !
FFMM_prestadores_vw
��! 4
(
��4 5
)
��5 6
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
FFMM_prestadores_vw
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ffmm_proceso
�� $
>
��$ %
FFMM_proceso
��& 2
(
��2 3
)
��3 4
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_proceso
��! -
(
��- .
)
��. /
;
��/ 0
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ffmm_servicio
�� %
>
��% &
FFMM_servicio
��' 4
(
��4 5
)
��5 6
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_servicio
��! .
(
��. /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ffmm_sexo
�� !
>
��! "
	FFMM_sexo
��# ,
(
��, -
)
��- .
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
	FFMM_sexo
��! *
(
��* +
)
��+ ,
;
��, -
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ffmm_sino
�� !
>
��! "
	FFMM_sino
��# ,
(
��, -
)
��- .
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
	FFMM_sino
��! *
(
��* +
)
��+ ,
;
��, -
}
�� 	
public
�� 
List
�� 
<
�� &
Ref_ffmm_tipo_afiliacion
�� ,
>
��, -"
FFMM_tipo_afiliacion
��. B
(
��B C
)
��C D
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
FFMM_tipo_afiliacion
��! 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� $
Ref_ffmm_tipo_servicio
�� *
>
��* + 
FFMM_tipo_servicio
��, >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
FFMM_tipo_servicio
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� &
Ref_ffmm_unidad_satelite
�� ,
>
��, -"
FFMM_unidad_satelite
��. B
(
��B C
)
��C D
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
FFMM_unidad_satelite
��! 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ffmm_unudadR
�� $
>
��$ %
FFMM_unudadR
��& 2
(
��2 3
)
��3 4
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_unudadR
��! -
(
��- .
)
��. /
;
��/ 0
}
�� 	
public
�� 
List
�� 
<
�� "
vw_ffmm_departamento
�� (
>
��( )
FFMM_departamento
��* ;
(
��; <
)
��< =
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_departamento
��! 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� 
vw_ffmm_municipio
�� %
>
��% &
FFMM_municipio
��' 5
(
��5 6
)
��6 7
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_municipio
��! /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� 
vw_FMM_RefGeneral
�� %
>
��% &
FFMM_Reg_General
��' 7
(
��7 8
)
��8 9
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_Reg_General
��! 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� ,
Ref_ffmm_prestadores_proveedor
�� 2
>
��2 3(
FFMM_prestadores_Proveedor
��4 N
(
��N O
)
��O P
{
�� 	
return
�� 
DACComonClass
��  
.
��  !(
FFMM_prestadores_Proveedor
��! ;
(
��; <
)
��< =
;
��= >
}
�� 	
public
�� 
List
�� 
<
�� %
Ref_ffmm_tipo_proveedor
�� +
>
��+ ,!
FFMM_tipo_proveedor
��- @
(
��@ A
)
��A B
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
FFMM_tipo_proveedor
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� '
Ref_ffmm_glosas_respuesta
�� -
>
��- .$
FFMM_respuestas_glosas
��/ E
(
��E F
)
��F G
{
�� 	
return
�� 
DACComonClass
��  
.
��  !$
FFMM_respuestas_glosas
��! 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
��  
Ref_ffmm_unidad_cp
�� &
>
��& '
FFMM_Unidad_CP
��( 6
(
��6 7
)
��7 8
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_Unidad_CP
��! /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� %
Ref_ffmm_tipo_visita_cp
�� +
>
��+ ,
FFMM_TipoV_CP
��- :
(
��: ;
)
��; <
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
FFMM_TipoV_CP
��! .
(
��. /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
Int32
�� #
InsertarFFMMAuditoria
�� *
(
��* +
ffmm_auditoria
��+ 9
OBJ
��: =
,
��= >
ref
��? B 
MessageResponseOBJ
��C U
MsgRes
��V \
)
��\ ]
{
�� 	
return
�� 

DACInserta
�� 
.
�� #
InsertarFFMMAuditoria
�� 3
(
��3 4
OBJ
��4 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
List
�� 
<
�� +
ref_ffmm_ips_prestadores_tipo
�� 1
>
��1 2
tipoIpsPrestador
��3 C
(
��C D
)
��D E
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
tipoIpsPrestador
��! 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� 3
%management_traerIpsoPrestadoresResult
�� 9
>
��9 : 
traerPrestadoresId
��; M
(
��M N
int
��N Q
tipo
��R V
,
��V W
int
��X [
nit
��\ _
)
��_ `
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
traerPrestadoresId
�� 1
(
��1 2
tipo
��2 6
,
��6 7
nit
��8 ;
)
��; <
;
��< =
}
�� 	
public
�� 
int
�� "
InsertarIpsPrestador
�� '
(
��' (&
ref_ffmm_ips_prestadores
��( @
obj
��A D
)
��D E
{
�� 	
return
�� 

DACInserta
�� 
.
�� "
InsertarIpsPrestador
�� 2
(
��2 3
obj
��3 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� &
ref_ffmm_ips_prestadores
�� ,
>
��, -&
existenciaIpsPrestadores
��. F
(
��F G
int
��G J
nit
��K N
)
��N O
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
existenciaIpsPrestadores
�� 7
(
��7 8
nit
��8 ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� &
ref_ffmm_ips_prestadores
�� ,
>
��, -,
existenciaIpsPrestadoresNombre
��. L
(
��L M
String
��M S
nombre
��T Z
)
��Z [
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
existenciaIpsPrestadoresNombre
�� =
(
��= >
nombre
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
int
�� &
ActualizarIpsPrestadores
�� +
(
��+ ,&
ref_ffmm_ips_prestadores
��, D
obj
��E H
)
��H I
{
�� 	
return
�� 
DACActualiza
�� 
.
��  &
ActualizarIpsPrestadores
��  8
(
��8 9
obj
��9 <
)
��< =
;
��= >
}
�� 	
public
�� 
List
�� 
<
�� &
ref_ffmm_ips_prestadores
�� ,
>
��, -!
ObtenerIpsPrestador
��. A
(
��A B
int
��B E
idCiudad
��F N
,
��N O
int
��P S
tipo
��T X
)
��X Y
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
ObtenerIpsPrestador
�� 2
(
��2 3
idCiudad
��3 ;
,
��; <
tipo
��= A
)
��A B
;
��B C
}
�� 	
public
�� 
List
�� 
<
�� &
ref_ffmm_ips_prestadores
�� ,
>
��, -(
ObtenerIpsPrestadorSinTipo
��. H
(
��H I
int
��I L
idCiudad
��M U
)
��U V
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
ObtenerIpsPrestadorSinTipo
�� 9
(
��9 :
idCiudad
��: B
)
��B C
;
��C D
}
�� 	
public
�� 
List
�� 
<
�� &
ref_ffmm_ips_prestadores
�� ,
>
��, -)
ObtenerIpsPrestadorCompleto
��. I
(
��I J
)
��J K
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
ObtenerIpsPrestadorCompleto
�� :
(
��: ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� 4
&management_contratos_prestadoresResult
�� :
>
��: ;(
ObtenerIpsPrestadorTablero
��< V
(
��V W
)
��W X
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
ObtenerIpsPrestadorTablero
�� 9
(
��9 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� 6
(managmentFFMMfacturasRadicadasLoteResult
�� <
>
��< =&
GetRecepcionFacturasFFMM
��> V
(
��V W
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetRecepcionFacturasFFMM
�� 7
(
��7 8
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
List
�� 
<
�� 6
(Management_FFMM_Glosas_PrestadoresResult
�� <
>
��< =&
GetFFMMGlosasPrestadores
��> V
(
��V W
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetFFMMGlosasPrestadores
�� 7
(
��7 8
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
List
�� 
<
�� 6
(managmentFFMMfacturasRadicadasdtllResult
�� <
>
��< =*
GetRecepcionFacturasDTLLFFMM
��> Z
(
��Z [
Int32
��[ `
id_cargue_base
��a o
,
��o p
ref
��q t!
MessageResponseOBJ��u �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
GetRecepcionFacturasDTLLFFMM
�� ;
(
��; <
id_cargue_base
��< J
,
��J K
ref
��L O
MsgRes
��P V
)
��V W
;
��W X
}
�� 	
public
�� 
List
�� 
<
�� 9
+managmentFFMMfacturasRadicadasid_dtllResult
�� ?
>
��? @,
GetRecepcionFacturasDTLLFFMMId
��A _
(
��_ `
Int32
��` e
id_cargue_dtll
��f t
,
��t u
ref
��v y!
MessageResponseOBJ��z �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
GetRecepcionFacturasDTLLFFMMId
�� =
(
��= >
id_cargue_dtll
��> L
,
��L M
ref
��N Q
MsgRes
��R X
)
��X Y
.
��Y Z
ToList
��Z `
(
��` a
)
��a b
;
��b c
}
�� 	
public
�� 
List
�� 
<
�� 4
&Management_actualizar_FacturaDigResult
�� :
>
��: ;$
ActualizarFFMMFacturas
��< R
(
��R S
Int32
��S X

Id_factura
��Y c
,
��c d
Int32
��e j
estado
��k q
,
��q r
ref
��s v!
MessageResponseOBJ��w �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACActualiza
�� 
.
��  $
ActualizarFFMMFacturas
��  6
(
��6 7

Id_factura
��7 A
,
��A B
estado
��C I
,
��I J
ref
��K N
MsgRes
��O U
)
��U V
;
��V W
}
�� 	
public
�� 
List
�� 
<
�� 5
'Management_FFMM_Consultas_cuentasResult
�� ;
>
��; <$
GetConsultaFFMMCuentas
��= S
(
��S T
ref
��T W 
MessageResponseOBJ
��X j
MsgRes
��k q
)
��q r
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
GetConsultaFFMMCuentas
�� 5
(
��5 6
ref
��6 9
MsgRes
��: @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
�� 9
+Management_FFMM_Consultas_ConcurreniaResult
�� ?
>
��? @)
GetConsultaFFMMConcurrencia
��A \
(
��\ ]
ref
��] ` 
MessageResponseOBJ
��a s
MsgRes
��t z
)
��z {
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
GetConsultaFFMMConcurrencia
�� :
(
��: ;
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
List
�� 
<
�� 1
#Management_FFMM_Consultas_PADResult
�� 7
>
��7 8 
GetConsultaFFMMPad
��9 K
(
��K L
ref
��L O 
MessageResponseOBJ
��P b
MsgRes
��c i
)
��i j
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
GetConsultaFFMMPad
�� 1
(
��1 2
ref
��2 5
MsgRes
��6 <
)
��< =
;
��= >
}
�� 	
public
�� 
List
�� 
<
�� <
.Management_FFMM_consulta_cuentas_primeraResult
�� B
>
��B C'
GetConsultaFFMMCuentasUno
��D ]
(
��] ^
DateTime
��^ f
fecha_inicial
��g t
,
��t u
DateTime
��v ~
fecha_final�� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
GetConsultaFFMMCuentasUno
�� 8
(
��8 9
fecha_inicial
��9 F
,
��F G
fecha_final
��H S
,
��S T
ref
��U X
MsgRes
��Y _
)
��_ `
;
��` a
}
�� 	
public
�� 
List
�� 
<
�� <
.Management_FFMM_consulta_cuentas_segundaResult
�� B
>
��B C'
GetConsultaFFMMCuentasDos
��D ]
(
��] ^
DateTime
��^ f
fecha_inicial
��g t
,
��t u
DateTime
��v ~
fecha_final�� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
GetConsultaFFMMCuentasDos
�� 8
(
��8 9
fecha_inicial
��9 F
,
��F G
fecha_final
��H S
,
��S T
ref
��U X
MsgRes
��Y _
)
��_ `
;
��` a
}
�� 	
public
�� 
List
�� 
<
�� <
.Management_FFMM_consulta_cuentas_terceraResult
�� B
>
��B C(
GetConsultaFFMMCuentasTres
��D ^
(
��^ _
DateTime
��_ g
fecha_inicial
��h u
,
��u v
DateTime
��w 
fecha_final��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
GetConsultaFFMMCuentasTres
�� 9
(
��9 :
fecha_inicial
��: G
,
��G H
fecha_final
��I T
,
��T U
ref
��V Y
MsgRes
��Z `
)
��` a
;
��a b
}
�� 	
public
�� 
List
�� 
<
�� ;
-Management_FFMM_consulta_cuentas_cuartaResult
�� A
>
��A B*
GetConsultaFFMMCuentasCuatro
��C _
(
��_ `
DateTime
��` h
fecha_inicial
��i v
,
��v w
DateTime��x �
fecha_final��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
GetConsultaFFMMCuentasCuatro
�� ;
(
��; <
fecha_inicial
��< I
,
��I J
fecha_final
��K V
,
��V W
ref
��X [
MsgRes
��\ b
)
��b c
;
��c d
}
�� 	
public
�� 
List
�� 
<
�� ;
-Management_FFMM_consulta_cuentas_quintaResult
�� A
>
��A B)
GetConsultaFFMMCuentasCinco
��C ^
(
��^ _
DateTime
��_ g
fecha_inicial
��h u
,
��u v
DateTime
��w 
fecha_final��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
GetConsultaFFMMCuentasCinco
�� :
(
��: ;
fecha_inicial
��; H
,
��H I
fecha_final
��J U
,
��U V
ref
��W Z
MsgRes
��[ a
)
��a b
;
��b c
}
�� 	
public
�� 
List
�� 
<
�� 
ref_auditor
�� 
>
��  
listAuditor
��! ,
(
��, -
)
��- .
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
listAuditor
�� *
(
��* +
)
��+ ,
;
��, -
}
�� 	
public
�� 
List
�� 
<
�� A
3Management_FFMM_consulta_concurrencia_primeraResult
�� G
>
��G H,
GetConsultaFFMMConcurrenciaUno
��I g
(
��g h
DateTime
��h p
fecha_inicial
��q ~
,
��~ 
DateTime��� �
fecha_final��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
GetConsultaFFMMConcurrenciaUno
�� =
(
��= >
fecha_inicial
��> K
,
��K L
fecha_final
��M X
,
��X Y
ref
��Z ]
MsgRes
��^ d
)
��d e
;
��e f
}
�� 	
public
�� 
List
�� 
<
�� A
3Management_FFMM_consulta_concurrencia_segundaResult
�� G
>
��G H,
GetConsultaFFMMConcurrenciaDos
��I g
(
��g h
DateTime
��h p
fecha_inicial
��q ~
,
��~ 
DateTime��� �
fecha_final��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
GetConsultaFFMMConcurrenciaDos
�� =
(
��= >
fecha_inicial
��> K
,
��K L
fecha_final
��M X
,
��X Y
ref
��Z ]
MsgRes
��^ d
)
��d e
;
��e f
}
�� 	
public
�� 
List
�� 
<
�� A
3Management_FFMM_consulta_concurrencia_terceroResult
�� G
>
��G H0
"GetConsultaFFMMConcurrenciaTercero
��I k
(
��k l
DateTime
��l t
fecha_inicial��u �
,��� �
DateTime��� �
fecha_final��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 0
"GetConsultaFFMMConcurrenciaTercero
�� A
(
��A B
fecha_inicial
��B O
,
��O P
fecha_final
��Q \
,
��\ ]
ref
��^ a
MsgRes
��b h
)
��h i
;
��i j
}
�� 	
public
�� 
List
�� 
<
�� @
2Management_FFMM_consulta_concurrencia_cuartoResult
�� F
>
��F G/
!GetConsultaFFMMConcurrenciaCuarto
��H i
(
��i j
DateTime
��j r
fecha_inicial��s �
,��� �
DateTime��� �
fecha_final��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� /
!GetConsultaFFMMConcurrenciaCuarto
�� @
(
��@ A
fecha_inicial
��A N
,
��N O
fecha_final
��P [
,
��[ \
ref
��] `
MsgRes
��a g
)
��g h
;
��h i
}
�� 	
public
�� 
Int32
�� 
CrearUsuairo
�� !
(
��! "
sis_usuario
��" -

ObjUsuario
��. 8
,
��8 9
ref
��: = 
MessageResponseOBJ
��> P
MsgRes
��Q W
)
��W X
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
CrearUsuairo
�� *
(
��* +

ObjUsuario
��+ 5
,
��5 6
ref
��7 :
MsgRes
��; A
)
��A B
;
��B C
}
�� 	
public
�� 
List
�� 
<
�� 
sis_usuario
�� 
>
��  
ValidaIngreso
��! .
(
��. /
sis_usuario
��/ :

ObjUsuario
��; E
,
��E F
ref
��G J 
MessageResponseOBJ
��K ]
MsgRes
��^ d
)
��d e
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ValidaIngreso
�� ,
(
��, -

ObjUsuario
��- 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
sis_usuario
��  
ValidaIngresoClave
�� -
(
��- .
sis_usuario
��. 9

ObjUsuario
��: D
,
��D E
ref
��F I 
MessageResponseOBJ
��J \
MsgRes
��] c
)
��c d
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
ValidaIngresoClave
�� 1
(
��1 2

ObjUsuario
��2 <
,
��< =
ref
��> A
MsgRes
��B H
)
��H I
;
��I J
}
�� 	
public
�� 
List
�� 
<
�� !
ManagmentMenuResult
�� '
>
��' (
ManagmentMenu
��) 6
(
��6 7
String
��7 =

Strusuario
��> H
,
��H I
ref
��J M 
MessageResponseOBJ
��N `
MsgRes
��a g
)
��g h
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ManagmentMenu
�� ,
(
��, -

Strusuario
��- 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
void
�� "
ActualizaContraseña
�� '
(
��' (
sis_usuario
��( 3

ObjUsuario
��4 >
,
��> ?
ref
��@ C 
MessageResponseOBJ
��D V
MsgRes
��W ]
)
��] ^
{
�� 	
DACActualiza
�� 
.
�� "
ActualizaContraseña
�� ,
(
��, -

ObjUsuario
��- 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
void
�� (
ActualizaContraseñaOlvido
�� -
(
��- .
sis_usuario
��. 9
Usuario
��: A
,
��A B
ref
��C F 
MessageResponseOBJ
��G Y
MsgRes
��Z `
)
��` a
{
�� 	
DACActualiza
�� 
.
�� (
ActualizaContraseñaOlvido
�� 2
(
��2 3
Usuario
��3 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
void
�� $
ActualizaEstadoUsuario
�� *
(
��* +
sis_usuario
��+ 6

ObjUsuario
��7 A
,
��A B
ref
��C F 
MessageResponseOBJ
��G Y
MsgRes
��Z `
)
��` a
{
�� 	
DACActualiza
�� 
.
�� $
ActualizaEstadoUsuario
�� /
(
��/ 0

ObjUsuario
��0 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
List
�� 
<
�� 
sis_usuario
�� 
>
��  
BuscaAuditorUsu
��! 0
(
��0 1
String
��1 7

strUsuario
��8 B
,
��B C
ref
��D G 
MessageResponseOBJ
��H Z
MsgRes
��[ a
)
��a b
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
BuscaAuditorUsu
�� .
(
��. /

strUsuario
��/ 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
List
�� 
<
�� 
sis_usuario
�� 
>
��  
BuscaAuditorNom
��! 0
(
��0 1
String
��1 7
	strNombre
��8 A
,
��A B
ref
��C F 
MessageResponseOBJ
��G Y
MsgRes
��Z `
)
��` a
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
BuscaAuditorNom
�� .
(
��. /
	strNombre
��/ 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
void
�� 
GestionUsuarios
�� #
(
��# $
sis_usuario
��$ /

ObjUsuario
��0 :
,
��: ;
ref
��< ? 
MessageResponseOBJ
��@ R
MsgRes
��S Y
)
��Y Z
{
�� 	
DACConsulta
�� 
.
�� 
GestionUsuarios
�� '
(
��' (

ObjUsuario
��( 2
,
��2 3
ref
��4 7
MsgRes
��8 >
)
��> ?
;
��? @
}
�� 	
public
�� 
DateTime
�� 
ManagmentHora
�� %
(
��% &
)
��& '
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ManagmentHora
�� ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� 
vw_rol_usuarios
�� #
>
��# $
Ref_rol_Usuario
��% 4
(
��4 5
)
��5 6
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
Ref_rol_Usuario
��! 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
List
�� 
<
�� 
vw_cargo_usuario
�� $
>
��$ %
Ref_cargo_Usuario
��& 7
(
��7 8
)
��8 9
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
Ref_cargo_Usuario
��! 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� 
sis_usuario
�� 
>
��  
BuscaAuditorDoc
��! 0
(
��0 1
String
��1 7

strUsuario
��8 B
,
��B C
ref
��D G 
MessageResponseOBJ
��H Z
MsgRes
��[ a
)
��a b
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
BuscaAuditorDoc
�� .
(
��. /

strUsuario
��/ 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
List
�� 
<
�� %
vw_pruebas_img_usuarios
�� +
>
��+ ,
BuscaAuditorImg
��- <
(
��< =
String
��= C

strUsuario
��D N
,
��N O
ref
��P S 
MessageResponseOBJ
��T f
MsgRes
��g m
)
��m n
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
BuscaAuditorImg
�� .
(
��. /

strUsuario
��/ 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
List
�� 
<
�� 
sis_usuario
�� 
>
��  !
BuscaAuditorUsuSami
��! 4
(
��4 5
String
��5 ;

strUsuario
��< F
,
��F G
ref
��H K 
MessageResponseOBJ
��L ^
MsgRes
��_ e
)
��e f
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
BuscaAuditorUsuSami
�� 2
(
��2 3

strUsuario
��3 =
,
��= >
ref
��? B
MsgRes
��C I
)
��I J
;
��J K
}
�� 	
public
�� 
List
�� 
<
�� 
sis_usuario
�� 
>
��  
GetUsuarios
��! ,
(
��, -
)
��- .
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetUsuarios
�� *
(
��* +
)
��+ ,
;
��, -
}
�� 	
public
�� 
List
�� 
<
�� C
5management_sis_usuarios_controlActividadesCensoResult
�� I
>
��I J
GetUsuariosCenso
��K [
(
��[ \
)
��\ ]
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetUsuariosCenso
�� /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
Int32
�� '
InsertarLogGestionUusario
�� .
(
��. /"
log_gestion_usuarios
��/ C
log
��D G
,
��G H
ref
��I L 
MessageResponseOBJ
��M _
MsgRes
��` f
)
��f g
{
�� 	
return
�� 

DACInserta
�� 
.
�� '
InsertarLogGestionUusario
�� 7
(
��7 8
log
��8 ;
,
��; <
ref
��= @
MsgRes
��A G
)
��G H
;
��H I
}
�� 	
public
�� 
void
�� #
ActualizaClaveUsuario
�� )
(
��) *
sis_usuario
��* 5
OBJ
��6 9
,
��9 :
ref
��; > 
MessageResponseOBJ
��? Q
MsgRes
��R X
)
��X Y
{
�� 	
DACActualiza
�� 
.
�� #
ActualizaClaveUsuario
�� .
(
��. /
OBJ
��/ 2
,
��2 3
ref
��4 7
MsgRes
��8 >
)
��> ?
;
��? @
}
�� 	
public
�� 
void
�� %
InsertarLogInicioSesion
�� +
(
��+ ,
Log_InicioSession
��, =
obj
��> A
,
��A B
ref
��C F 
MessageResponseOBJ
��G Y
MsgRes
��Z `
)
��` a
{
�� 	

DACInserta
�� 
.
�� %
InsertarLogInicioSesion
�� .
(
��. /
obj
��/ 2
,
��2 3
ref
��4 7
MsgRes
��8 >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� 
vw_datos_censo
�� "
>
��" #
CensoDocumento
��$ 2
(
��2 3
String
��3 9
	Documento
��: C
,
��C D
ref
��E H 
MessageResponseOBJ
��I [
MsgRes
��\ b
)
��b c
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
CensoDocumento
�� -
(
��- .
	Documento
��. 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 

ecop_censo
�� ,
ConsultaCensoIdentificacionPac
�� 8
(
��8 9
string
��9 ?

idPaciente
��@ J
)
��J K
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
ConsultaCensoIdentificacionPac
�� =
(
��= >

idPaciente
��> H
)
��H I
;
��I J
}
�� 	
public
�� 4
&management_datosPaciente_alertasResult
�� 5
DatosPaciente
��6 C
(
��C D
int
��D G
idConcurrencia
��H V
)
��V W
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
DatosPaciente
�� ,
(
��, -
idConcurrencia
��- ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� 
vw_datos_censo
�� "
>
��" #
CensoFacturas
��$ 1
(
��1 2
ref
��2 5 
MessageResponseOBJ
��6 H
MsgRes
��I O
)
��O P
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
CensoFacturas
�� ,
(
��, -
ref
��- 0
MsgRes
��1 7
)
��7 8
;
��8 9
}
�� 	
public
�� 
List
�� 
<
�� *
management_datos_censoResult
�� 0
>
��0 1$
CensoFacturasDetallado
��2 H
(
��H I
string
��I O
	documento
��P Y
,
��Y Z
string
��[ a
nombre
��b h
,
��h i
DateTime
��j r
?
��r s
fechaEgresoConcu��t �
,��� �
DateTime��� �
?��� � 
fechaEgresoCenso��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
CensoFacturasDetallado
�� 5
(
��5 6
	documento
��6 ?
,
��? @
nombre
��A G
,
��G H
fechaEgresoConcu
��I Y
,
��Y Z
fechaEgresoCenso
��[ k
,
��k l
ref
��m p
MsgRes
��q w
)
��w x
;
��x y
}
�� 	
public
�� 
Int32
�� 
InsertarCenso
�� "
(
��" #

ecop_censo
��# -
OBJ
��. 1
,
��1 2
ref
��3 6 
MessageResponseOBJ
��7 I
MsgRes
��J P
)
��P Q
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarCenso
�� +
(
��+ ,
OBJ
��, /
,
��/ 0
ref
��1 4
MsgRes
��5 ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� 
vw_datos_censo
�� "
>
��" #
CensoId
��$ +
(
��+ ,
Int32
��, 1
Id
��2 4
,
��4 5
ref
��6 9 
MessageResponseOBJ
��: L
MsgRes
��M S
)
��S T
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
CensoId
�� &
(
��& '
Id
��' )
,
��) *
ref
��+ .
MsgRes
��/ 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� 

ecop_censo
�� 
>
�� 

GetCensoId
��  *
(
��* +
Int32
��+ 0
Id
��1 3
,
��3 4
ref
��5 8 
MessageResponseOBJ
��9 K
MsgRes
��L R
)
��R S
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 

GetCensoId
�� )
(
��) *
Id
��* ,
,
��, -
ref
��. 1
MsgRes
��2 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
void
�� 
ActualizarCenso
�� #
(
��# $

ecop_censo
��$ . 
ActualizaSiniestro
��/ A
,
��A B
ref
��C F 
MessageResponseOBJ
��G Y
MsgRes
��Z `
)
��` a
{
�� 	
DACActualiza
�� 
.
�� 
ActualizarCenso
�� (
(
��( ) 
ActualizaSiniestro
��) ;
,
��; <
ref
��= @
MsgRes
��A G
)
��G H
;
��H I
}
�� 	
public
�� 

ecop_censo
�� "
ConsultaCensoIdCenso
�� .
(
��. /
int
��/ 2
?
��2 3
idCenso
��4 ;
)
��; <
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
ConsultaCensoIdCenso
�� 3
(
��3 4
idCenso
��4 ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� +
vw_censo_control_concurrencia
�� 1
>
��1 2'
CensoConcurrenciasTotales
��3 L
(
��L M
)
��M N
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
CensoConcurrenciasTotales
�� 8
(
��8 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
List
�� 
<
�� D
6management_censo_control_concurrencia_optimizadaResult
�� J
>
��J K1
#CensoConcurrenciasTotalesOptimizada
��L o
(
��o p
int
��p s
?
��s t
regional
��u }
,
��} ~
string�� �
	documento��� �
,��� �
string��� �
nombre��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 1
#CensoConcurrenciasTotalesOptimizada
�� B
(
��B C
regional
��C K
,
��K L
	documento
��M V
,
��V W
nombre
��X ^
)
��^ _
;
��_ `
}
�� 	
public
�� 
List
�� 
<
�� -
vw_censo_control_cuentasMedicas
�� 3
>
��3 4(
CensoCuentasMedicasTotales
��5 O
(
��O P
)
��P Q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
CensoCuentasMedicasTotales
�� 9
(
��9 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� F
8management_censo_control_cuentasMedicas_optimizadaResult
�� L
>
��L M2
$CensoCuentasMedicasTotalesOptimizada
��N r
(
��r s
int
��s v
?
��v w
regional��x �
,��� �
string��� �
	documento��� �
,��� �
string��� �
nombre��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 2
$CensoCuentasMedicasTotalesOptimizada
�� C
(
��C D
regional
��D L
,
��L M
	documento
��N W
,
��W X
nombre
��Y _
)
��_ `
;
��` a
}
�� 	
public
�� 
List
�� 
<
�� &
vw_censo_control_visitas
�� ,
>
��, -!
CensoVisitasTotales
��. A
(
��A B
)
��B C
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
CensoVisitasTotales
�� 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� ?
1management_censo_control_visitas_optimizadaResult
�� E
>
��E F+
CensoVisitasTotalesOptimizada
��G d
(
��d e
int
��e h
?
��h i
regional
��j r
,
��r s
string
��t z
	documento��{ �
,��� �
string��� �
nombre��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
CensoVisitasTotalesOptimizada
�� <
(
��< =
regional
��= E
,
��E F
	documento
��G P
,
��P Q
nombre
��R X
)
��X Y
;
��Y Z
}
�� 	
public
�� 
List
�� 
<
�� N
@management_sis_usuarios_controlActividadesCenso_optimizadaResult
�� T
>
��T U(
GetUsuariosCensoOptimizada
��V p
(
��p q
int
��q t
?
��t u
regional
��v ~
,
��~ 
string��� �
	documento��� �
,��� �
string��� �
nombre��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
GetUsuariosCensoOptimizada
�� 9
(
��9 :
regional
��: B
,
��B C
	documento
��D M
,
��M N
nombre
��O U
)
��U V
;
��V W
}
�� 	
public
�� 
List
�� 
<
�� *
ref_ecop_censo_tiposConsulta
�� 0
>
��0 1.
 CensoConsultaReportesActividades
��2 R
(
��R S
)
��S T
{
�� 	
return
�� 
DACConsulta
�� 
.
�� .
 CensoConsultaReportesActividades
�� ?
(
��? @
)
��@ A
;
��A B
}
�� 	
public
�� 
Int32
�� '
InsertarLogCensoReingreso
�� .
(
��. /"
log_censo_reingresos
��/ C
OBJ
��D G
,
��G H
ref
��I L 
MessageResponseOBJ
��M _
MsgRes
��` f
)
��f g
{
�� 	
return
�� 

DACInserta
�� 
.
�� '
InsertarLogCensoReingreso
�� 7
(
��7 8
OBJ
��8 ;
,
��; <
ref
��= @
MsgRes
��A G
)
��G H
;
��H I
}
�� 	
public
�� 
void
�� (
ActualizarFechaEgresoCenso
�� .
(
��. /

ecop_censo
��/ 9
OBJ
��: =
,
��= >
ref
��? B 
MessageResponseOBJ
��C U
MsgRes
��V \
)
��\ ]
{
�� 	
DACActualiza
�� 
.
�� (
ActualizarFechaEgresoCenso
�� 3
(
��3 4
OBJ
��4 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
int
�� ,
ActualizarCensoSacarDelTablero
�� 1
(
��1 2

ecop_censo
��2 <
censo
��= B
)
��B C
{
�� 	
return
�� 
DACActualiza
�� 
.
��  ,
ActualizarCensoSacarDelTablero
��  >
(
��> ?
censo
��? D
)
��D E
;
��E F
}
�� 	
public
�� 
void
�� %
ActualizarLeEgresoCenso
�� +
(
��+ ,

ecop_censo
��, 6
OBJ
��7 :
,
��: ;
ref
��< ? 
MessageResponseOBJ
��@ R
MsgRes
��S Y
)
��Y Z
{
�	�	 	
DACActualiza
�	�	 
.
�	�	 %
ActualizarLeEgresoCenso
�	�	 0
(
�	�	0 1
OBJ
�	�	1 4
,
�	�	4 5
ref
�	�	6 9
MsgRes
�	�	: @
)
�	�	@ A
;
�	�	A B
}
�	�	 	
public
�	�	 
void
�	�	 #
ActualizarEgresoConcu
�	�	 )
(
�	�	) *
ecop_concurrencia
�	�	* ;
OBJ
�	�	< ?
,
�	�	? @
ref
�	�	A D 
MessageResponseOBJ
�	�	E W
MsgRes
�	�	X ^
)
�	�	^ _
{
�	�	 	
DACActualiza
�	�	 
.
�	�	 #
ActualizarEgresoConcu
�	�	 .
(
�	�	. /
OBJ
�	�	/ 2
,
�	�	2 3
ref
�	�	4 7
MsgRes
�	�	8 >
)
�	�	> ?
;
�	�	? @
}
�	�	 	
public
�	�	 
void
�	�	 #
ActualizarCensoEgreso
�	�	 )
(
�	�	) *

ecop_censo
�	�	* 4 
ActualizaSiniestro
�	�	5 G
,
�	�	G H
ref
�	�	I L 
MessageResponseOBJ
�	�	M _
MsgRes
�	�	` f
)
�	�	f g
{
�	�	 	
DACActualiza
�	�	 
.
�	�	 #
ActualizarCensoEgreso
�	�	 .
(
�	�	. / 
ActualizaSiniestro
�	�	/ A
,
�	�	A B
ref
�	�	C F
MsgRes
�	�	G M
)
�	�	M N
;
�	�	N O
}
�	�	 	
public
�	�	 
void
�	�	 %
ActualizarCensoEgresoOK
�	�	 +
(
�	�	+ ,

ecop_censo
�	�	, 6 
ActualizaSiniestro
�	�	7 I
,
�	�	I J
ref
�	�	K N 
MessageResponseOBJ
�	�	O a
MsgRes
�	�	b h
)
�	�	h i
{
�	�	 	
DACActualiza
�	�	 
.
�	�	 %
ActualizarCensoEgresoOK
�	�	 0
(
�	�	0 1 
ActualizaSiniestro
�	�	1 C
,
�	�	C D
ref
�	�	E H
MsgRes
�	�	I O
)
�	�	O P
;
�	�	P Q
}
�	�	 	
public
�	�	 
void
�	�	 +
ActualizaEgresoConcurrenciaOk
�	�	 1
(
�	�	1 2
ecop_concurrencia
�	�	2 C
ObjConcurrencia
�	�	D S
,
�	�	S T
ref
�	�	U X 
MessageResponseOBJ
�	�	Y k
MsgRes
�	�	l r
)
�	�	r s
{
�	�	 	
DACActualiza
�	�	 
.
�	�	 +
ActualizaEgresoConcurrenciaOk
�	�	 6
(
�	�	6 7
ObjConcurrencia
�	�	7 F
,
�	�	F G
ref
�	�	H K
MsgRes
�	�	L R
)
�	�	R S
;
�	�	S T
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 #
VW_base_beneficiarios
�	�	 )
>
�	�	) *$
BeneficiariosDocumento
�	�	+ A
(
�	�	A B
String
�	�	B H
	Documento
�	�	I R
,
�	�	R S
ref
�	�	T W 
MessageResponseOBJ
�	�	X j
MsgRes
�	�	k q
)
�	�	q r
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 $
BeneficiariosDocumento
�	�	 5
(
�	�	5 6
	Documento
�	�	6 ?
,
�	�	? @
ref
�	�	A D
MsgRes
�	�	E K
)
�	�	K L
;
�	�	L M
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 '
vw_tablero_levante_egreso
�	�	 -
>
�	�	- .
GetlevanteEgreso
�	�	/ ?
(
�	�	? @
String
�	�	@ F
	Documento
�	�	G P
,
�	�	P Q
ref
�	�	R U 
MessageResponseOBJ
�	�	V h
MsgRes
�	�	i o
)
�	�	o p
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 
GetlevanteEgreso
�	�	 /
(
�	�	/ 0
	Documento
�	�	0 9
,
�	�	9 :
ref
�	�	; >
MsgRes
�	�	? E
)
�	�	E F
;
�	�	F G
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 #
VW_base_beneficiarios
�	�	 )
>
�	�	) *
GetBeneficiarios
�	�	+ ;
(
�	�	; <
string
�	�	< B
term
�	�	C G
,
�	�	G H
ref
�	�	I L 
MessageResponseOBJ
�	�	M _
MsgRes
�	�	` f
)
�	�	f g
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 
GetBeneficiarios
�	�	 /
(
�	�	/ 0
term
�	�	0 4
,
�	�	4 5
ref
�	�	6 9
MsgRes
�	�	: @
)
�	�	@ A
;
�	�	A B
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	  
base_beneficiarios
�	�	 &
>
�	�	& '$
GetUltimoBeneficiarios
�	�	( >
(
�	�	> ?
string
�	�	? E
	documento
�	�	F O
,
�	�	O P
string
�	�	Q W
tipo
�	�	X \
,
�	�	\ ]
ref
�	�	^ a 
MessageResponseOBJ
�	�	b t
MsgRes
�	�	u {
)
�	�	{ |
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 $
GetUltimoBeneficiarios
�	�	 5
(
�	�	5 6
	documento
�	�	6 ?
,
�	�	? @
tipo
�	�	A E
,
�	�	E F
ref
�	�	G J
MsgRes
�	�	K Q
)
�	�	Q R
;
�	�	R S
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 
vw_consulta_censo
�	�	 %
>
�	�	% &
ConsultaCenso
�	�	' 4
(
�	�	4 5
ref
�	�	5 8 
MessageResponseOBJ
�	�	9 K
MsgRes
�	�	L R
)
�	�	R S
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 
ConsultaCenso
�	�	 ,
(
�	�	, -
ref
�	�	- 0
MsgRes
�	�	1 7
)
�	�	7 8
;
�	�	8 9
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 
vw_consulta_ecop
�	�	 $
>
�	�	$ %'
ConsultaCensoConcurrencia
�	�	& ?
(
�	�	? @
ref
�	�	@ C 
MessageResponseOBJ
�	�	D V
MsgRes
�	�	W ]
)
�	�	] ^
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 '
ConsultaCensoConcurrencia
�	�	 8
(
�	�	8 9
ref
�	�	9 <
MsgRes
�	�	= C
)
�	�	C D
;
�	�	D E
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 ,
Management_Consulta_EcopResult
�	�	 2
>
�	�	2 3)
ConsultaCensoConcurrenciaII
�	�	4 O
(
�	�	O P
int
�	�	P S
tipo
�	�	T X
,
�	�	X Y
int
�	�	Z ]
?
�	�	] ^
regional
�	�	_ g
,
�	�	g h
string
�	�	i o
	documento
�	�	p y
,
�	�	y z
DateTime�	�	{ �
?�	�	� �
fechaInicio�	�	� �
,�	�	� �
DateTime�	�	� �
?�	�	� �

fechaFinal�	�	� �
,�	�	� �
ref�	�	� �"
MessageResponseOBJ�	�	� �
MsgRes�	�	� �
)�	�	� �
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 )
ConsultaCensoConcurrenciaII
�	�	 :
(
�	�	: ;
tipo
�	�	; ?
,
�	�	? @
regional
�	�	A I
,
�	�	I J
	documento
�	�	K T
,
�	�	T U
fechaInicio
�	�	V a
,
�	�	a b

fechaFinal
�	�	c m
,
�	�	m n
ref
�	�	o r
MsgRes
�	�	s y
)
�	�	y z
;
�	�	z {
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 -
Management_Consulta_Ecop2Result
�	�	 3
>
�	�	3 4*
ConsultaCensoConcurrenciaII2
�	�	5 Q
(
�	�	Q R
int
�	�	R U
tipo
�	�	V Z
,
�	�	Z [
int
�	�	\ _
?
�	�	_ `
regional
�	�	a i
,
�	�	i j
string
�	�	k q
	documento
�	�	r {
,
�	�	{ |
DateTime�	�	} �
?�	�	� �
fechaInicio�	�	� �
,�	�	� �
DateTime�	�	� �
?�	�	� �

fechaFinal�	�	� �
,�	�	� �
ref�	�	� �"
MessageResponseOBJ�	�	� �
MsgRes�	�	� �
)�	�	� �
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 *
ConsultaCensoConcurrenciaII2
�	�	 ;
(
�	�	; <
tipo
�	�	< @
,
�	�	@ A
regional
�	�	B J
,
�	�	J K
	documento
�	�	L U
,
�	�	U V
fechaInicio
�	�	W b
,
�	�	b c

fechaFinal
�	�	d n
,
�	�	n o
ref
�	�	p s
MsgRes
�	�	t z
)
�	�	z {
;
�	�	{ |
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 *
vw_consulta_pacientesActivos
�	�	 0
>
�	�	0 1&
ConsultaPacientesActivos
�	�	2 J
(
�	�	J K
)
�	�	K L
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 &
ConsultaPacientesActivos
�	�	 7
(
�	�	7 8
)
�	�	8 9
;
�	�	9 :
}
�	�	 	
public
�	�	 
void
�	�	 
CensoEgreso
�	�	 
(
�	�	  

ecop_censo
�	�	  * 
ActualizaSiniestro
�	�	+ =
,
�	�	= >
ref
�	�	? B 
MessageResponseOBJ
�	�	C U
MsgRes
�	�	V \
)
�	�	\ ]
{
�	�	 	
DACActualiza
�	�	 
.
�	�	 
CensoEgreso
�	�	 $
(
�	�	$ % 
ActualizaSiniestro
�	�	% 7
,
�	�	7 8
ref
�	�	9 <
MsgRes
�	�	= C
)
�	�	C D
;
�	�	D E
}
�	�	 	
public
�	�	 
void
�	�	 #
ActualizarEgresoCenso
�	�	 )
(
�	�	) *

ecop_censo
�	�	* 4 
ActualizaSiniestro
�	�	5 G
,
�	�	G H
ref
�	�	I L 
MessageResponseOBJ
�	�	M _
MsgRes
�	�	` f
)
�	�	f g
{
�	�	 	
DACActualiza
�	�	 
.
�	�	 #
ActualizarEgresoCenso
�	�	 .
(
�	�	. / 
ActualizaSiniestro
�	�	/ A
,
�	�	A B
ref
�	�	C F
MsgRes
�	�	G M
)
�	�	M N
;
�	�	N O
}
�	�	 	
public
�	�	 
void
�	�	 -
ActualizarFechaEgresoConcucenso
�	�	 3
(
�	�	3 4

ecop_censo
�	�	4 >
censo
�	�	? D
,
�	�	D E
int
�	�	F I
idconcu
�	�	J Q
,
�	�	Q R
ref
�	�	S V 
MessageResponseOBJ
�	�	W i
MsgRes
�	�	j p
)
�	�	p q
{
�	�	 	
DACActualiza
�	�	 
.
�	�	 -
ActualizarFechaEgresoConcucenso
�	�	 8
(
�	�	8 9
censo
�	�	9 >
,
�	�	> ?
idconcu
�	�	@ G
,
�	�	G H
ref
�	�	I L
MsgRes
�	�	M S
)
�	�	S T
;
�	�	T U
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 /
!management_egresBuscar_megaResult
�	�	 5
>
�	�	5 6
BuscarMegaEgreso
�	�	7 G
(
�	�	G H
string
�	�	H N
	documento
�	�	O X
)
�	�	X Y
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 
BuscarMegaEgreso
�	�	 /
(
�	�	/ 0
	documento
�	�	0 9
)
�	�	9 :
;
�	�	: ;
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 %
ref_censo_caso_especial
�	�	 +
>
�	�	+ ,'
listaCensoCasosEspeciales
�	�	- F
(
�	�	F G
)
�	�	G H
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 '
listaCensoCasosEspeciales
�	�	 8
(
�	�	8 9
)
�	�	9 :
;
�	�	: ;
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 "
ref_censo_linea_pago
�	�	 (
>
�	�	( )"
listaCensoLineasPago
�	�	* >
(
�	�	> ?
)
�	�	? @
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 "
listaCensoLineasPago
�	�	 3
(
�	�	3 4
)
�	�	4 5
;
�	�	5 6
}
�	�	 	
public
�	�	 5
'management_censo_ultimaHabitacionResult
�	�	 6
datosEgreso
�	�	7 B
(
�	�	B C
int
�	�	C F
?
�	�	F G
idCenso
�	�	H O
)
�	�	O P
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 
datosEgreso
�	�	 *
(
�	�	* +
idCenso
�	�	+ 2
)
�	�	2 3
;
�	�	3 4
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 
ecop_concurrencia
�	�	 %
>
�	�	% &)
ConsultaAfiliadoConcurrenia
�	�	' B
(
�	�	B C
ecop_concurrencia
�	�	C T
ObjAfiliado
�	�	U `
,
�	�	` a
ref
�	�	b e 
MessageResponseOBJ
�	�	f x
MsgRes
�	�	y 
)�	�	 �
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 )
ConsultaAfiliadoConcurrenia
�	�	 :
(
�	�	: ;
ObjAfiliado
�	�	; F
,
�	�	F G
ref
�	�	H K
MsgRes
�	�	L R
)
�	�	R S
;
�	�	S T
}
�	�	 	
public
�	�	 
List
�	�	 
<
�	�	 
ecop_concurrencia
�	�	 %
>
�	�	% &#
ConsultaIdConcurrenia
�	�	' <
(
�	�	< =
ecop_concurrencia
�	�	= N
ObjAfiliado
�	�	O Z
,
�	�	Z [
ref
�	�	\ _ 
MessageResponseOBJ
�	�	` r
MsgRes
�	�	s y
)
�	�	y z
{
�	�	 	
return
�	�	 
DACConsulta
�	�	 
.
�	�	 #
ConsultaIdConcurrenia
�	�	 4
(
�	�	4 5
ObjAfiliado
�	�	5 @
,
�	�	@ A
ref
�	�	B E
MsgRes
�	�	F L
)
�	�	L M
;
�	�	M N
}
�	�	 	
public
�	�	 
ecop_concurrencia
�	�	  $
ConsultaConcurrenciaId
�	�	! 7
(
�	�	7 8
int
�	�	8 ;
idconcurrencia
�	�	< J
)
�	�	J K
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 $
ConsultaConcurrenciaId
�
�
 5
(
�
�
5 6
idconcurrencia
�
�
6 D
)
�
�
D E
;
�
�
E F
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 
ecop_concurrencia
�
�
 %
>
�
�
% &)
ConsultaConcurrenciaIdLista
�
�
' B
(
�
�
B C
Int32
�
�
C H
IdConcu
�
�
I P
,
�
�
P Q
ref
�
�
R U 
MessageResponseOBJ
�
�
V h
MsgRes
�
�
i o
)
�
�
o p
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 )
ConsultaConcurrenciaIdLista
�
�
 :
(
�
�
: ;
IdConcu
�
�
; B
,
�
�
B C
ref
�
�
D G
MsgRes
�
�
H N
)
�
�
N O
;
�
�
O P
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 

ecop_censo
�
�
 
>
�
�
 "
ConsultaCensoIdLista
�
�
  4
(
�
�
4 5
Int32
�
�
5 :
IdCenso
�
�
; B
,
�
�
B C
ref
�
�
D G 
MessageResponseOBJ
�
�
H Z
MsgRes
�
�
[ a
)
�
�
a b
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 "
ConsultaCensoIdLista
�
�
 3
(
�
�
3 4
IdCenso
�
�
4 ;
,
�
�
; <
ref
�
�
= @
MsgRes
�
�
A G
)
�
�
G H
;
�
�
H I
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 (
vw_ecop_cohortes_evolucion
�
�
 .
>
�
�
. /
ConsultaCohortes
�
�
0 @
(
�
�
@ A(
vw_ecop_cohortes_evolucion
�
�
A [
ObjAfiliado
�
�
\ g
,
�
�
g h
ref
�
�
i l 
MessageResponseOBJ
�
�
m 
MsgRes�
�
� �
)�
�
� �
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 
ConsultaCohortes
�
�
 /
(
�
�
/ 0
ObjAfiliado
�
�
0 ;
,
�
�
; <
ref
�
�
= @
MsgRes
�
�
A G
)
�
�
G H
;
�
�
H I
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 &
vw_tipo_habitacion_censo
�
�
 ,
>
�
�
, -$
ConsultaTipoHabitacion
�
�
. D
(
�
�
D E&
vw_tipo_habitacion_censo
�
�
E ]
ObjAfiliado
�
�
^ i
,
�
�
i j
ref
�
�
k n!
MessageResponseOBJ�
�
o �
MsgRes�
�
� �
)�
�
� �
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 $
ConsultaTipoHabitacion
�
�
 5
(
�
�
5 6
ObjAfiliado
�
�
6 A
,
�
�
A B
ref
�
�
C F
MsgRes
�
�
G M
)
�
�
M N
;
�
�
N O
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 
vw_ciudad_ips
�
�
 !
>
�
�
! ")
ConsultaIdConcurreniaciudad
�
�
# >
(
�
�
> ?
vw_ciudad_ips
�
�
? L
ObjAfiliado
�
�
M X
,
�
�
X Y
ref
�
�
Z ] 
MessageResponseOBJ
�
�
^ p
MsgRes
�
�
q w
)
�
�
w x
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 )
ConsultaIdConcurreniaciudad
�
�
 :
(
�
�
: ;
ObjAfiliado
�
�
; F
,
�
�
F G
ref
�
�
H K
MsgRes
�
�
L R
)
�
�
R S
;
�
�
S T
}
�
�
 	
public
�
�
 
void
�
�
 #
ActualizaConcurrencia
�
�
 )
(
�
�
) *
ecop_concurrencia
�
�
* ;
ObjConcurrencia
�
�
< K
,
�
�
K L
String
�
�
M S
User
�
�
T X
,
�
�
X Y
String
�
�
Z `
	IPAddress
�
�
a j
,
�
�
j k
ref
�
�
l o!
MessageResponseOBJ�
�
p �
MsgRes�
�
� �
)�
�
� �
{
�
�
 	
DACActualiza
�
�
 
.
�
�
 #
ActualizaConcurrencia
�
�
 .
(
�
�
. /
ObjConcurrencia
�
�
/ >
,
�
�
> ?
User
�
�
@ D
,
�
�
D E
	IPAddress
�
�
F O
,
�
�
O P
ref
�
�
Q T
MsgRes
�
�
U [
)
�
�
[ \
;
�
�
\ ]
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 
ecop_concurrencia
�
�
 %
>
�
�
% &&
GetconcurrenciaAfiliados
�
�
' ?
(
�
�
? @
string
�
�
@ F
numidafiliado
�
�
G T
)
�
�
T U
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 &
GetconcurrenciaAfiliados
�
�
 7
(
�
�
7 8
numidafiliado
�
�
8 E
)
�
�
E F
;
�
�
F G
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 
Ref_ips
�
�
 
>
�
�
 
	GetRefIps
�
�
 &
(
�
�
& '
)
�
�
' (
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 
	GetRefIps
�
�
 (
(
�
�
( )
)
�
�
) *
;
�
�
* +
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 
ref_eps
�
�
 
>
�
�
 
	GetRefEps
�
�
 &
(
�
�
& '
)
�
�
' (
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 
	GetRefEps
�
�
 (
(
�
�
( )
)
�
�
) *
;
�
�
* +
}
�
�
 	
public
�
�
 
void
�
�
 )
ActualizaEgresoConcurrencia
�
�
 /
(
�
�
/ 0
ecop_concurrencia
�
�
0 A
ObjConcurrencia
�
�
B Q
,
�
�
Q R
String
�
�
S Y
User
�
�
Z ^
,
�
�
^ _
String
�
�
` f
	IPAddress
�
�
g p
,
�
�
p q
ref
�
�
r u!
MessageResponseOBJ�
�
v �
MsgRes�
�
� �
)�
�
� �
{
�
�
 	
DACActualiza
�
�
 
.
�
�
 )
ActualizaEgresoConcurrencia
�
�
 4
(
�
�
4 5
ObjConcurrencia
�
�
5 D
,
�
�
D E
User
�
�
F J
,
�
�
J K
	IPAddress
�
�
L U
,
�
�
U V
ref
�
�
W Z
MsgRes
�
�
[ a
)
�
�
a b
;
�
�
b c
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 
ecop_concurrencia
�
�
 %
>
�
�
% &0
"ConsultaCriterioIngresoActualizado
�
�
' I
(
�
�
I J
Int32
�
�
J O
IdConcu
�
�
P W
,
�
�
W X
ref
�
�
Y \ 
MessageResponseOBJ
�
�
] o
MsgRes
�
�
p v
)
�
�
v w
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 0
"ConsultaCriterioIngresoActualizado
�
�
 A
(
�
�
A B
IdConcu
�
�
B I
,
�
�
I J
ref
�
�
K N
MsgRes
�
�
O U
)
�
�
U V
;
�
�
V W
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 4
&ecop_concurrencia_encuesta_satisfacion
�
�
 :
>
�
�
: ;%
ConsultaEncuestaCargada
�
�
< S
(
�
�
S T
Int32
�
�
T Y
IdConcu
�
�
Z a
,
�
�
a b
ref
�
�
c f 
MessageResponseOBJ
�
�
g y
MsgRes�
�
z �
)�
�
� �
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 %
ConsultaEncuestaCargada
�
�
 6
(
�
�
6 7
IdConcu
�
�
7 >
,
�
�
> ?
ref
�
�
@ C
MsgRes
�
�
D J
)
�
�
J K
;
�
�
K L
}
�
�
 	
public
�
�
 
int
�
�
 
InsertaEgreso
�
�
  
(
�
�
  !+
egreso_auditoria_Hospitalaria
�
�
! >
Egreso
�
�
? E
,
�
�
E F
String
�
�
G M
UserName
�
�
N V
,
�
�
V W
String
�
�
X ^
	IPAddress
�
�
_ h
,
�
�
h i
ref
�
�
j m!
MessageResponseOBJ�
�
n �
MsgRes�
�
� �
)�
�
� �
{
�
�
 	
return
�
�
 

DACInserta
�
�
 
.
�
�
 
InsertaEgreso
�
�
 +
(
�
�
+ ,
Egreso
�
�
, 2
,
�
�
2 3
UserName
�
�
4 <
,
�
�
< =
	IPAddress
�
�
> G
,
�
�
G H
ref
�
�
I L
MsgRes
�
�
M S
)
�
�
S T
;
�
�
T U
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 0
"vw_concurrencia_evolucion_Contrato
�
�
 6
>
�
�
6 7,
ConsultaIdConcurreniaEvolucion
�
�
8 V
(
�
�
V W
ecop_concurrencia
�
�
W h
ObjAfiliado
�
�
i t
,
�
�
t u
ref
�
�
v y!
MessageResponseOBJ�
�
z �
MsgRes�
�
� �
)�
�
� �
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 ,
ConsultaIdConcurreniaEvolucion
�
�
 =
(
�
�
= >
ObjAfiliado
�
�
> I
,
�
�
I J
ref
�
�
K N
MsgRes
�
�
O U
)
�
�
U V
;
�
�
V W
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 &
vw_consulta_concurrencia
�
�
 ,
>
�
�
, -"
ConsultaConcurrencia
�
�
. B
(
�
�
B C
ref
�
�
C F 
MessageResponseOBJ
�
�
G Y
MsgRes
�
�
Z `
)
�
�
` a
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 "
ConsultaConcurrencia
�
�
 3
(
�
�
3 4
ref
�
�
4 7
MsgRes
�
�
8 >
)
�
�
> ?
;
�
�
? @
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
  
vw_consulta_egreso
�
�
 &
>
�
�
& '
ConsultaEgreso
�
�
( 6
(
�
�
6 7
ref
�
�
7 : 
MessageResponseOBJ
�
�
; M
MsgRes
�
�
N T
)
�
�
T U
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 
ConsultaEgreso
�
�
 -
(
�
�
- .
ref
�
�
. 1
MsgRes
�
�
2 8
)
�
�
8 9
;
�
�
9 :
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 0
"managment_vw_consulta_egresoResult
�
�
 6
>
�
�
6 7
ConsultaEgreso2
�
�
8 G
(
�
�
G H
ref
�
�
H K 
MessageResponseOBJ
�
�
L ^
MsgRes
�
�
_ e
)
�
�
e f
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 
ConsultaEgreso2
�
�
 .
(
�
�
. /
ref
�
�
/ 2
MsgRes
�
�
3 9
)
�
�
9 :
;
�
�
: ;
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 *
vw_consulta_eventos_adversos
�
�
 0
>
�
�
0 1
ConsultaEventosAd
�
�
2 C
(
�
�
C D
ref
�
�
D G 
MessageResponseOBJ
�
�
H Z
MsgRes
�
�
[ a
)
�
�
a b
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 
ConsultaEventosAd
�
�
 0
(
�
�
0 1
ref
�
�
1 4
MsgRes
�
�
5 ;
)
�
�
; <
;
�
�
< =
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 +
vw_consulta_situacion_calidad
�
�
 1
>
�
�
1 2"
ConsultaSituacionCal
�
�
3 G
(
�
�
G H
ref
�
�
H K 
MessageResponseOBJ
�
�
L ^
MsgRes
�
�
_ e
)
�
�
e f
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 "
ConsultaSituacionCal
�
�
 3
(
�
�
3 4
ref
�
�
4 7
MsgRes
�
�
8 >
)
�
�
> ?
;
�
�
? @
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 
vw_gestantes
�
�
  
>
�
�
  !
ConsultaGestantes
�
�
" 3
(
�
�
3 4
ref
�
�
4 7 
MessageResponseOBJ
�
�
8 J
MsgRes
�
�
K Q
)
�
�
Q R
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 
ConsultaGestantes
�
�
 0
(
�
�
0 1
ref
�
�
1 4
MsgRes
�
�
5 ;
)
�
�
; <
;
�
�
< =
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 /
!management_controlNatalidadResult
�
�
 5
>
�
�
5 6$
ConsultaGestantesNuevo
�
�
7 M
(
�
�
M N
ref
�
�
N Q 
MessageResponseOBJ
�
�
R d
MsgRes
�
�
e k
)
�
�
k l
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 $
ConsultaGestantesNuevo
�
�
 5
(
�
�
5 6
ref
�
�
6 9
MsgRes
�
�
: @
)
�
�
@ A
;
�
�
A B
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 
vw_gestantes_sin
�
�
 $
>
�
�
$ %"
ConsultaGestantesSin
�
�
& :
(
�
�
: ;
ref
�
�
; > 
MessageResponseOBJ
�
�
? Q
MsgRes
�
�
R X
)
�
�
X Y
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 "
ConsultaGestantesSin
�
�
 3
(
�
�
3 4
ref
�
�
4 7
MsgRes
�
�
8 >
)
�
�
> ?
;
�
�
? @
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 
vw_Mortalidad
�
�
 !
>
�
�
! " 
ConsultaMortalidad
�
�
# 5
(
�
�
5 6
ref
�
�
6 9 
MessageResponseOBJ
�
�
: L
MsgRes
�
�
M S
)
�
�
S T
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
  
ConsultaMortalidad
�
�
 1
(
�
�
1 2
ref
�
�
2 5
MsgRes
�
�
6 <
)
�
�
< =
;
�
�
= >
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 <
.ManagementConsultaConcuMortalidadCon_SinResult
�
�
 B
>
�
�
B C"
ConsultaMortalidadV2
�
�
D X
(
�
�
X Y
int
�
�
Y \
tipoconsulta
�
�
] i
,
�
�
i j
ref
�
�
k n!
MessageResponseOBJ�
�
o �
MsgRes�
�
� �
)�
�
� �
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 "
ConsultaMortalidadV2
�
�
 3
(
�
�
3 4
tipoconsulta
�
�
4 @
,
�
�
@ A
ref
�
�
B E
MsgRes
�
�
F L
)
�
�
L M
;
�
�
M N
}
�
�
 	
public
�
�
 
List
�
�
 
<
�
�
 
vw_Mortalidad_sin
�
�
 %
>
�
�
% &#
ConsultaMortalidadSin
�
�
' <
(
�
�
< =
ref
�
�
= @ 
MessageResponseOBJ
�
�
A S
MsgRes
�
�
T Z
)
�
�
Z [
{
�
�
 	
return
�
�
 
DACConsulta
�
�
 
.
�
�
 #
ConsultaMortalidadSin
�
�
 4
(
�
�
4 5
ref
�
�
5 8
MsgRes
�
�
9 ?
)
�
�
? @
;
�
�
@ A
}
�
�
 	
public
�� 
void
�� *
InsertarEncuestaConcurrencia
�� 0
(
��0 1(
ecop_concurrencia_encuesta
��1 K
Encuesta
��L T
,
��T U
ref
��V Y 
MessageResponseOBJ
��Z l
MsgRes
��m s
)
��s t
{
�� 	

DACInserta
�� 
.
�� *
InsertarEncuestaConcurrencia
�� 3
(
��3 4
Encuesta
��4 <
,
��< =
ref
��> A
MsgRes
��B H
)
��H I
;
��I J
}
�� 	
public
�� 
List
�� 
<
�� +
egreso_auditoria_Hospitalaria
�� 1
>
��1 2
ConsultaAgresoH
��3 B
(
��B C
Int32
��C H
IdConcu
��I P
,
��P Q
ref
��R U 
MessageResponseOBJ
��V h
MsgRes
��i o
)
��o p
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaAgresoH
�� .
(
��. /
IdConcu
��/ 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
void
�� "
Actualizarprevenible
�� (
(
��( )
ecop_concurrencia
��) :
Objconcurrencia
��; J
,
��J K
ref
��L O 
MessageResponseOBJ
��P b
MsgRes
��c i
)
��i j
{
�� 	
DACActualiza
�� 
.
�� "
Actualizarprevenible
�� -
(
��- .
Objconcurrencia
��. =
,
��= >
ref
��? B
MsgRes
��C I
)
��I J
;
��J K
}
�� 	
public
�� 
List
�� 
<
�� /
!vw_max_concurrencia_por_documento
�� 5
>
��5 6.
 ConsultaMaxConcurrenciaDocumento
��7 W
(
��W X
String
��X ^
	Documento
��_ h
,
��h i
ref
��j m!
MessageResponseOBJ��n �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� .
 ConsultaMaxConcurrenciaDocumento
�� ?
(
��? @
	Documento
��@ I
,
��I J
ref
��K N
MsgRes
��O U
)
��U V
;
��V W
}
�� 	
public
�� 
void
�� 
ActualizarEgreso
�� $
(
��$ %+
egreso_auditoria_Hospitalaria
��% B
	Objegreso
��C L
,
��L M
ref
��N Q 
MessageResponseOBJ
��R d
MsgRes
��e k
)
��k l
{
�� 	
DACActualiza
�� 
.
�� 
ActualizarEgreso
�� )
(
��) *
	Objegreso
��* 3
,
��3 4
ref
��5 8
MsgRes
��9 ?
)
��? @
;
��@ A
}
�� 	
public
�� 
void
�� %
InsertarEgresoGestantes
�� +
(
��+ ,
egreso_gestantes
��, <
	Gestantes
��= F
,
��F G
ref
��H K 
MessageResponseOBJ
��L ^
MsgRes
��_ e
)
��e f
{
�� 	

DACInserta
�� 
.
�� %
InsertarEgresoGestantes
�� .
(
��. /
	Gestantes
��/ 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
List
�� 
<
�� 
egreso_gestantes
�� $
>
��$ %&
ConsultasEgresoGestantes
��& >
(
��> ?
Int32
��? D
id_concurrencia
��E T
,
��T U
ref
��V Y 
MessageResponseOBJ
��Z l
MsgRes
��m s
)
��s t
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
ConsultasEgresoGestantes
�� 7
(
��7 8
id_concurrencia
��8 G
,
��G H
ref
��I L
MsgRes
��M S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� +
vw_ecop_egresos_hospitalarios
�� 1
>
��1 2
GetListaEgresos
��3 B
(
��B C
DateTime
��C K
?
��K L
fechainicial
��M Y
,
��Y Z
DateTime
��[ c
?
��c d

fechafinal
��e o
)
��o p
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetListaEgresos
�� .
(
��. /
fechainicial
��/ ;
,
��; <

fechafinal
��= G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� 9
+management_ecop_egresos_hospitalariosResult
�� ?
>
��? @)
listadoEgresosHospitalarios
��A \
(
��\ ]
DateTime
��] e
?
��e f
fechainicial
��g s
,
��s t
DateTime
��u }
?
��} ~

fechafinal�� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
listadoEgresosHospitalarios
�� :
(
��: ;
fechainicial
��; G
,
��G H

fechafinal
��I S
)
��S T
;
��T U
}
�� 	
public
�� 
ecop_concurrencia
��  0
"traerDatosBeneficiarioConcurrencia
��! C
(
��C D
string
��D J
documentoBen
��K W
)
��W X
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 0
"traerDatosBeneficiarioConcurrencia
�� A
(
��A B
documentoBen
��B N
)
��N O
;
��O P
}
�� 	
public
�� 
List
�� 
<
�� 8
*ecop_concurrencia_evolucion_procedimientos
�� >
>
��> ?/
!traerDatosEvolucionProcedimientos
��@ a
(
��a b
int
��b e
id_evolucion
��f r
)
��r s
{
�� 	
return
�� 
DACConsulta
�� 
.
�� /
!traerDatosEvolucionProcedimientos
�� @
(
��@ A
id_evolucion
��A M
)
��M N
;
��N O
}
�� 	
public
�� 
List
�� 
<
�� &
ref_tipo_hospitalizacion
�� ,
>
��, -'
GetRefTipoHospitalizacion
��. G
(
��G H
)
��H I
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
GetRefTipoHospitalizacion
�� 8
(
��8 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
List
�� 
<
�� -
ref_tipo_patologia_catastrofica
�� 3
>
��3 4-
GetRefTipoPatologiaCatastrofica
��5 T
(
��T U
)
��U V
{
�� 	
return
�� 
DACConsulta
�� 
.
�� -
GetRefTipoPatologiaCatastrofica
�� >
(
��> ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
�� 1
#ref_pertinencia_estancia_prolongada
�� 7
>
��7 8)
GetRefPertinenciaProlongada
��9 T
(
��T U
)
��U V
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
GetRefPertinenciaProlongada
�� :
(
��: ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� /
!ref_condicion_estancia_prolongada
�� 5
>
��5 6'
GetRefCondicionProlongada
��7 P
(
��P Q
)
��Q R
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
GetRefCondicionProlongada
�� 8
(
��8 9
)
��9 :
;
��: ;
}
�� 	
public
�� 0
"categorizacion_egreso_hospitalario
�� 1
getcatbyegreso
��2 @
(
��@ A
int
��A D
idegreso
��E M
)
��M N
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
getcatbyegreso
�� -
(
��- .
idegreso
��. 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
void
�� $
insertarcategorizacion
�� *
(
��* +0
"categorizacion_egreso_hospitalario
��+ M
obj
��N Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
{
�� 	

DACInserta
�� 
.
�� $
insertarcategorizacion
�� -
(
��- .
obj
��. 1
,
��1 2
ref
��3 6
MsgRes
��7 =
)
��= >
;
��> ?
}
�� 	
public
�� 
List
�� 
<
�� 8
*management_egresos_verCategorizacionResult
�� >
>
��> ?,
traerDatosCategorizacionEgreso
��@ ^
(
��^ _
int
��_ b
idEgreso
��c k
)
��k l
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
traerDatosCategorizacionEgreso
�� =
(
��= >
idEgreso
��> F
)
��F G
;
��G H
}
�� 	
public
�� 
void
�� &
actualizarcategorizacion
�� ,
(
��, -0
"categorizacion_egreso_hospitalario
��- O
obj
��P S
,
��S T
ref
��U X 
MessageResponseOBJ
��Y k
MsgRes
��l r
)
��r s
{
�� 	
DACActualiza
�� 
.
�� &
actualizarcategorizacion
�� 1
(
��1 2
obj
��2 5
,
��5 6
ref
��7 :
MsgRes
��; A
)
��A B
;
��B C
}
�� 	
public
�� 
void
�� 
ActualizarIps
�� !
(
��! "
int
��" %
idconcurrencia
��& 4
,
��4 5
int
��6 9
idips
��: ?
,
��? @
ref
��A D 
MessageResponseOBJ
��E W
Msg
��X [
)
��[ \
{
�� 	
DACActualiza
�� 
.
�� 
ActualizarIps
�� &
(
��& '
idconcurrencia
��' 5
,
��5 6
idips
��7 <
,
��< =
ref
��> A
Msg
��B E
)
��E F
;
��F G
}
�� 	
public
�� 
List
�� 
<
�� 
ref_trimeste
��  
>
��  !
GetRefTrimestre
��" 1
(
��1 2
)
��2 3
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetRefTrimestre
�� .
(
��. /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
List
�� 
<
�� '
Ref_plan_mejora_categoria
�� -
>
��- .)
GetRefplan_mejora_categoria
��/ J
(
��J K
)
��K L
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
GetRefplan_mejora_categoria
�� :
(
��: ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� "
Ref_plan_mejora_foco
�� (
>
��( )%
GetRef_plan_mejora_foco
��* A
(
��A B
)
��B C
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
GetRef_plan_mejora_foco
�� 6
(
��6 7
)
��7 8
;
��8 9
}
�� 	
public
�� 
List
�� 
<
�� "
Ref_proceso_auditado
�� (
>
��( )%
GetRef_proceso_auditado
��* A
(
��A B
)
��B C
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
GetRef_proceso_auditado
�� 6
(
��6 7
)
��7 8
;
��8 9
}
�� 	
public
�� 
List
�� 
<
�� .
 management_planmejora_focoResult
�� 4
>
��4 5
Cuentadetallefoco
��6 G
(
��G H
Int32
��H M
id_plan_de_mejora
��N _
,
��_ `
ref
��a d 
MessageResponseOBJ
��e w
MsgRes
��x ~
)
��~ 
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
Cuentadetallefoco
�� 0
(
��0 1
id_plan_de_mejora
��1 B
,
��B C
ref
��D G
MsgRes
��H N
)
��N O
;
��O P
}
�� 	
public
�� 
Int32
��  
InsertarPlanMejora
�� '
(
��' (!
ecop_plan_de_mejora
��( ;
obj
��< ?
,
��? @
ref
��A D 
MessageResponseOBJ
��E W
MsgRes
��X ^
)
��^ _
{
�� 	
return
�� 

DACInserta
�� 
.
��  
InsertarPlanMejora
�� 0
(
��0 1
obj
��1 4
,
��4 5
ref
��6 9
MsgRes
��: @
)
��@ A
;
��A B
}
�� 	
public
�� 
Int32
�� $
InsertarPlanMejorafoco
�� +
(
��+ ,0
"ecop_plan_mejora_foco_intervencion
��, N
obj
��O R
,
��R S
ref
��T W 
MessageResponseOBJ
��X j
MsgRes
��k q
)
��q r
{
�� 	
return
�� 

DACInserta
�� 
.
�� $
InsertarPlanMejorafoco
�� 4
(
��4 5
obj
��5 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
void
�� !
EliminarDetallePlan
�� '
(
��' (
int
��( +.
 id_plan_mejora_foco_intervencion
��, L
,
��L M
ref
��N Q 
MessageResponseOBJ
��R d
MsgRes
��e k
)
��k l
{
�� 	

DACElimina
�� 
.
�� !
EliminarDetallePlan
�� *
(
��* +.
 id_plan_mejora_foco_intervencion
��+ K
,
��K L
ref
��M P
MsgRes
��Q W
)
��W X
;
��X Y
}
�� 	
public
�� 
List
�� 
<
�� /
!management_planmejora_tareaResult
�� 5
>
��5 6 
CuentadetalleTarea
��7 I
(
��I J
Int32
��J O.
 id_plan_mejora_foco_intervencion
��P p
,
��p q
ref
��r u!
MessageResponseOBJ��v �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
CuentadetalleTarea
�� 1
(
��1 2.
 id_plan_mejora_foco_intervencion
��2 R
,
��R S
ref
��T W
MsgRes
��X ^
)
��^ _
;
��_ `
}
�� 	
public
�� 
void
�� &
EliminarDetallePlanTarea
�� ,
(
��, -
int
��- 0#
id_plan_mejora_tareas
��1 F
,
��F G
ref
��H K 
MessageResponseOBJ
��L ^
MsgRes
��_ e
)
��e f
{
�� 	

DACElimina
�� 
.
�� &
EliminarDetallePlanTarea
�� /
(
��/ 0#
id_plan_mejora_tareas
��0 E
,
��E F
ref
��G J
MsgRes
��K Q
)
��Q R
;
��R S
}
�� 	
public
�� 
Int32
�� %
InsertarPlanMejoratarea
�� ,
(
��, -%
ecop_plan_mejora_tareas
��- D
obj
��E H
,
��H I
ref
��J M 
MessageResponseOBJ
��N `
MsgRes
��a g
)
��g h
{
�� 	
return
�� 

DACInserta
�� 
.
�� %
InsertarPlanMejoratarea
�� 5
(
��5 6
obj
��6 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
void
�� "
ActualizarPlanEgreso
�� (
(
��( )
int
��) ,
id_plan_mejora
��- ;
,
��; <
Int32
��= B
estado
��C I
,
��I J
ref
��K N 
MessageResponseOBJ
��O a
MsgRes
��b h
)
��h i
{
�� 	
DACActualiza
�� 
.
�� "
ActualizarPlanEgreso
�� -
(
��- .
id_plan_mejora
��. <
,
��< =
estado
��> D
,
��D E
ref
��F I
MsgRes
��J P
)
��P Q
;
��Q R
}
�� 	
public
�� 
List
�� 
<
�� 7
)management_planmejora_tarea_controlResult
�� =
>
��= >'
CuentadetalleTareacontrol
��? X
(
��X Y
Int32
��Y ^
id_plan_mejora
��_ m
,
��m n
ref
��o r!
MessageResponseOBJ��s �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
CuentadetalleTareacontrol
�� 8
(
��8 9
id_plan_mejora
��9 G
,
��G H
ref
��I L
MsgRes
��M S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� 2
$management_plan_mejora_tableroResult
�� 8
>
��8 9 
PlanTableroGeneral
��: L
(
��L M
)
��M N
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
PlanTableroGeneral
�� 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� 1
#management_planMejora_reporteResult
�� 7
>
��7 8
DatosPMReporte
��9 G
(
��G H
int
��H K
?
��K L
idPlan
��M S
)
��S T
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
DatosPMReporte
�� -
(
��- .
idPlan
��. 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� @
2management_planMejora_reporte_detalleCeacionResult
�� F
>
��F G+
DatosPMReporteDetalleCreacion
��H e
(
��e f
int
��f i
?
��i j
idPlan
��k q
)
��q r
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
DatosPMReporteDetalleCreacion
�� <
(
��< =
idPlan
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
List
�� 
<
�� ?
1management_planMejora_reporte_detalleCierreResult
�� E
>
��E F)
DatosPMReporteDetalleCierre
��G b
(
��b c
int
��c f
?
��f g
idPlan
��h n
)
��n o
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
DatosPMReporteDetalleCierre
�� :
(
��: ;
idPlan
��; A
)
��A B
;
��B C
}
�� 	
public
�� 
List
�� 
<
�� ;
-management_planMejora_tableroDocumentalResult
�� A
>
��A B&
DatosPMgestionDocumental
��C [
(
��[ \
int
��\ _
?
��_ `
regional
��a i
,
��i j
DateTime
��k s
?
��s t
fechaInicio��u �
,��� �
DateTime��� �
?��� �
fechaFin��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
DatosPMgestionDocumental
�� 7
(
��7 8
regional
��8 @
,
��@ A
fechaInicio
��B M
,
��M N
fechaFin
��O W
)
��W X
;
��X Y
}
�� 	
public
�� 
List
�� 
<
�� >
0management_planesMejora_reporteSeguimientoResult
�� D
>
��D E-
DatosPMgestionDocumentalReporte
��F e
(
��e f
int
��f i
?
��i j
regional
��k s
,
��s t
DateTime
��u }
?
��} ~
fechaInicio�� �
,��� �
DateTime��� �
?��� �
fechaFin��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� -
DatosPMgestionDocumentalReporte
�� >
(
��> ?
regional
��? G
,
��G H
fechaInicio
��I T
,
��T U
fechaFin
��V ^
)
��^ _
;
��_ `
}
�� 	
public
�� 
List
�� 
<
�� &
ref_planMejora_prioridad
�� ,
>
��, -
listaPrioridadPM
��. >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
listaPrioridadPM
�� /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� 7
)management_plan_mejora_tablero_dtllResult
�� =
>
��= >$
PlanTableroGeneralDtll
��? U
(
��U V
Int32
��V [
id_plan_de_mejora
��\ m
,
��m n
ref
��o r!
MessageResponseOBJ��s �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
PlanTableroGeneralDtll
�� 5
(
��5 6
id_plan_de_mejora
��6 G
,
��G H
ref
��I L
MsgRes
��M S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� 6
(management_planMejora_ampliacionesResult
�� <
>
��< =$
PlanMejoraAmpliaciones
��> T
(
��T U
int
��U X
?
��X Y
idPlan
��Z `
)
��` a
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
PlanMejoraAmpliaciones
�� 5
(
��5 6
idPlan
��6 <
)
��< =
;
��= >
}
�� 	
public
�� 
List
�� 
<
�� 8
*management_planMejora_DocumentosPlanResult
�� >
>
��> ?&
PlanMejoraArchivoporTipo
��@ X
(
��X Y
int
��Y \
?
��\ ]
idPlan
��^ d
,
��d e
int
��f i
?
��i j
tipo
��k o
)
��o p
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
PlanMejoraArchivoporTipo
�� 7
(
��7 8
idPlan
��8 >
,
��> ?
tipo
��@ D
)
��D E
;
��E F
}
�� 	
public
�� 
int
�� *
InsertarAmpliacionPlanMejora
�� /
(
��/ 0!
log_PM_ampliaciones
��0 C
obj
��D G
)
��G H
{
�� 	
return
�� 

DACInserta
�� 
.
�� *
InsertarAmpliacionPlanMejora
�� :
(
��: ;
obj
��; >
)
��> ?
;
��? @
}
�� 	
public
�� 
int
�� ,
ActualizarPlanEgresoAmpliacion
�� 1
(
��1 2
DateTime
��2 :
?
��: ;
fechaAmpliacion
��< K
,
��K L
string
��M S
obsAmpliacion
��T a
,
��a b
int
��c f
?
��f g
idPlan
��h n
)
��n o
{
�� 	
return
�� 
DACActualiza
�� 
.
��  ,
ActualizarPlanEgresoAmpliacion
��  >
(
��> ?
fechaAmpliacion
��? N
,
��N O
obsAmpliacion
��P ]
,
��] ^
idPlan
��_ e
)
��e f
;
��f g
}
�� 	
public
�� ,
ecop_plan_de_mejora_documental
�� -+
PlanMejoraGestionDocumentalId
��. K
(
��K L
int
��L O
?
��O P
idPlan
��Q W
,
��W X
int
��Y \
?
��\ ]
tipo
��^ b
)
��b c
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
PlanMejoraGestionDocumentalId
�� <
(
��< =
idPlan
��= C
,
��C D
tipo
��E I
)
��I J
;
��J K
}
�� 	
public
�� ,
ecop_plan_de_mejora_documental
�� -2
$PlanMejoraGestionDocumentalIdArchivo
��. R
(
��R S
int
��S V
?
��V W
	idArchivo
��X a
)
��a b
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 2
$PlanMejoraGestionDocumentalIdArchivo
�� C
(
��C D
	idArchivo
��D M
)
��M N
;
��N O
}
�� 	
public
�� 
void
�� 
EliminarArchivoPM
�� %
(
��% &
int
��& )
?
��) *
	idArchivo
��+ 4
,
��4 5
ref
��6 9 
MessageResponseOBJ
��: L
MsgRes
��M S
)
��S T
{
�� 	

DACElimina
�� 
.
�� 
EliminarArchivoPM
�� (
(
��( )
	idArchivo
��) 2
,
��2 3
ref
��4 7
MsgRes
��8 >
)
��> ?
;
��? @
}
�� 	
public
�� !
ecop_plan_de_mejora
�� "
PlanMejoraId
��# /
(
��/ 0
int
��0 3
?
��3 4
idPlan
��5 ;
)
��; <
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
PlanMejoraId
�� +
(
��+ ,
idPlan
��, 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� !
ref_medicion_riesgo
�� '
>
��' (&
PlanMejoraMedicionRiesgo
��) A
(
��A B
)
��B C
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
PlanMejoraMedicionRiesgo
�� 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� "
ref_costos_noCalidad
�� (
>
��( )'
PlanMejoraCostosNoCalidad
��* C
(
��C D
)
��D E
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
PlanMejoraCostosNoCalidad
�� 8
(
��8 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
List
�� 
<
�� 
ref_cobertura
�� !
>
��! "!
PlanMejoraCobertura
��# 6
(
��6 7
)
��7 8
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
PlanMejoraCobertura
�� 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� 3
%management_planmejora_tarea_obsResult
�� 9
>
��9 :
GetobsTareas
��; G
(
��G H
Int32
��H M#
id_plan_mejora_tareas
��N c
,
��c d
ref
��e h 
MessageResponseOBJ
��i {
MsgRes��| �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetobsTareas
�� +
(
��+ ,#
id_plan_mejora_tareas
��, A
,
��A B
ref
��C F
MsgRes
��G M
)
��M N
;
��N O
}
�� 	
public
�� 
Int32
�� $
InsertarPlanMejora_obs
�� +
(
��+ ,)
ecop_plan_mejora_obs_tareas
��, G
obj
��H K
,
��K L
ref
��M P 
MessageResponseOBJ
��Q c
MsgRes
��d j
)
��j k
{
�� 	
return
�� 

DACInserta
�� 
.
�� $
InsertarPlanMejora_obs
�� 4
(
��4 5
obj
��5 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
void
�� )
Actualizarplan_estado_tarea
�� /
(
��/ 0
int
��0 3#
id_plan_mejora_tareas
��4 I
,
��I J
Int32
��K P
estado
��Q W
,
��W X
ref
��Y \ 
MessageResponseOBJ
��] o
MsgRes
��p v
)
��v w
{
�� 	
DACActualiza
�� 
.
�� )
Actualizarplan_estado_tarea
�� 4
(
��4 5#
id_plan_mejora_tareas
��5 J
,
��J K
estado
��L R
,
��R S
ref
��T W
MsgRes
��X ^
)
��^ _
;
��_ `
}
�� 	
public
�� 
List
�� 
<
�� >
0management_planmejora_tarea_control_estadoResult
�� D
>
��D E-
CuentadetalleTareacontrolEstado
��F e
(
��e f
Int32
��f k
id_plan_mejora
��l z
,
��z {
ref
��| "
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� -
CuentadetalleTareacontrolEstado
�� >
(
��> ?
id_plan_mejora
��? M
,
��M N
ref
��O R
MsgRes
��S Y
)
��Y Z
;
��Z [
}
�� 	
public
�� 
Int32
�� +
InsertarPlanMejora_documentos
�� 2
(
��2 3,
ecop_plan_de_mejora_documental
��3 Q
obj
��R U
)
��U V
{
�� 	
return
�� 

DACInserta
�� 
.
�� +
InsertarPlanMejora_documentos
�� ;
(
��; <
obj
��< ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
�� 8
*management_planMejora_tableroVisitasResult
�� >
>
��> ?
DatosPMVisitas
��@ N
(
��N O
string
��O U
auditor
��V ]
)
��] ^
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
DatosPMVisitas
�� -
(
��- .
auditor
��. 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� *
vw_planMejora_tableroVisitas
�� 0
>
��0 1!
DatosPMVisitasRoles
��2 E
(
��E F
)
��F G
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
DatosPMVisitasRoles
�� 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_valor_ahorro
�� $
>
��$ %
GetRefValorAhorro
��& 7
(
��7 8
)
��8 9
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetRefValorAhorro
��! 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
void
�� *
InsertaConcurrenciaEvolucion
�� 0
(
��0 1)
ecop_concurrencia_evolucion
��1 L
	Evolucion
��M V
,
��V W
List
��X \
<
��\ ]9
*ecop_concurrencia_evolucion_procedimientos��] �
>��� �
lista��� �
,��� �
String��� �
UserName��� �
,��� �
String��� �
	IPAddress��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	

DACInserta
�� 
.
�� *
InsertaConcurrenciaEvolucion
�� 3
(
��3 4
	Evolucion
��4 =
,
��= >
lista
��? D
,
��D E
UserName
��F N
,
��N O
	IPAddress
��P Y
,
��Y Z
ref
��[ ^
MsgRes
��_ e
)
��e f
;
��f g
}
�� 	
public
�� 
List
�� 
<
�� )
ecop_concurrencia_evolucion
�� /
>
��/ 0!
ConsultaEvoluciones
��1 D
(
��D E)
ecop_concurrencia_evolucion
��E `
ObjEvolu
��a i
,
��i j
ref
��k n!
MessageResponseOBJ��o �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
ConsultaEvoluciones
�� 2
(
��2 3
ObjEvolu
��3 ;
,
��; <
ref
��= @
MsgRes
��A G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� 2
$vw_evo_ecop_concurrencia_evoluciones
�� 8
>
��8 9$
ConsultaEvolucionesIps
��: P
(
��P Q2
$vw_evo_ecop_concurrencia_evoluciones
��Q u
ObjEvolu
��v ~
,
��~ 
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
ConsultaEvolucionesIps
�� 5
(
��5 6
ObjEvolu
��6 >
,
��> ?
ref
��@ C
MsgRes
��D J
)
��J K
;
��K L
}
�� 	
public
�� 
void
�� +
EliminarConcurrenciaEvolucion
�� 1
(
��1 2)
ecop_concurrencia_evolucion
��2 M
ObjEvolucion
��N Z
,
��Z [
String
��\ b
UserName
��c k
,
��k l
String
��m s
	IPAddress
��t }
,
��} ~
ref�� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	

DACElimina
�� 
.
�� +
EliminarConcurrenciaEvolucion
�� 4
(
��4 5
ObjEvolucion
��5 A
,
��A B
UserName
��C K
,
��K L
	IPAddress
��M V
,
��V W
ref
��X [
MsgRes
��\ b
)
��b c
;
��c d
}
�� 	
public
�� 
void
��  
EliminarPlanAccion
�� &
(
��& '8
*ecop_concurrencia_eventos_en_salud_detalle
��' Q
OBJ
��R U
,
��U V
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	

DACElimina
�� 
.
��  
EliminarPlanAccion
�� )
(
��) *
OBJ
��* -
,
��- .
ref
��/ 2
MsgRes
��3 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
List
�� 
<
�� 2
$ecop_concurrencia_evolucion_diag_def
�� 8
>
��8 9+
ConsultaDiagnosticoDefinitivo
��: W
(
��W X2
$ecop_concurrencia_evolucion_diag_def
��X |

Objdiagdef��} �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
ConsultaDiagnosticoDefinitivo
�� <
(
��< =

Objdiagdef
��= G
,
��G H
ref
��I L
MsgRes
��M S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� '
ecop_concurrencia_cohorte
�� -
>
��- .
ConsultaCohorte
��/ >
(
��> ?'
ecop_concurrencia_cohorte
��? X

ObjCohorte
��Y c
,
��c d
ref
��e h 
MessageResponseOBJ
��i {
MsgRes��| �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaCohorte
�� .
(
��. /

ObjCohorte
��/ 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
void
�� *
InsertaDiagnosticoDefinitivo
�� 0
(
��0 12
$ecop_concurrencia_evolucion_diag_def
��1 U4
&Concurrencia_Diagnostico_Definitivo_id
��V |
,
��| }
String��~ �
UserName��� �
,��� �
String��� �
	IPAddress��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	

DACInserta
�� 
.
�� *
InsertaDiagnosticoDefinitivo
�� 3
(
��3 44
&Concurrencia_Diagnostico_Definitivo_id
��4 Z
,
��Z [
UserName
��\ d
,
��d e
	IPAddress
��f o
,
��o p
ref
��q t
MsgRes
��u {
)
��{ |
;
��| }
}
�� 	
public
�� 
void
�� 
InsertaGlosa
��  
(
��  !%
ecop_concurrencia_glosa
��! 8
ObjGlosa
��9 A
,
��A B
String
��C I
UserName
��J R
,
��R S
String
��T Z
	IPAddress
��[ d
,
��d e
ref
��f i 
MessageResponseOBJ
��j |
MsgRes��} �
)��� �
{
�� 	

DACInserta
�� 
.
�� 
InsertaGlosa
�� #
(
��# $
ObjGlosa
��$ ,
,
��, -
UserName
��. 6
,
��6 7
	IPAddress
��8 A
,
��A B
ref
��C F
MsgRes
��G M
)
��M N
;
��N O
}
�� 	
public
�� 
List
�� 
<
�� %
ecop_concurrencia_glosa
�� +
>
��+ ,
ConsultaGlosa
��- :
(
��: ;%
ecop_concurrencia_glosa
��; R
ObjGlosa
��S [
,
��[ \
ref
��] ` 
MessageResponseOBJ
��a s
MsgRes
��t z
)
��z {
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaGlosa
�� ,
(
��, -
ObjGlosa
��- 5
,
��5 6
ref
��7 :
MsgRes
��; A
)
��A B
;
��B C
}
�� 	
public
�� 
List
�� 
<
�� %
ecop_concurrencia_glosa
�� +
>
��+ ,+
ConsultaGlosabyidconcurrencia
��- J
(
��J K
int
��K N
idconcurrencia
��O ]
,
��] ^
ref
��_ b 
MessageResponseOBJ
��c u
MsgRes
��v |
)
��| }
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
ConsultaGlosabyidconcurrencia
�� <
(
��< =
idconcurrencia
��= K
,
��K L
ref
��M P
MsgRes
��Q W
)
��W X
;
��X Y
}
�� 	
public
�� 
List
�� 
<
�� &
ecop_concurrencia_ahorro
�� ,
>
��, -
ConsultaAhorro
��. <
(
��< =&
ecop_concurrencia_ahorro
��= U
	ObjAhorro
��V _
,
��_ `
ref
��a d 
MessageResponseOBJ
��e w
MsgRes
��x ~
)
��~ 
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaAhorro
�� -
(
��- .
	ObjAhorro
��. 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
List
�� 
<
�� )
vw_ecop_concurrencia_ahorro
�� /
>
��/ 0 
ConsultaAhorroOtro
��1 C
(
��C D)
vw_ecop_concurrencia_ahorro
��D _
	ObjAhorro
��` i
,
��i j
ref
��k n!
MessageResponseOBJ��o �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
ConsultaAhorroOtro
�� 1
(
��1 2
	ObjAhorro
��2 ;
,
��; <
ref
��= @
MsgRes
��A G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� *
vw_ecop_concurrencia_cohorte
�� 0
>
��0 1
ConsultaCohorte
��2 A
(
��A B*
vw_ecop_concurrencia_cohorte
��B ^

ObjCohorte
��_ i
,
��i j
ref
��k n!
MessageResponseOBJ��o �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaCohorte
�� .
(
��. /

ObjCohorte
��/ 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
List
�� 
<
�� "
Ref_eventos_adversos
�� (
>
��( ) 
GetEventosAdversos
��* <
(
��< =
)
��= >
{
�� 	
return
�� 
DACComonClass
��  
.
��  ! 
GetEventosAdversos
��! 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_grado_lesion
�� $
>
��$ %
GetGradoLesion
��& 4
(
��4 5
)
��5 6
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetGradoLesion
��! /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� )
Ref_factores_contribuyentes
�� /
>
��/ 0'
GetFactoresContribuyentes
��1 J
(
��J K
)
��K L
{
�� 	
return
�� 
DACComonClass
��  
.
��  !'
GetFactoresContribuyentes
��! :
(
��: ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� $
Ref_barreras_seguridad
�� *
>
��* +$
GetBarrerasDeSeguridad
��, B
(
��B C
)
��C D
{
�� 	
return
�� 
DACComonClass
��  
.
��  !$
GetBarrerasDeSeguridad
��! 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� $
Ref_acciones_inseguras
�� *
>
��* +"
GetAccionesInseguras
��, @
(
��@ A
)
��A B
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
GetAccionesInseguras
��! 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
��  
Ref_plan_de_manejo
�� &
>
��& '
GetPlanDeManejo
��( 7
(
��7 8
)
��8 9
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetPlanDeManejo
��! 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
void
�� "
InsertaEventoAdverso
�� (
(
��( )0
"ecop_concurrencia_eventos_adversos
��) K
ObjEventoAdv
��L X
,
��X Y
String
��Z `
UserName
��a i
,
��i j
String
��k q
	IPAddress
��r {
,
��{ |
ref��} �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	

DACInserta
�� 
.
�� "
InsertaEventoAdverso
�� +
(
��+ ,
ObjEventoAdv
��, 8
,
��8 9
UserName
��: B
,
��B C
	IPAddress
��D M
,
��M N
ref
��O R
MsgRes
��S Y
)
��Y Z
;
��Z [
}
�� 	
public
�� 
List
�� 
<
�� 0
"ecop_concurrencia_eventos_adversos
�� 6
>
��6 7#
ConsultaEventoAdverso
��8 M
(
��M N0
"ecop_concurrencia_eventos_adversos
��N p
ObjEventoAdverso��q �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
ConsultaEventoAdverso
�� 4
(
��4 5
ObjEventoAdverso
��5 E
,
��E F
ref
��G J
MsgRes
��K Q
)
��Q R
;
��R S
}
�� 	
public
�� 
List
�� 
<
�� (
Ref_situaciones_de_calidad
�� .
>
��. /%
GetSituacionesDeCalidad
��0 G
(
��G H
)
��H I
{
�� 	
return
�� 
DACComonClass
��  
.
��  !%
GetSituacionesDeCalidad
��! 8
(
��8 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
void
�� '
InsertaSituacionesCalidad
�� -
(
��- .6
(ecop_concurrencia_situaciones_de_calidad
��. V
ObjSituacionCalid
��W h
,
��h i
String
��j p
UserName
��q y
,
��y z
String��{ �
	IPAddress��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	

DACInserta
�� 
.
�� '
InsertaSituacionesCalidad
�� 0
(
��0 1
ObjSituacionCalid
��1 B
,
��B C
UserName
��D L
,
��L M
	IPAddress
��N W
,
��W X
ref
��Y \
MsgRes
��] c
)
��c d
;
��d e
}
�� 	
public
�� 
List
�� 
<
�� 6
(ecop_concurrencia_situaciones_de_calidad
�� <
>
��< =(
ConsultaSituacionesCalidad
��> X
(
��X Y7
(ecop_concurrencia_situaciones_de_calidad��Y �
ObjSituCali��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
ConsultaSituacionesCalidad
�� 9
(
��9 :
ObjSituCali
��: E
,
��E F
ref
��G J
MsgRes
��K Q
)
��Q R
;
��R S
}
�� 	
public
�� 
List
�� 
<
�� 2
$Ref_motivo_cancelacion_procedimiento
�� 8
>
��8 9"
GetMotivoCancelacion
��: N
(
��N O
)
��O P
{
�� 	
return
�� 
DACComonClass
��  
.
��  !"
GetMotivoCancelacion
��! 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
void
�� 4
&InsertaProcedimientoQuirugicoCancelado
�� :
(
��: ;E
7ecop_concurrencia_procedimientos_quirurgicos_cancelados
��; r%
ProcedimientoQuirCance��s �
,��� �
String��� �
UserName��� �
,��� �
String��� �
	IPAddress��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	

DACInserta
�� 
.
�� 4
&InsertaProcedimientoQuirugicoCancelado
�� =
(
��= >$
ProcedimientoQuirCance
��> T
,
��T U
UserName
��V ^
,
��^ _
	IPAddress
��` i
,
��i j
ref
��k n
MsgRes
��o u
)
��u v
;
��v w
}
�� 	
public
�� 
List
�� 
<
�� E
7ecop_concurrencia_procedimientos_quirurgicos_cancelados
�� K
>
��K L*
ConsultaProcQuirurgicosCance
��M i
(
��i jF
7ecop_concurrencia_procedimientos_quirurgicos_cancelados��j �
ObjProcQuir��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
ConsultaProcQuirurgicosCance
�� ;
(
��; <
ObjProcQuir
��< G
,
��G H
ref
��I L
MsgRes
��M S
)
��S T
;
��T U
}
�� 	
public
�� 
void
�� 
InsertarNatalidad
�� %
(
��% &(
natalidad_sin_concurrencia
��& @
	Natalidad
��A J
,
��J K
ref
��L O 
MessageResponseOBJ
��P b
MsgRes
��c i
)
��i j
{
�� 	

DACInserta
�� 
.
�� 
InsertarNatalidad
�� (
(
��( )
	Natalidad
��) 2
,
��2 3
ref
��4 7
MsgRes
��8 >
)
��> ?
;
��? @
}
�� 	
public
�� 
void
��  
InsertarMortalidad
�� &
(
��& ')
mortalidad_sin_concurrencia
��' B

Mortalidad
��C M
,
��M N
ref
��O R 
MessageResponseOBJ
��S e
MsgRes
��f l
)
��l m
{
�� 	

DACInserta
�� 
.
��  
InsertarMortalidad
�� )
(
��) *

Mortalidad
��* 4
,
��4 5
ref
��6 9
MsgRes
��: @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
�� )
vw_tablero_eventos_adversos
�� /
>
��/ 0#
ReportesEventoAdverso
��1 F
(
��F G
)
��G H
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
ReportesEventoAdverso
�� 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
void
�� )
InsertarAlertasConcurrencia
�� /
(
��/ 0,
alertas_generadas_concurrencia
��0 N
Alertas
��O V
,
��V W
ref
��X [ 
MessageResponseOBJ
��\ n
MsgRes
��o u
)
��u v
{
�� 	

DACInserta
�� 
.
�� )
InsertarAlertasConcurrencia
�� 2
(
��2 3
Alertas
��3 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
void
�� (
InsertarConcurrenciaAhorro
�� .
(
��. /&
ecop_concurrencia_ahorro
��/ G
Ahorro
��H N
,
��N O
ref
��P S 
MessageResponseOBJ
��T f
MsgRes
��g m
)
��m n
{
�� 	

DACInserta
�� 
.
�� (
InsertarConcurrenciaAhorro
�� 1
(
��1 2
Ahorro
��2 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
void
�� )
InsertarConcurrenciaCohorte
�� /
(
��/ 0'
ecop_concurrencia_cohorte
��0 I
Cohorte
��J Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
{
�� 	

DACInserta
�� 
.
�� )
InsertarConcurrenciaCohorte
�� 2
(
��2 3
Cohorte
��3 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_causal_glosa
�� $
>
��$ %!
ConsultaCausalGlosa
��& 9
(
��9 :
int
��: =!
id_respnsable_glosa
��> Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
ConsultaCausalGlosa
�� 2
(
��2 3!
id_respnsable_glosa
��3 F
,
��F G
ref
��H K
MsgRes
��L R
)
��R S
;
��S T
}
�� 	
public
�� 
List
�� 
<
�� ,
alertas_generadas_concurrencia
�� 2
>
��2 3)
ConsultaAlertasConcurrencia
��4 O
(
��O P
Int32
��P U
Idalerta
��V ^
,
��^ _
string
��` f
idcie10
��g n
,
��n o
ref
��p s!
MessageResponseOBJ��t �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
ConsultaAlertasConcurrencia
�� :
(
��: ;
Idalerta
��; C
,
��C D
idcie10
��E L
,
��L M
ref
��N Q
MsgRes
��R X
)
��X Y
;
��Y Z
}
�� 	
public
�� 
vw_cie10_alertas
�� !
ConsultaAlertaCie10
��  3
(
��3 4
String
��4 :
idcie10
��; B
,
��B C
ref
��D G 
MessageResponseOBJ
��H Z
MsgRes
��[ a
)
��a b
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
ConsultaAlertaCie10
�� 2
(
��2 3
idcie10
��3 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
ref_cie10_detalle
��  (
ConsultaAlertaCie10Detalle
��! ;
(
��; <
String
��< B
idcie10
��C J
,
��J K
ref
��L O 
MessageResponseOBJ
��P b
MsgRes
��c i
)
��i j
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
ConsultaAlertaCie10Detalle
�� 9
(
��9 :
idcie10
��: A
,
��A B
ref
��C F
MsgRes
��G M
)
��M N
;
��N O
}
�� 	
public
�� 
List
�� 
<
�� $
analisis_caso_original
�� *
>
��* +3
%ConsultaEvolucionAnalisisCasoOriginal
��, Q
(
��Q R
Int32
��R W
?
��W X
idconcurrencia
��Y g
,
��g h
Int32
��i n
?
��n o
idanalisiscaso
��p ~
,
��~ 
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 3
%ConsultaEvolucionAnalisisCasoOriginal
�� D
(
��D E
idconcurrencia
��E S
,
��S T
idanalisiscaso
��U c
,
��c d
ref
��e h
MsgRes
��i o
)
��o p
;
��p q
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_valor_ahorro
�� $
>
��$ %
ValorAhorro
��& 1
(
��1 2
)
��2 3
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ValorAhorro
�� *
(
��* +
)
��+ ,
;
��, -
}
�� 	
public
�� 
List
�� 
<
�� 2
$vw_evo_ecop_concurrencia_evoluciones
�� 8
>
��8 9*
GetConcurrenciaEvolucionById
��: V
(
��V W
int
��W Z
id_evolucion
��[ g
)
��g h
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
GetConcurrenciaEvolucionById
�� ;
(
��; <
id_evolucion
��< H
)
��H I
;
��I J
}
�� 	
public
�� 
void
�� -
MandarConcurrenciaContactCenter
�� 3
(
��3 4
List
��4 8
<
��8 9
int
��9 <
>
��< =
listado
��> E
,
��E F
ref
��G J 
MessageResponseOBJ
��K ]
MsgRes
��^ d
)
��d e
{
�� 	
DACActualiza
�� 
.
�� -
MandarConcurrenciaContactCenter
�� 8
(
��8 9
listado
��9 @
,
��@ A
ref
��B E
MsgRes
��F L
)
��L M
;
��M N
}
�� 	
public
�� 
void
�� 7
)MandarindividualConcurrenciaContactCenter
�� =
(
��= >
int
��> A
?
��A B
idConcu
��C J
,
��J K
string
��L R
observacion
��S ^
,
��^ _
ref
��` c 
MessageResponseOBJ
��d v
MsgRes
��w }
)
��} ~
{
�� 	
DACActualiza
�� 
.
�� 7
)MandarindividualConcurrenciaContactCenter
�� B
(
��B C
idConcu
��C J
,
��J K
observacion
��L W
,
��W X
ref
��Y \
MsgRes
��] c
)
��c d
;
��d e
}
�� 	
public
�� 0
"vw_ecop_evo_evaluacion_pertinencia
�� 1*
GetEvaluacionPertinenciaById
��2 N
(
��N O
int
��O R
idevolucion
��S ^
)
��^ _
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
GetEvaluacionPertinenciaById
�� ;
(
��; <
idevolucion
��< G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� 4
&management_evolucionEgresosListaResult
�� :
>
��: ;(
GetEvolucionesConcurrencia
��< V
(
��V W
int
��W Z
idConcu
��[ b
)
��b c
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
GetEvolucionesConcurrencia
�� 9
(
��9 :
idConcu
��: A
)
��A B
;
��B C
}
�� 	
public
�� 
List
�� 
<
�� 7
)Management_evolucion_procedimientosResult
�� =
>
��= >0
"ConsultaProcedimientosConcurrencia
��? a
(
��a b
ref
��b e 
MessageResponseOBJ
��f x
MsgRes
��y 
)�� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 0
"ConsultaProcedimientosConcurrencia
�� A
(
��A B
ref
��B E
MsgRes
��F L
)
��L M
;
��M N
}
�� 	
public
�� 
List
�� 
<
�� +
ManagmentAlertasCalidadResult
�� 1
>
��1 2
CuentaFechaCargue
��3 D
(
��D E
Int32
��E J
Opc
��K N
,
��N O
DateTime
��P X
FechaInicial
��Y e
,
��e f
DateTime
��g o
FechaFin
��p x
,
��x y
String��z �
strProveedor��� �
,��� �
String��� �
	strEstado��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
CuentaFechaCargue
�� 0
(
��0 1
Opc
��1 4
,
��4 5
FechaInicial
��6 B
,
��B C
FechaFin
��D L
,
��L M
strProveedor
��N Z
,
��Z [
	strEstado
��\ e
,
��e f
ref
��g j
MsgRes
��k q
)
��q r
;
��r s
}
�� 	
public
�� 
List
�� 
<
�� +
vw_Devoluciones_sin_gestionar
�� 1
>
��1 2$
DevolucionesSinGestion
��3 I
(
��I J
)
��J K
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
DevolucionesSinGestion
�� 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
Int32
�� +
InsertarDevolucionGestionadas
�� 2
(
��2 3,
factura_devolucion_gestionadas
��3 Q
OBJ
��R U
,
��U V
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 

DACInserta
�� 
.
�� +
InsertarDevolucionGestionadas
�� ;
(
��; <
OBJ
��< ?
,
��? @
ref
��A D
MsgRes
��E K
)
��K L
;
��L M
}
�� 	
public
�� 
List
�� 
<
�� 
vw_hallazgos_RIPS
�� %
>
��% &%
HallazgosRipsSinGestion
��' >
(
��> ?
)
��? @
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
HallazgosRipsSinGestion
�� 6
(
��6 7
)
��7 8
;
��8 9
}
�� 	
public
�� 
List
�� 
<
�� %
vw_facturas_sin_auditar
�� +
>
��+ , 
FacturasporAuditar
��- ?
(
��? @
)
��@ A
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
FacturasporAuditar
�� 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� 
vw_costo_evitado
�� $
>
��$ %
CostoEvitado
��& 2
(
��2 3
Int32
��3 8
Id
��9 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
CostoEvitado
�� +
(
��+ ,
Id
��, .
,
��. /
ref
��0 3
MsgRes
��4 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� &
vw_facturas_diagnosticos
�� ,
>
��, -!
DiagnosticosCuentas
��. A
(
��A B
Int32
��B G
Id
��H J
,
��J K
ref
��L O 
MessageResponseOBJ
��P b
MsgRes
��c i
)
��i j
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
DiagnosticosCuentas
�� 2
(
��2 3
Id
��3 5
,
��5 6
ref
��7 :
MsgRes
��; A
)
��A B
;
��B C
}
�� 	
public
�� 
List
�� 
<
�� )
vw_ECOPETROL_DEVOLUCION_FAC
�� /
>
��/ 0
VwDevoluciones
��1 ?
(
��? @
)
��@ A
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
VwDevoluciones
�� -
(
��- .
)
��. /
;
��/ 0
}
�� 	
public
�� 
List
�� 
<
�� )
vw_ECOPETROL_HALLAZGOS_RIPS
�� /
>
��/ 0
VwHallazgosRIPS
��1 @
(
��@ A
)
��A B
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
VwHallazgosRIPS
�� .
(
��. /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
List
�� 
<
�� *
ECOPETROL_RECEPCION_FACTURAS
�� 0
>
��0 1!
VwRecepcionFacturas
��2 E
(
��E F
)
��F G
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
VwRecepcionFacturas
�� 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
void
�� 
InsertarMega
��  
(
��  !
List
��! %
<
��% &
megas_cargue_base
��& 7
>
��7 8
ListMega
��9 A
,
��A B
ref
��C F 
MessageResponseOBJ
��G Y
MsgRes
��Z `
)
��` a
{
�� 	

DACInserta
�� 
.
�� 
InsertarMega
�� #
(
��# $
ListMega
��$ ,
,
��, -
ref
��. 1
MsgRes
��2 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� 9
+ManagmentClinicosCensoConConcurrenciaResult
�� ?
>
��? @(
CensoConcurrenciaEcopetrol
��A [
(
��[ \
DateTime
��\ d
	fecha_ini
��e n
,
��n o
DateTime
��p x
fecha_final��y �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
CensoConcurrenciaEcopetrol
�� 9
(
��9 :
	fecha_ini
��: C
,
��C D
fecha_final
��E P
,
��P Q
ref
��R U
MsgRes
��V \
)
��\ ]
;
��] ^
}
�� 	
public
�� 
	DataTable
�� *
CensoConcurrenciaEcopetrolII
�� 5
(
��5 6
DateTime
��6 >
	fecha_ini
��? H
,
��H I
DateTime
��J R
fecha_final
��S ^
,
��^ _
String
��` f
Conexion
��g o
,
��o p
ref
��q t!
MessageResponseOBJ��u �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
CensoConcurrenciaEcopetrolII
�� ;
(
��; <
	fecha_ini
��< E
,
��E F
fecha_final
��G R
,
��R S
Conexion
��T \
,
��\ ]
ref
��^ a
MsgRes
��b h
)
��h i
;
��i j
}
�� 	
public
�� 
List
�� 
<
�� *
ManagmentClinicosCensoResult
�� 0
>
��0 1
CensoEcopetrol
��2 @
(
��@ A
DateTime
��A I
	fecha_ini
��J S
,
��S T
DateTime
��U ]
fecha_final
��^ i
,
��i j
ref
��k n!
MessageResponseOBJ��o �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
CensoEcopetrol
�� -
(
��- .
	fecha_ini
��. 7
,
��7 8
fecha_final
��9 D
,
��D E
ref
��F I
MsgRes
��J P
)
��P Q
;
��Q R
}
�� 	
public
�� 
List
�� 
<
�� 5
'ManagmentClinicosConsultasAlertasResult
�� ;
>
��; <
AlertasEcopetrol
��= M
(
��M N
DateTime
��N V
	fecha_ini
��W `
,
��` a
DateTime
��b j
fecha_final
��k v
,
��v w
ref
��x {!
MessageResponseOBJ��| �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
AlertasEcopetrol
�� /
(
��/ 0
	fecha_ini
��0 9
,
��9 :
fecha_final
��; F
,
��F G
ref
��H K
MsgRes
��L R
)
��R S
;
��S T
}
�� 	
public
�� 
Int32
�� (
InsertarDevolucionFacturas
�� /
(
��/ 0 
factura_devolucion
��0 B
OBJ
��C F
,
��F G
ref
��H K 
MessageResponseOBJ
��L ^
MsgRes
��_ e
)
��e f
{
�� 	
return
�� 

DACInserta
�� 
.
�� (
InsertarDevolucionFacturas
�� 8
(
��8 9
OBJ
��9 <
,
��< =
ref
��> A
MsgRes
��B H
)
��H I
;
��I J
}
�� 	
public
�� 
Int32
�� /
!InsertarDevolucionFacturasDetalle
�� 6
(
��6 7(
factura_devolucion_detalle
��7 Q
OBJ
��R U
,
��U V
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 

DACInserta
�� 
.
�� /
!InsertarDevolucionFacturasDetalle
�� ?
(
��? @
OBJ
��@ C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
List
�� 
<
�� 0
"ManagmentReportDevolucionFacResult
�� 6
>
��6 7*
ConsultaReporteDevolucionFac
��8 T
(
��T U
Int32
��U Z#
id_devolucion_factura
��[ p
)
��p q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
ConsultaReporteDevolucionFac
�� ;
(
��; <#
id_devolucion_factura
��< Q
)
��Q R
;
��R S
}
�� 	
public
�� 
Int32
�� %
InsertarFacturaSinCenso
�� ,
(
��, -
factura_sin_censo
��- >
OBJ
��? B
,
��B C
ref
��D G 
MessageResponseOBJ
��H Z
MsgRes
��[ a
)
��a b
{
�� 	
return
�� 

DACInserta
�� 
.
�� %
InsertarFacturaSinCenso
�� 5
(
��5 6
OBJ
��6 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
Int32
�� 
InsertarHallazgos
�� &
(
��& '
hallazgo_RIPS
��' 4
OBJ
��5 8
,
��8 9
ref
��: = 
MessageResponseOBJ
��> P
MsgRes
��Q W
)
��W X
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarHallazgos
�� /
(
��/ 0
OBJ
��0 3
,
��3 4
ref
��5 8
MsgRes
��9 ?
)
��? @
;
��@ A
}
�� 	
public
�� 
Int32
�� &
InsertarHallazgosDetalle
�� -
(
��- .#
hallazgo_RIPS_detalle
��. C
OBJ
��D G
,
��G H
ref
��I L 
MessageResponseOBJ
��M _
MsgRes
��` f
)
��f g
{
�� 	
return
�� 

DACInserta
�� 
.
�� &
InsertarHallazgosDetalle
�� 6
(
��6 7
OBJ
��7 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
List
�� 
<
�� /
!ManagmentReportHallazgosRipResult
�� 5
>
��5 6*
ConsultaReporteHallazgosRips
��7 S
(
��S T
Int32
��T Y
id_hallazgo_RIPS
��Z j
)
��j k
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
ConsultaReporteHallazgosRips
�� ;
(
��; <
id_hallazgo_RIPS
��< L
)
��L M
;
��M N
}
�� 	
public
�� 
void
�� $
ActualizaHallazgosRips
�� *
(
��* +
hallazgo_RIPS
��+ 8
Objrips
��9 @
,
��@ A
ref
��B E 
MessageResponseOBJ
��F X
MsgRes
��Y _
)
��_ `
{
�� 	
DACActualiza
�� 
.
�� $
ActualizaHallazgosRips
�� /
(
��/ 0
Objrips
��0 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
List
�� 
<
�� 
factura_sin_censo
�� %
>
��% &%
ConsultaFacturasSinAudi
��' >
(
��> ?
Int32
��? D"
id_factura_sin_censo
��E Y
)
��Y Z
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
ConsultaFacturasSinAudi
�� 6
(
��6 7"
id_factura_sin_censo
��7 K
)
��K L
;
��L M
}
�� 	
public
�� 
Int32
�� "
InsertarCostoEvitado
�� )
(
��) *+
factura_sin_censo_cos_evitado
��* G
Obj
��H K
,
��K L
ref
��M P 
MessageResponseOBJ
��Q c
MsgRes
��d j
)
��j k
{
�� 	
return
�� 

DACInserta
�� 
.
�� "
InsertarCostoEvitado
�� 2
(
��2 3
Obj
��3 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� (
InsertarDiagnosticoCuentas
�� /
(
��/ 0,
factura_sin_censo_diagnosticos
��0 N
Obj
��O R
,
��R S
ref
��T W 
MessageResponseOBJ
��X j
MsgRes
��k q
)
��q r
{
�� 	
return
�� 

DACInserta
�� 
.
�� (
InsertarDiagnosticoCuentas
�� 8
(
��8 9
Obj
��9 <
,
��< =
ref
��> A
MsgRes
��B H
)
��H I
;
��I J
}
�� 	
public
�� 
void
�� &
ActualizaFacturaAuditada
�� ,
(
��, -
factura_sin_censo
��- >
ObjAudi
��? F
,
��F G
ref
��H K 
MessageResponseOBJ
��L ^
MsgRes
��_ e
)
��e f
{
�� 	
DACActualiza
�� 
.
�� &
ActualizaFacturaAuditada
�� 1
(
��1 2
ObjAudi
��2 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
List
�� 
<
��  
factura_devolucion
�� &
>
��& ')
ConsultaDevolucionesFactura
��( C
(
��C D
String
��D J
Numero_factura
��K Y
)
��Y Z
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
ConsultaDevolucionesFactura
�� :
(
��: ;
Numero_factura
��; I
)
��I J
;
��J K
}
�� 	
public
�� 
List
�� 
<
�� 
factura_sin_censo
�� %
>
��% &#
ConsultaFacturaNumero
��' <
(
��< =
String
��= C
Numero_factura
��D R
)
��R S
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
ConsultaFacturaNumero
�� 4
(
��4 5
Numero_factura
��5 C
)
��C D
;
��D E
}
�� 	
public
�� 
List
�� 
<
��  
factura_devolucion
�� &
>
��& '+
ConsultaDevolucionesFacturaId
��( E
(
��E F
Int32
��F K
Id_devolucion
��L Y
)
��Y Z
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
ConsultaDevolucionesFacturaId
�� <
(
��< =
Id_devolucion
��= J
)
��J K
;
��K L
}
�� 	
public
�� 
List
�� 
<
�� 
hallazgo_RIPS
�� !
>
��! "!
ConsultaHallazgosId
��# 6
(
��6 7
Int32
��7 <
Id_rips
��= D
)
��D E
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
ConsultaHallazgosId
�� 2
(
��2 3
Id_rips
��3 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� /
!management_rips_busqueda_acResult
�� 5
>
��5 6!
TraerConsultaRIPSAC
��7 J
(
��J K
DateTime
��K S
?
��S T
fechaInicio
��U `
,
��` a
DateTime
��b j
?
��j k
fechaFin
��l t
,
��t u
string
��v |
codCups��} �
,��� �
string��� �
diagnostico��� �
,��� �
string��� �
cedula��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
TraerConsultaRIPSAC
�� 2
(
��2 3
fechaInicio
��3 >
,
��> ?
fechaFin
��@ H
,
��H I
codCups
��J Q
,
��Q R
diagnostico
��S ^
,
��^ _
cedula
��` f
)
��f g
;
��g h
}
�� 	
public
�� 
List
�� 
<
�� /
!management_rips_busqueda_apResult
�� 5
>
��5 6!
TraerConsultaRIPSAP
��7 J
(
��J K
DateTime
��K S
?
��S T
fechaInicio
��U `
,
��` a
DateTime
��b j
?
��j k
fechaFin
��l t
,
��t u
string
��v |
codCups��} �
,��� �
string��� �
diagnostico��� �
,��� �
string��� �
cedula��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
TraerConsultaRIPSAP
�� 2
(
��2 3
fechaInicio
��3 >
,
��> ?
fechaFin
��@ H
,
��H I
codCups
��J Q
,
��Q R
diagnostico
��S ^
,
��^ _
cedula
��` f
)
��f g
;
��g h
}
�� 	
public
�� 
Int32
�� '
InsertarloteContabilizado
�� .
(
��. />
0ecop_gestion_factura_digital_contabilizados_lote
��/ _
OBJ
��` c
,
��c d
ref
��e h 
MessageResponseOBJ
��i {
MsgRes��| �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� '
InsertarloteContabilizado
�� 7
(
��7 8
OBJ
��8 ;
,
��; <
ref
��= @
MsgRes
��A G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� 7
)management_reportelotecontabilizadoResult
�� =
>
��= >!
ConsultaReporteLote
��? R
(
��R S
Int32
��S X
ID
��Y [
)
��[ \
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
ConsultaReporteLote
�� 2
(
��2 3
ID
��3 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� $
facturacion_sap_cargue
�� *
>
��* +%
validarCargueFacturaSap
��, C
(
��C D
int
��D G
?
��G H
mes
��I L
,
��L M
int
��N Q
?
��Q R
año
��S V
)
��V W
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
validarCargueFacturaSap
�� 6
(
��6 7
mes
��7 :
,
��: ;
año
��< ?
)
��? @
;
��@ A
}
�� 	
public
�� 
int
�� $
InsertarFacturacionSAP
�� )
(
��) *
List
��* .
<
��. /"
facturacion_sap_dtll
��/ C
>
��C D
List
��E I
,
��I J$
facturacion_sap_cargue
��K a
objbase
��b i
,
��i j
ref
��k n!
MessageResponseOBJ��o �
MsgRes��� �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� $
InsertarFacturacionSAP
�� 4
(
��4 5
List
��5 9
,
��9 :
objbase
��; B
,
��B C
ref
��D G
MsgRes
��H N
)
��N O
;
��O P
}
�� 	
public
�� 
List
�� 
<
�� 3
%management_facturacionSAP_listaResult
�� 9
>
��9 :"
ListarFacturacionSAP
��; O
(
��O P
)
��P Q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
ListarFacturacionSAP
�� 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� :
,management_facturacionSAP_listaDetalleResult
�� @
>
��@ A)
ListarFacturacionSAPDetalle
��B ]
(
��] ^
int
��^ a
año
��b e
,
��e f
string
��g m
mes
��n q
)
��q r
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
ListarFacturacionSAPDetalle
�� :
(
��: ;
año
��; >
,
��> ?
mes
��@ C
)
��C D
;
��D E
}
�� 	
public
�� 
List
�� 
<
�� :
,management_facturacionSAP_listaREPORTEResult
�� @
>
��@ A)
ListarFacturacionSAPReporte
��B ]
(
��] ^
DateTime
��^ f
fechaIni
��g o
,
��o p
DateTime
��q y
fechaFin��z �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
ListarFacturacionSAPReporte
�� :
(
��: ;
fechaIni
��; C
,
��C D
fechaFin
��E M
)
��M N
;
��N O
}
�� 	
public
�� 
Int32
�� 
InsertarRips
�� !
(
��! "
RIPS
��" &
Objrips
��' .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRips
�� *
(
��* +
Objrips
��+ 2
,
��2 3
ref
��4 7
MsgRes
��8 >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS
�� 
>
�� 
ConsultaRips
�� &
(
��& '
Int32
��' ,
IdRips
��- 3
,
��3 4
ref
��5 8 
MessageResponseOBJ
��9 K
MsgRes
��L R
)
��R S
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaRips
�� +
(
��+ ,
IdRips
��, 2
,
��2 3
ref
��4 7
MsgRes
��8 >
)
��> ?
;
��? @
}
�� 	
public
�� 
bool
�� 
ActualizaRips
�� !
(
��! "
RIPS
��" &
ObjRips
��' .
,
��. /
ref
��0 3 
MessageResponseOBJ
��4 F
MsgRes
��G M
)
��M N
{
�� 	
return
�� 
DACActualiza
�� 
.
��  
ActualizaRips
��  -
(
��- .
ObjRips
��. 5
,
��5 6
ref
��7 :
MsgRes
��; A
)
��A B
;
��B C
}
�� 	
public
�� 
Int32
�� 
InsertarRipsAC
�� #
(
��# $
List
��$ (
<
��( )
RIPS_AC
��) 0
>
��0 1
	ObjripsAc
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsAC
�� ,
(
��, -
	ObjripsAc
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� 
InsertarRipsAD
�� #
(
��# $
List
��$ (
<
��( )
RIPS_AD
��) 0
>
��0 1
	ObjripsAD
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsAD
�� ,
(
��, -
	ObjripsAD
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� 
InsertarRipsAF
�� #
(
��# $
List
��$ (
<
��( )
RIPS_AF
��) 0
>
��0 1
	ObjripsAF
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsAF
�� ,
(
��, -
	ObjripsAF
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� 
InsertarRipsAH
�� #
(
��# $
List
��$ (
<
��( )
RIPS_AH
��) 0
>
��0 1
	ObjripsAH
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsAH
�� ,
(
��, -
	ObjripsAH
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� 
InsertarRipsAM
�� #
(
��# $
List
��$ (
<
��( )
RIPS_AM
��) 0
>
��0 1
	ObjripsAM
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsAM
�� ,
(
��, -
	ObjripsAM
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� 
InsertarRipsAN
�� #
(
��# $
List
��$ (
<
��( )
RIPS_AN
��) 0
>
��0 1
	ObjripsAN
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsAN
�� ,
(
��, -
	ObjripsAN
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� 
InsertarRipsAP
�� #
(
��# $
List
��$ (
<
��( )
RIPS_AP
��) 0
>
��0 1
	ObjripsAP
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsAP
�� ,
(
��, -
	ObjripsAP
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� 
InsertarRipsAT
�� #
(
��# $
List
��$ (
<
��( )
RIPS_AT
��) 0
>
��0 1
	ObjripsAT
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsAT
�� ,
(
��, -
	ObjripsAT
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� 
InsertarRipsAU
�� #
(
��# $
List
��$ (
<
��( )
RIPS_AU
��) 0
>
��0 1
	ObjripsAU
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsAU
�� ,
(
��, -
	ObjripsAU
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� 
InsertarRipsCT
�� #
(
��# $
List
��$ (
<
��( )
RIPS_CT
��) 0
>
��0 1
	ObjripsCT
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsCT
�� ,
(
��, -
	ObjripsCT
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� 
InsertarRipsUS
�� #
(
��# $
List
��$ (
<
��( )
RIPS_US
��) 0
>
��0 1
	ObjripsUS
��2 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarRipsUS
�� ,
(
��, -
	ObjripsUS
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
List
�� 
<
�� 
ECOPETROL_COMMON
�� $
.
��$ %
ENUM
��% )
.
��) *
reporterips
��* 5
>
��5 6$
ConsultaRipsEvaluacion
��7 M
(
��M N
Int32
��N S
IdRips
��T Z
,
��Z [
string
��\ b
conexion
��c k
,
��k l
ref
��m p!
MessageResponseOBJ��q �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
ConsultaRipsEvaluacion
�� 5
(
��5 6
IdRips
��6 <
,
��< =
conexion
��> F
,
��F G
ref
��H K
MsgRes
��L R
)
��R S
;
��S T
}
�� 	
public
�� 
List
�� 
<
�� ;
-managmentReportePrestadoresNoExistentesResult
�� A
>
��A B(
getprestadoresnoexistentes
��C ]
(
��] ^
Int32
��^ c
IdRips
��d j
,
��j k
ref
��l o!
MessageResponseOBJ��p �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
getprestadoresnoexistentes
�� 9
(
��9 :
IdRips
��: @
,
��@ A
ref
��B E
MsgRes
��F L
)
��L M
;
��M N
}
�� 	
public
�� 
List
�� 
<
�� &
Logerroresevaluacionrips
�� ,
>
��, -'
ConsultaLogRipsEvaluacion
��. G
(
��G H
Int32
��H M
IdRips
��N T
,
��T U
ref
��V Y 
MessageResponseOBJ
��Z l
MsgRes
��m s
)
��s t
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
ConsultaLogRipsEvaluacion
�� 8
(
��8 9
IdRips
��9 ?
,
��? @
ref
��A D
MsgRes
��E K
)
��K L
;
��L M
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS
�� 
>
�� %
GetListaRipsPorMesYAño
�� 0
(
��0 1
int
��1 4
mes
��5 8
,
��8 9
int
��: =
año
��> A
,
��A B
int
��C F
?
��F G
regional
��H P
)
��P Q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
GetListaRipsPorMesYAño
�� 5
(
��5 6
mes
��6 9
,
��9 :
año
��; >
,
��> ?
regional
��@ H
)
��H I
;
��I J
}
�� 	
public
�� 
List
�� 
<
�� <
.ManagmentErroresRipsEvaluacion_historicoResult
�� B
>
��B C0
"ConsultaLogRipsEvaluacionHistorico
��D f
(
��f g
Int32
��g l
IdRips
��m s
,
��s t
ref
��u x!
MessageResponseOBJ��y �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 0
"ConsultaLogRipsEvaluacionHistorico
�� A
(
��A B
IdRips
��B H
,
��H I
ref
��J M
MsgRes
��N T
)
��T U
;
��U V
}
�� 	
public
�� 
List
�� 
<
�� *
LogerroresevaluacionripsDtll
�� 0
>
��0 1+
ConsultaLogRipsEvaluacionDtll
��2 O
(
��O P
Int32
��P U
IdRips
��V \
,
��\ ]
ref
��^ a 
MessageResponseOBJ
��b t
MsgRes
��u {
)
��{ |
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
ConsultaLogRipsEvaluacionDtll
�� <
(
��< =
IdRips
��= C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
List
�� 
<
�� A
3ManagmentErroresRipsEvaluacion_Dtll_historicoResult
�� G
>
��G H4
&ConsultaLogRipsEvaluacionDtllHistorico
��I o
(
��o p
Int32
��p u
IdRips
��v |
,
��| }
ref��~ �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 4
&ConsultaLogRipsEvaluacionDtllHistorico
�� E
(
��E F
IdRips
��F L
,
��L M
ref
��N Q
MsgRes
��R X
)
��X Y
;
��Y Z
}
�� 	
public
�� (
vw_totales_rips_evaluacion
�� )+
ConsultaTotalesRipsEvaluacion
��* G
(
��G H
Int32
��H M
IdRips
��N T
,
��T U
ref
��V Y 
MessageResponseOBJ
��Z l
MsgRes
��m s
)
��s t
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
ConsultaTotalesRipsEvaluacion
�� <
(
��< =
IdRips
��= C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
RIPS_AC
�� 
GetRipsAcById
�� $
(
��$ %
int
��% (
idripsac
��) 1
)
��1 2
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetRipsAcById
�� ,
(
��, -
idripsac
��- 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
RIPS_AP
�� 
GetRipsApById
�� $
(
��$ %
int
��% (
idripsap
��) 1
)
��1 2
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetRipsApById
�� ,
(
��, -
idripsap
��- 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
RIPS_AU
�� 
GetRipsAuById
�� $
(
��$ %
int
��% (
id
��) +
)
��+ ,
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetRipsAuById
�� ,
(
��, -
id
��- /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
RIPS_AH
�� 
GetRipsAhById
�� $
(
��$ %
int
��% (
id
��) +
)
��+ ,
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetRipsAhById
�� ,
(
��, -
id
��- /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
RIPS_AN
�� 
GetRipsAnById
�� $
(
��$ %
int
��% (
id
��) +
)
��+ ,
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetRipsAnById
�� ,
(
��, -
id
��- /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AC_HISTORICO
�� %
>
��% &$
GetRipsAcHistoricoById
��' =
(
��= >
int
��> A
id
��B D
,
��D E
int
��F I
modo
��J N
)
��N O
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
GetRipsAcHistoricoById
�� 5
(
��5 6
id
��6 8
,
��8 9
modo
��: >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AP_HISTORICO
�� %
>
��% &$
GetRipsApHistoricoById
��' =
(
��= >
int
��> A
id
��B D
,
��D E
int
��F I
modo
��J N
)
��N O
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
GetRipsApHistoricoById
�� 5
(
��5 6
id
��6 8
,
��8 9
modo
��: >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AU_HISTORICO
�� %
>
��% &$
GetRipsAuHistoricoById
��' =
(
��= >
int
��> A
id
��B D
,
��D E
int
��F I
modo
��J N
)
��N O
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
GetRipsAuHistoricoById
�� 5
(
��5 6
id
��6 8
,
��8 9
modo
��: >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AH_HISTORICO
�� %
>
��% &$
GetRipsAhHistoricoById
��' =
(
��= >
int
��> A
id
��B D
,
��D E
int
��F I
modo
��J N
)
��N O
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
GetRipsAhHistoricoById
�� 5
(
��5 6
id
��6 8
,
��8 9
modo
��: >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AN_HISTORICO
�� %
>
��% &$
GetRipsAnHistoricoById
��' =
(
��= >
int
��> A
id
��B D
,
��D E
int
��F I
modo
��J N
)
��N O
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
GetRipsAnHistoricoById
�� 5
(
��5 6
id
��6 8
,
��8 9
modo
��: >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AF_HISTORICO
�� %
>
��% &$
GetRipsAfHistoricoById
��' =
(
��= >
int
��> A
id
��B D
)
��D E
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
GetRipsAfHistoricoById
�� 5
(
��5 6
id
��6 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_US_HISTORICO
�� %
>
��% &$
GetRipsUsHistoricoById
��' =
(
��= >
int
��> A
id
��B D
)
��D E
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
GetRipsUsHistoricoById
�� 5
(
��5 6
id
��6 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AC_HISTORICO
�� %
>
��% &&
GetRipsAcOportunidadById
��' ?
(
��? @
int
��@ C
id
��D F
,
��F G
int
��H K
modo
��L P
)
��P Q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetRipsAcOportunidadById
�� 7
(
��7 8
id
��8 :
,
��: ;
modo
��< @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AP_HISTORICO
�� %
>
��% &&
GetRipsApOportunidadById
��' ?
(
��? @
int
��@ C
id
��D F
,
��F G
int
��H K
modo
��L P
)
��P Q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetRipsApOportunidadById
�� 7
(
��7 8
id
��8 :
,
��: ;
modo
��< @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
�� ?
1Managment_ReportePrefacturas_total_abiertasResult
�� E
>
��E F)
GetPrefacturasTotalAbiertas
��G b
(
��b c
)
��c d
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
GetPrefacturasTotalAbiertas
�� :
(
��: ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� ?
1Managment_ReportePrefacturas_total_cerradasResult
�� E
>
��E F)
GetPrefacturasTotalCerradas
��G b
(
��b c
)
��c d
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
GetPrefacturasTotalCerradas
�� :
(
��: ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AU_HISTORICO
�� %
>
��% &&
GetRipsAuoportunidadById
��' ?
(
��? @
int
��@ C
id
��D F
,
��F G
int
��H K
modo
��L P
)
��P Q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetRipsAuoportunidadById
�� 7
(
��7 8
id
��8 :
,
��: ;
modo
��< @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AH_HISTORICO
�� %
>
��% &&
GetRipsAhOportunidadById
��' ?
(
��? @
int
��@ C
id
��D F
,
��F G
int
��H K
modo
��L P
)
��P Q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetRipsAhOportunidadById
�� 7
(
��7 8
id
��8 :
,
��: ;
modo
��< @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
�� 
RIPS_AN_HISTORICO
�� %
>
��% &&
GetRipsAnOportunidadById
��' ?
(
��? @
int
��@ C
id
��D F
,
��F G
int
��H K
modo
��L P
)
��P Q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetRipsAnOportunidadById
�� 7
(
��7 8
id
��8 :
,
��: ;
modo
��< @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
�� 2
$managmentRips_AC_FechaconsultaResult
�� 8
>
��8 9'
ConsultaRipsFechaConsulta
��: S
(
��S T
DateTime
��T \
FechaInicio
��] h
,
��h i
DateTime
��j r

FechaFinal
��s }
,
��} ~
ref�� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
ConsultaRipsFechaConsulta
�� 8
(
��8 9
FechaInicio
��9 D
,
��D E

FechaFinal
��F P
,
��P Q
ref
��R U
MsgRes
��V \
)
��\ ]
;
��] ^
}
�� 	
public
�� 
List
�� 
<
�� 7
)managmentRips_AP_FechaProcedimientoResult
�� =
>
��= >,
ConsultaRipsFechaProcedimeinto
��? ]
(
��] ^
int
��^ a
?
��a b
regional
��c k
,
��k l
DateTime
��m u
FechaInicio��v �
,��� �
DateTime��� �

FechaFinal��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
ConsultaRipsFechaProcedimiento
�� =
(
��= >
regional
��> F
,
��F G
FechaInicio
��H S
,
��S T

FechaFinal
��U _
,
��_ `
ref
��a d
MsgRes
��e k
)
��k l
;
��l m
}
�� 	
public
�� 
List
�� 
<
�� 1
#vw_consulta_rips_an_fechanacimiento
�� 7
>
��7 8(
GetListRipsFechaNacimiento
��9 S
(
��S T
DateTime
��T \
FechaInicio
��] h
,
��h i
DateTime
��j r

FechaFinal
��s }
,
��} ~
ref�� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
ConsultaRipsFechaNacimiento
�� :
(
��: ;
FechaInicio
��; F
,
��F G

FechaFinal
��H R
,
��R S
ref
��T W
MsgRes
��X ^
)
��^ _
;
��_ `
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_tipo_rips
�� !
>
��! "
ConsultaTipoRips
��# 3
(
��3 4
ref
��4 7 
MessageResponseOBJ
��8 J
MsgRes
��K Q
)
��Q R
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaTipoRips
�� /
(
��/ 0
ref
��0 3
MsgRes
��4 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� ,
vw_consulta_rips_ah_mortalidad
�� 2
>
��2 3%
GetListRipsMortalidadAH
��4 K
(
��K L
DateTime
��L T
?
��T U
FechaInicial
��V b
,
��b c
DateTime
��d l
?
��l m

FechaFinal
��n x
,
��x y
ref
��z }!
MessageResponseOBJ��~ �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
GetListRipsMortalidadAH
�� 6
(
��6 7
FechaInicial
��7 C
,
��C D

FechaFinal
��E O
,
��O P
ref
��Q T
MsgRes
��U [
)
��[ \
;
��\ ]
}
�� 	
public
�� 
List
�� 
<
�� ,
vw_consulta_rips_au_mortalidad
�� 2
>
��2 3%
GetListRipsMortalidadAU
��4 K
(
��K L
DateTime
��L T
?
��T U
FechaInicial
��V b
,
��b c
DateTime
��d l
?
��l m

FechaFinal
��n x
,
��x y
ref
��z }!
MessageResponseOBJ��~ �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
GetListRipsMortalidadAU
�� 6
(
��6 7
FechaInicial
��7 C
,
��C D

FechaFinal
��E O
,
��O P
ref
��Q T
MsgRes
��U [
)
��[ \
;
��\ ]
}
�� 	
public
�� 
RIPS
�� "
ValidacionCargueRips
�� (
(
��( )
int
��) ,

idregional
��- 7
,
��7 8
int
��9 <
mes
��= @
,
��@ A
int
��B E
año
��F I
)
��I J
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
ValidacionCargueRips
�� 3
(
��3 4

idregional
��4 >
,
��> ?
mes
��@ C
,
��C D
año
��E H
)
��H I
;
��I J
}
�� 	
public
�� 
void
�� 
ActualizarPQRS
�� "
(
��" #
	ecop_PQRS
��# ,
ObjPqrs
��- 4
,
��4 5
ref
��6 9 
MessageResponseOBJ
��: L
MsgRes
��M S
)
��S T
{
�� 	
DACActualiza
�� 
.
�� 
ActualizarPQRS
�� '
(
��' (
ObjPqrs
��( /
,
��/ 0
ref
��1 4
MsgRes
��5 ;
)
��; <
;
��< =
}
�� 	
public
�� 
	ecop_PQRS
�� 
LlamarPqrsById
�� '
(
��' (
int
��( +
id
��, .
)
��. /
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
LlamarPqrsById
�� -
(
��- .
id
��. 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
int
�� *
eliminarArchivoPqrsidArchivo
�� /
(
��/ 0
int
��0 3
id
��4 6
)
��6 7
{
�� 	
return
�� 

DACElimina
�� 
.
�� *
eliminarArchivoPqrsidArchivo
�� :
(
��: ;
id
��; =
)
��= >
;
��> ?
}
�� 	
public
�� 
int
�� *
GuardarLogEliminarArchivoPqr
�� /
(
��/ 0,
log_ecop_pqrs_eliminarArchivos
��0 N
obj
��O R
)
��R S
{
�� 	
return
�� 

DACInserta
�� 
.
�� *
GuardarLogEliminarArchivoPqr
�� :
(
��: ;
obj
��; >
)
��> ?
;
��? @
}
�� 	
public
�� 
void
�� 1
#ActualizarEstadoEnrevisionpryectada
�� 7
(
��7 8"
ecop_PQRS_enrevision
��8 L
OBJ
��M P
,
��P Q
ref
��R U 
MessageResponseOBJ
��V h
MsgRes
��i o
)
��o p
{
�� 	
DACActualiza
�� 
.
�� 1
#ActualizarEstadoEnrevisionpryectada
�� <
(
��< =
OBJ
��= @
,
��@ A
ref
��B E
MsgRes
��F L
)
��L M
;
��M N
}
�� 	
public
�� 
List
�� 
<
�� 
vw_ecop_PQRS
��  
>
��  !
ConsultaPQRS
��" .
(
��. /
)
��/ 0
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaPQRS
�� +
(
��+ ,
)
��, -
;
��- .
}
�� 	
public
�� 
List
�� 
<
�� 2
$management_pqrs_tableroAuditorResult
�� 8
>
��8 9$
ConsultaTableroAuditor
��: P
(
��P Q
)
��Q R
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
ConsultaTableroAuditor
�� 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� 2
$management_pqrsAuditor_reporteResult
�� 8
>
��8 9 
ReporteAuditorPqrs
��: L
(
��L M
int
��M P
idPqrs
��Q W
)
��W X
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
ReporteAuditorPqrs
�� 1
(
��1 2
idPqrs
��2 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� %
vw_ecop_PQRS_enrevision
�� +
>
��+ ,$
ConsultaPQRSEnRevision
��- C
(
��C D
)
��D E
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
ConsultaPQRSEnRevision
�� 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_PQRS_usuarios
�� %
>
��% &
GetusuariosPQRS
��' 6
(
��6 7
)
��7 8
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetusuariosPQRS
�� .
(
��. /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
List
�� 
<
�� 
	ecop_PQRS
�� 
>
�� 
	GetPQRSId
�� (
(
��( )
int
��) ,
id
��- /
)
��/ 0
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
	GetPQRSId
�� (
(
��( )
id
��) +
)
��+ ,
;
��, -
}
�� 	
public
�� 
List
�� 
<
�� "
ecop_PQRS_enrevision
�� (
>
��( )!
GetPQRSIdEnrevision
��* =
(
��= >
int
��> A
id
��B D
)
��D E
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
GetPQRSIdEnrevision
�� 2
(
��2 3
id
��3 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� "
ecop_PQRS_enrevision
�� (
>
��( )!
GetIdPQRSEnrevision
��* =
(
��= >
int
��> A
id
��B D
)
��D E
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
GetIdPQRSEnrevision
�� 2
(
��2 3
id
��3 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
List
�� 
<
�� 
vw_ecop_PQRS2
�� !
>
��! "
ConsultaPQRS2
��# 0
(
��0 1
)
��1 2
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaPQRS2
�� ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
Int32
�� 
InsertarPQRS
�� !
(
��! "
	ecop_PQRS
��" +
OBJ
��, /
,
��/ 0
ref
��1 4 
MessageResponseOBJ
��5 G
MsgRes
��H N
)
��N O
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarPQRS
�� *
(
��* +
OBJ
��+ .
,
��. /
ref
��0 3
MsgRes
��4 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� #
vw_ecop_PQRS_DetalleG
�� )
>
��) *!
ConsultaPQRSDetalle
��+ >
(
��> ?
Int32
��? D
Id_pqrs
��E L
)
��L M
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
ConsultaPQRSDetalle
�� 2
(
��2 3
Id_pqrs
��3 :
)
��: ;
;
��; <
}
�� 	
public
�� 9
+log_pqrs_reinicioConteo_asignacionAnalistas
�� :&
BuscarReinicioConteoPqrs
��; S
(
��S T
int
��T W
?
��W X
mes
��Y \
,
��\ ]
int
��^ a
?
��a b
año
��c f
)
��f g
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
BuscarReinicioConteoPqrs
�� 7
(
��7 8
mes
��8 ;
,
��; <
año
��= @
)
��@ A
;
��A B
}
�� 	
public
�� 
int
�� +
InsertarLogReinicioConteoPqrs
�� 0
(
��0 19
+log_pqrs_reinicioConteo_asignacionAnalistas
��1 \
obj
��] `
)
��` a
{
�� 	
return
�� 

DACInserta
�� 
.
�� +
InsertarLogReinicioConteoPqrs
�� ;
(
��; <
obj
��< ?
)
��? @
;
��@ A
}
�� 	
public
�� 
int
�� *
ActualizaConteoPqrsAnalistas
�� /
(
��/ 0
)
��0 1
{
�� 	
return
�� 
DACActualiza
�� 
.
��  *
ActualizaConteoPqrsAnalistas
��  <
(
��< =
)
��= >
;
��> ?
}
�� 	
public
�� 
List
�� 
<
�� 1
#management_buscarAnalistaPqrsResult
�� 7
>
��7 8
AnalistaPqr
��9 D
(
��D E
int
��E H
ciudad
��I O
,
��O P
int
��Q T
regional
��U ]
)
��] ^
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
AnalistaPqr
�� *
(
��* +
ciudad
��+ 1
,
��1 2
regional
��3 ;
)
��; <
;
��< =
}
�� 	
public
�� 
Ref_PQRS_usuarios
��  
GetUsuarioPqrs
��! /
(
��/ 0
string
��0 6
usuario
��7 >
)
��> ?
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetUsuarioPqrs
�� -
(
��- .
usuario
��. 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
Ref_PQRS_usuarios
��  
GetUsuarioPqrsId
��! 1
(
��1 2
int
��2 5
?
��5 6
	idUsuario
��7 @
)
��@ A
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetUsuarioPqrsId
�� /
(
��/ 0
	idUsuario
��0 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_PQRS_Atributo
�� %
>
��% & 
listaAtributosPqrs
��' 9
(
��9 :
)
��: ;
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
listaAtributosPqrs
�� 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� 
sis_usuario
�� 
>
��  "
GetListAuditorCiudad
��! 5
(
��5 6
Int32
��6 ;
?
��; <
regional
��= E
,
��E F
Int32
��G L
?
��L M
ciudad
��N T
,
��T U
ref
��V Y 
MessageResponseOBJ
��Z l
MsgRes
��m s
)
��s t
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
GetListAuditorCiudad
�� 3
(
��3 4
regional
��4 <
,
��< =
ciudad
��> D
,
��D E
ref
��F I
MsgRes
��J P
)
��P Q
;
��Q R
}
�� 	
public
�� 
Int32
�� 
?
�� 
Getidauditor
�� "
(
��" #
string
��# )
nombre
��* 0
)
��0 1
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
Getidauditor
�� +
(
��+ ,
nombre
��, 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� 
vw_ecop_PQRS
��  
>
��  !
ConsultaPQRSId
��" 0
(
��0 1
Int32
��1 6
id_ecop_PQRS
��7 C
)
��C D
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaPQRSId
�� -
(
��- .
id_ecop_PQRS
��. :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� 
vw_ecop_PQRS2
�� !
>
��! "
ConsultaPQRSId2
��# 2
(
��2 3
Int32
��3 8
id_ecop_PQRS
��9 E
)
��E F
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaPQRSId2
�� .
(
��. /
id_ecop_PQRS
��/ ;
)
��; <
;
��< =
}
�� 	
public
�� 
Int32
�� !
InsertarPQRSAuditor
�� (
(
��( )
ecop_PQRS_Auditor
��) :
OBJ
��; >
,
��> ?
ref
��@ C 
MessageResponseOBJ
��D V
MsgRes
��W ]
)
��] ^
{
�� 	
return
�� 

DACInserta
�� 
.
�� !
InsertarPQRSAuditor
�� 1
(
��1 2
OBJ
��2 5
,
��5 6
ref
��7 :
MsgRes
��; A
)
��A B
;
��B C
}
�� 	
public
�� 
Int32
�� $
InsertarPQRSProyeccion
�� +
(
��+ ,$
GestionDocumentalPQRS2
��, B
OBJ
��C F
,
��F G
ref
��H K 
MessageResponseOBJ
��L ^
MsgRes
��_ e
)
��e f
{
�� 	
return
�� 

DACInserta
�� 
.
�� $
InsertarPQRSProyeccion
�� 4
(
��4 5
OBJ
��5 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
Int32
�� 3
%InsertarArchivoPQRRespuestaProyectada
�� :
(
��: ;$
GestionDocumentalPQRS2
��; Q
OBJ
��R U
,
��U V
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 

DACInserta
�� 
.
�� 3
%InsertarArchivoPQRRespuestaProyectada
�� C
(
��C D
OBJ
��D G
,
��G H
ref
��I L
MsgRes
��M S
)
��S T
;
��T U
}
�� 	
public
�� 
Int32
�� 1
#PqrInsertarArchivoRepositorioCierre
�� 8
(
��8 9$
GestionDocumentalPQRS2
��9 O
OBJ
��P S
,
��S T
ref
��U X 
MessageResponseOBJ
��Y k
MsgRes
��l r
)
��r s
{
�� 	
return
�� 

DACInserta
�� 
.
�� 1
#PqrInsertarArchivoRepositorioCierre
�� A
(
��A B
OBJ
��B E
,
��E F
ref
��G J
MsgRes
��K Q
)
��Q R
;
��R S
}
�� 	
public
�� 
int
�� *
InsertarArchivoReaperturaPQR
�� /
(
��/ 0$
GestionDocumentalPQRS2
��0 F
OBJ
��G J
)
��J K
{
�� 	
return
�� 

DACInserta
�� 
.
�� *
InsertarArchivoReaperturaPQR
�� :
(
��: ;
OBJ
��; >
)
��> ?
;
��? @
}
�� 	
public
�� 
Int32
�� $
InsertarPQRSEnrevision
�� +
(
��+ ,"
ecop_PQRS_enrevision
��, @
OBJ
��A D
,
��D E
ref
��F I 
MessageResponseOBJ
��J \
MsgRes
��] c
)
��c d
{
�� 	
return
�� 

DACInserta
�� 
.
�� $
InsertarPQRSEnrevision
�� 4
(
��4 5
OBJ
��5 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
List
�� 
<
�� 
ecop_PQRS_Auditor
�� %
>
��% &!
ConsultaPQRSAuditor
��' :
(
��: ;
Int32
��; @
Id_pqrs
��A H
)
��H I
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
ConsultaPQRSAuditor
�� 2
(
��2 3
Id_pqrs
��3 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� 0
"management_pqrs_auditorListaResult
�� 6
>
��6 7
ListaPqrsAuditor
��8 H
(
��H I
int
��I L
idPqrs
��M S
)
��S T
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ListaPqrsAuditor
�� /
(
��/ 0
idPqrs
��0 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� $
GestionDocumentalPQRS2
�� *
>
��* +
GetUrlProyeccion
��, <
(
��< =
Int32
��= B
Id
��C E
,
��E F
ref
��G J 
MessageResponseOBJ
��K ]
MsgRes
��^ d
)
��d e
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetUrlProyeccion
�� /
(
��/ 0
Id
��0 2
,
��2 3
ref
��4 7
MsgRes
��8 >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� 1
#management_pqrs_mirarArchivosResult
�� 7
>
��7 8
ArchivosPqrs
��9 E
(
��E F
Int32
��F K
idPqr
��L Q
)
��Q R
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ArchivosPqrs
�� +
(
��+ ,
idPqr
��, 1
)
��1 2
;
��2 3
}
�� 	
public
�� $
GestionDocumentalPQRS2
�� %
traerArchivoPqr
��& 5
(
��5 6
int
��6 9
	idArchivo
��: C
)
��C D
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
traerArchivoPqr
�� .
(
��. /
	idArchivo
��/ 8
)
��8 9
;
��9 :
}
�� 	
public
�� $
GestionDocumentalPQRS2
�� %
traerArchivoPqrId
��& 7
(
��7 8
int
��8 ;
idPqr
��< A
)
��A B
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
traerArchivoPqrId
�� 0
(
��0 1
idPqr
��1 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� #
GestionDocumentalPQRS
�� )
>
��) *"
GetUrlDocumentosPqrs
��+ ?
(
��? @
Int32
��@ E
Id
��F H
,
��H I
ref
��J M 
MessageResponseOBJ
��N `
MsgRes
��a g
)
��g h
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
GetUrlDocumentosPqrs
�� 3
(
��3 4
Id
��4 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
void
�� !
ActualizarFechaPQRS
�� '
(
��' (
Int32
��( -
id_ecop_PQRS
��. :
,
��: ;
ref
��< ? 
MessageResponseOBJ
��@ R
MsgRes
��S Y
)
��Y Z
{
�� 	
DACActualiza
�� 
.
�� !
ActualizarFechaPQRS
�� ,
(
��, -
id_ecop_PQRS
��- 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
void
�� +
ActualizaestadoPQRSEnrevision
�� 1
(
��1 2"
ecop_PQRS_enrevision
��2 F
obj
��G J
,
��J K
ref
��L O 
MessageResponseOBJ
��P b
MsgRes
��c i
)
��i j
{
�� 	
DACActualiza
�� 
.
�� +
ActualizaestadoPQRSEnrevision
�� 6
(
��6 7
obj
��7 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
void
�� -
ActualizarGestionPQRSEnrevision
�� 3
(
��3 4"
ecop_PQRS_enrevision
��4 H
obj
��I L
,
��L M
ref
��N Q 
MessageResponseOBJ
��R d
MsgRes
��e k
)
��k l
{
�� 	
DACActualiza
�� 
.
�� -
ActualizarGestionPQRSEnrevision
�� 8
(
��8 9
obj
��9 <
,
��< =
ref
��> A
MsgRes
��B H
)
��H I
;
��I J
}
�� 	
public
�� 
void
�� "
ActualizaReabrirPQRS
�� (
(
��( )
	ecop_PQRS
��) 2
obj
��3 6
,
��6 7
ref
��8 ; 
MessageResponseOBJ
��< N
MsgRes
��O U
)
��U V
{
�� 	
DACActualiza
�� 
.
�� "
ActualizaReabrirPQRS
�� -
(
��- .
obj
��. 1
,
��1 2
ref
��3 6
MsgRes
��7 =
)
��= >
;
��> ?
}
�� 	
public
�� 
void
�� &
ActualizarFechaPQRSDirec
�� ,
(
��, -
Int32
��- 2
id_ecop_PQRS
��3 ?
,
��? @
ref
��A D 
MessageResponseOBJ
��E W
MsgRes
��X ^
)
��^ _
{
�� 	
DACActualiza
�� 
.
�� &
ActualizarFechaPQRSDirec
�� 1
(
��1 2
id_ecop_PQRS
��2 >
,
��> ?
ref
��@ C
MsgRes
��D J
)
��J K
;
��K L
}
�� 	
public
�� 
int
�� "
ActualizarPqrsEstado
�� '
(
��' (
	ecop_PQRS
��( 1
obj
��2 5
,
��5 6
ref
��7 : 
MessageResponseOBJ
��; M
MsgRes
��N T
)
��T U
{
�� 	
return
�� 
DACActualiza
�� 
.
��  "
ActualizarPqrsEstado
��  4
(
��4 5
obj
��5 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
List
�� 
<
�� '
vw_ecop_PQRS_correo_direc
�� -
>
��- . 
ConsultaPQRSCorreo
��/ A
(
��A B
)
��B C
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
ConsultaPQRSCorreo
�� 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
void
�� 
EliminarPQRS
��  
(
��  !
int
��! $
id_ecop_PQRS
��% 1
,
��1 2
ref
��3 6 
MessageResponseOBJ
��7 I
MsgRes
��J P
)
��P Q
{
�� 	

DACElimina
�� 
.
�� 
EliminarPQRS
�� #
(
��# $
id_ecop_PQRS
��$ 0
,
��0 1
ref
��2 5
MsgRes
��6 <
)
��< =
;
��= >
}
�� 	
public
�� 
Int32
�� "
InsertarPQRSEliminar
�� )
(
��) *"
Log_eliminacion_pqrs
��* >
OBJ
��? B
,
��B C
ref
��D G 
MessageResponseOBJ
��H Z
MsgRes
��[ a
)
��a b
{
�� 	
return
�� 

DACInserta
�� 
.
�� "
InsertarPQRSEliminar
�� 2
(
��2 3
OBJ
��3 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
List
�� 
<
�� "
vw_prestadores_lotes
�� (
>
��( )"
GetRecepcionFacturas
��* >
(
��> ?
ref
��? B 
MessageResponseOBJ
��C U
MsgRes
��V \
)
��\ ]
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
GetRecepcionFacturas
�� 3
(
��3 4
ref
��4 7
MsgRes
��8 >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
��  
vw_analistas_lotes
�� &
>
��& '+
GetRecepcionFacturasAnalistas
��( E
(
��E F
ref
��F I 
MessageResponseOBJ
��J \
MsgRes
��] c
)
��c d
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
GetRecepcionFacturasAnalistas
�� <
(
��< =
ref
��= @
MsgRes
��A G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� "
vw_prestadores_lotes
�� (
>
��( )#
GetRecepcionFacturas2
��* ?
(
��? @
ref
��@ C 
MessageResponseOBJ
��D V
MsgRes
��W ]
)
��] ^
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
GetRecepcionFacturas2
�� 4
(
��4 5
ref
��5 8
MsgRes
��9 ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
�� 2
$managment_prestadores_facturasResult
�� 8
>
��8 9&
GetFacturasByIdRecepcion
��: R
(
��R S
int
��S V
idrecepcion
��W b
,
��b c
ref
��d g 
MessageResponseOBJ
��h z
MsgRes��{ �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetFacturasByIdRecepcion
�� 7
(
��7 8
idrecepcion
��8 C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
List
�� 
<
�� 2
$managment_prestadores_facturasResult
�� 8
>
��8 9

GetFactura
��: D
(
��D E
int
��E H
idrecepcion
��I T
,
��T U
int
��V Y
	iddetalle
��Z c
,
��c d
ref
��e h 
MessageResponseOBJ
��i {
MsgRes��| �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 

GetFactura
�� )
(
��) *
idrecepcion
��* 5
,
��5 6
	iddetalle
��7 @
,
��@ A
ref
��B E
MsgRes
��F L
)
��L M
;
��M N
}
�� 	
public
�� 5
'managment_prestadores_facturas_GDResult
�� 6
GetFacturaGD
��7 C
(
��C D
int
��D G
	iddetalle
��H Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetFacturaGD
�� +
(
��+ ,
	iddetalle
��, 5
,
��5 6
ref
��7 :
MsgRes
��; A
)
��A B
;
��B C
}
�� 	
public
�� 9
+managment_prestadores_facturas_GD_zipResult
�� :
GetFacturaGD2
��; H
(
��H I
int
��I L
	iddetalle
��M V
,
��V W
ref
��X [ 
MessageResponseOBJ
��\ n
MsgRes
��o u
)
��u v
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetFacturaGD2
�� ,
(
��, -
	iddetalle
��- 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
List
�� 
<
�� 6
(managmentprestadoresfacturasestadoResult
�� <
>
��< =!
GetFacturasByEstado
��> Q
(
��Q R
int
��R U
idestado
��V ^
,
��^ _
ref
��` c 
MessageResponseOBJ
��d v
MsgRes
��w }
)
��} ~
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
GetFacturasByEstado
�� 2
(
��2 3
idestado
��3 ;
,
��; <
ref
��= @
MsgRes
��A G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� 9
+managmentprestadoresfacturasaceptadasResult
�� ?
>
��? @"
GetFacturasAceptadas
��A U
(
��U V
int
��V Y
idestado
��Z b
,
��b c
int
��d g

id_usuario
��h r
,
��r s
ref
��t w!
MessageResponseOBJ��x �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
GetFacturasAceptadas
�� 3
(
��3 4
idestado
��4 <
,
��< =

id_usuario
��> H
,
��H I
ref
��J M
MsgRes
��N T
)
��T U
;
��U V
}
�� 	
public
�� 
List
�� 
<
�� ;
-managmentprestadoresfacturasaceptadasOKResult
�� A
>
��A B#
GetFacturasAceptadas2
��C X
(
��X Y
ref
��Y \ 
MessageResponseOBJ
��] o
MsgRes
��p v
)
��v w
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
GetFacturasAceptadas2
�� 4
(
��4 5
ref
��5 8
MsgRes
��9 ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
�� ;
-managmentprestadoresfacturasgestionadasResult
�� A
>
��A B 
GetGestionFacturas
��C U
(
��U V
)
��V W
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
GetGestionFacturas
�� 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� A
3managmentprestadoresfacturasgestionadasFechasResult
�� G
>
��G H&
GetGestionFacturasFechas
��I a
(
��a b
DateTime
��b j
fechaIni
��k s
,
��s t
DateTime
��u }
fechaFin��~ �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetGestionFacturasFechas
�� 7
(
��7 8
fechaIni
��8 @
,
��@ A
fechaFin
��B J
)
��J K
;
��K L
}
�� 	
public
�� 
List
�� 
<
�� ;
-managmentprestadoresfacturasgestionadasResult
�� A
>
��A B"
GetGestionFacturasv2
��C W
(
��W X
int
��X [
?
��[ \
	idDetalle
��] f
,
��f g
DateTime
��h p
?
��p q
fechainicial
��r ~
,
��~ 
DateTime��� �
?��� �

fechafinal��� �
,��� �
String��� �
estado��� �
,��� �
int��� �
?��� �
regional��� �
,��� �
String��� �
	prestador��� �
,��� �
String��� �
nit��� �
,��� �
String��� �
numFac��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
GetGestionFacturasv2
�� 3
(
��3 4
	idDetalle
��4 =
,
��= >
fechainicial
��? K
,
��K L

fechafinal
��M W
,
��W X
estado
��Y _
,
��_ `
regional
��a i
,
��i j
	prestador
��k t
,
��t u
nit
��v y
,
��y z
numFac��{ �
)��� �
;��� �
}
�� 	
public
�� 
List
�� 
<
�� C
5managmentprestadoresfacturasgestionadasCompletaResult
�� I
>
��I J"
GetGestionFacturasv3
��K _
(
��_ `
String
��` f
numFac
��g m
,
��m n
String
��o u
nit
��v y
,
��y z
String��{ �
	prestador��� �
,��� �
String��� �
sap��� �
,��� �
int��� �
?��� �
estado��� �
,��� �
int��� �
?��� �
	idDetalle��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
GetGestionFacturasv3
�� 3
(
��3 4
numFac
��4 :
,
��: ;
nit
��< ?
,
��? @
	prestador
��A J
,
��J K
sap
��L O
,
��O P
estado
��Q W
,
��W X
	idDetalle
��Y b
)
��b c
;
��c d
}
�� 	
public
�� 
List
�� 
<
�� G
9managmentprestadoresfacturasgestionadasTrazabilidadResult
�� M
>
��M N,
GetGestionFacturasTrazabilidad
��O m
(
��m n
)
��n o
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
GetGestionFacturasTrazabilidad
�� =
(
��= >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� G
9managmentprestadoresfacturasgestionadasTrazabilidadResult
�� M
>
��M N.
 GetGestionFacturasTrazabilidadV2
��O o
(
��o p
DateTime
��p x
?
��x y
fechainicial��z �
,��� �
DateTime��� �
?��� �

fechafinal��� �
,��� �
String��� �
estado��� �
,��� �
int��� �
?��� �
regional��� �
,��� �
String��� �
	prestador��� �
,��� �
String��� �
nit��� �
,��� �
String��� �
numFac��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� .
 GetGestionFacturasTrazabilidadV2
�� ?
(
��? @
fechainicial
��@ L
,
��L M

fechafinal
��N X
,
��X Y
estado
��Z `
,
��` a
regional
��b j
,
��j k
	prestador
��l u
,
��u v
nit
��w z
,
��z {
numFac��| �
)��� �
;��� �
}
�� 	
public
�� 
List
�� 
<
�� >
0managmentprestadores_estados_factura_totalResult
�� D
>
��D E
GetTotalFacturas
��F V
(
��V W
)
��W X
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetTotalFacturas
�� /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� /
!vw_ref_estado_factura_total_rango
�� 5
>
��5 6'
GetRecepcionFacturasRango
��7 P
(
��P Q
Int32
��Q V
opc
��W Z
)
��Z [
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
GetRecepcionFacturasRango
�� 8
(
��8 9
opc
��9 <
)
��< =
;
��= >
}
�� 	
public
�� 
List
�� 
<
�� 0
"managmentprestadoresFacturasResult
�� 6
>
��6 7*
GetFacturasByEstadoAceptadas
��8 T
(
��T U
int
��U X
idestado
��Y a
,
��a b
ref
��c f 
MessageResponseOBJ
��g y
MsgRes��z �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
GetFacturasByEstadoAceptadas
�� ;
(
��; <
idestado
��< D
,
��D E
ref
��F I
MsgRes
��J P
)
��P Q
;
��Q R
}
�� 	
public
�� 
List
�� 
<
�� :
,managmentprestadoresFacturas_devueltasResult
�� @
>
��@ A*
GetFacturasByEstadoDevueltas
��B ^
(
��^ _
int
��_ b
idestado
��c k
,
��k l
int
��m p
?
��p q
id
��r t
,
��t u
ref
��v y!
MessageResponseOBJ��z �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
GetFacturasByEstadoDevueltas
�� ;
(
��; <
idestado
��< D
,
��D E
id
��F H
,
��H I
ref
��J M
MsgRes
��N T
)
��T U
;
��U V
}
�� 	
public
�� 
List
�� 
<
�� 6
(managmentprestadoresFacturas_rangoResult
�� <
>
��< =/
!GetFacturasByEstadoAceptadasRango
��> _
(
��_ `
int
��` c
rango
��d i
,
��i j
Int32
��k p
id_regional
��q |
)
��| }
{
�� 	
return
�� 
DACConsulta
�� 
.
�� /
!GetFacturasByEstadoAceptadasRango
�� @
(
��@ A
rango
��A F
,
��F G
id_regional
��H S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� 7
)managmentprestadoresFacturasAuditorResult
�� =
>
��= >"
GetFacturasByAuditor
��? S
(
��S T
int
��T W
idestado
��X `
,
��` a
int
��b e

id_usuario
��f p
,
��p q
ref
��r u!
MessageResponseOBJ��v �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
GetFacturasByAuditor
�� 3
(
��3 4
idestado
��4 <
,
��< =

id_usuario
��> H
,
��H I
ref
��J M
MsgRes
��N T
)
��T U
;
��U V
}
�� 	
public
�� 
List
�� 
<
�� 9
+managmentprestadoresFacturasAuditorOKResult
�� ?
>
��? @#
GetFacturasByAuditor2
��A V
(
��V W
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
GetFacturasByAuditor2
�� 4
(
��4 5
ref
��5 8
MsgRes
��9 ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
�� A
3managmentprestadoresFacturasAuditorOKCompletaResult
�� G
>
��G H#
GetFacturasByAuditor3
��I ^
(
��^ _
int
��_ b
	idAuditor
��c l
)
��l m
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
GetFacturasByAuditor3
�� 4
(
��4 5
	idAuditor
��5 >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� 9
+managmentprestadoresFacturasAprobadasResult
�� ?
>
��? @"
GetFacturasAprobadas
��A U
(
��U V
int
��V Y
idestado
��Z b
,
��b c
ref
��d g 
MessageResponseOBJ
��h z
MsgRes��{ �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
GetFacturasAprobadas
�� 3
(
��3 4
idestado
��4 <
,
��< =
ref
��> A
MsgRes
��B H
)
��H I
;
��I J
}
�� 	
public
�� 
List
�� 
<
�� 7
)managmentprestadoresFacturasReporteResult
�� =
>
��= >(
GetFacturasByEstadoReporte
��? Y
(
��Y Z
int
��Z ]
idestado
��^ f
,
��f g
ref
��h k 
MessageResponseOBJ
��l ~
MsgRes�� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
GetFacturasByEstadoReporte
�� 9
(
��9 :
idestado
��: B
,
��B C
ref
��D G
MsgRes
��H N
)
��N O
;
��O P
}
�� 	
public
�� 
List
�� 
<
�� 3
%managmentRechazoFacturasReporteResult
�� 9
>
��9 :)
GetFacturasByRechazoReporte
��; V
(
��V W
int
��W Z
id_dtll
��[ b
,
��b c
ref
��d g 
MessageResponseOBJ
��h z
MsgRes��{ �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
GetFacturasByRechazoReporte
�� :
(
��: ;
id_dtll
��; B
,
��B C
ref
��D G
MsgRes
��H N
)
��N O
;
��O P
}
�� 	
public
�� 
List
�� 
<
�� 7
)managmentRechazoLoteFacturasReporteResult
�� =
>
��= >-
GetLoteFacturasByRechazoReporte
��? ^
(
��^ _
int
��_ b
id_lote
��c j
,
��j k
ref
��l o!
MessageResponseOBJ��p �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� -
GetLoteFacturasByRechazoReporte
�� >
(
��> ?
id_lote
��? F
,
��F G
ref
��H K
MsgRes
��L R
)
��R S
;
��S T
}
�� 	
public
�� 
List
�� 
<
�� ;
-managmentRechazoLoteDtllFacturasReporteResult
�� A
>
��A B1
#GetLoteFacturasdtllByRechazoReporte
��C f
(
��f g
int
��g j
id_lote
��k r
,
��r s
ref
��t w!
MessageResponseOBJ��x �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 1
#GetLoteFacturasdtllByRechazoReporte
�� B
(
��B C
id_lote
��C J
,
��J K
ref
��L O
MsgRes
��P V
)
��V W
;
��W X
}
�� 	
public
�� 
List
�� 
<
�� ;
-managment_prestadores_soportes_clinicosResult
�� A
>
��A B%
GetSoportesClinicosList
��C Z
(
��Z [
int
��[ ^
idcargue
��_ g
,
��g h
int
��i l
detalle
��m t
)
��t u
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
GetSoportesClinicosList
�� 6
(
��6 7
idcargue
��7 ?
,
��? @
detalle
��A H
)
��H I
;
��I J
}
�� 	
public
�� 
List
�� 
<
�� 4
&managment_prestadores_documentosResult
�� :
>
��: ;
GetSoportesList
��< K
(
��K L
int
��L O
detalle
��P W
)
��W X
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetSoportesList
�� .
(
��. /
detalle
��/ 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� -
managment_ffmm_documentosResult
�� 3
>
��3 4!
GetSoportesListFFMM
��5 H
(
��H I
int
��I L
detalle
��M T
)
��T U
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
GetSoportesListFFMM
�� 2
(
��2 3
detalle
��3 :
)
��: ;
;
��; <
}
�� 	
public
�� 6
(management_prestadores_get_soporteResult
�� 7
Getsoporteclinico
��8 I
(
��I J
int
��J M

idsoportee
��N X
)
��X Y
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
Getsoporteclinico
�� 0
(
��0 1

idsoportee
��1 ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� 
ref_rechazos_Fac
�� $
>
��$ %!
Getref_rechazos_Fac
��& 9
(
��9 :
ref
��: = 
MessageResponseOBJ
��> P
MsgRes
��Q W
)
��W X
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
Getref_rechazos_Fac
�� 2
(
��2 3
ref
��3 6
MsgRes
��7 =
)
��= >
;
��> ?
}
�� 	
public
�� 
List
�� 
<
�� "
vw_auditores_totales
�� (
>
��( )
GetAuditorTotales
��* ;
(
��; <
ref
��< ? 
MessageResponseOBJ
��@ R
MsgRes
��S Y
)
��Y Z
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetAuditorTotales
�� 0
(
��0 1
ref
��1 4
MsgRes
��5 ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� '
vw_auditores_totales_pqrs
�� -
>
��- .#
GetAuditorTotalesPqrs
��/ D
(
��D E
ref
��E H 
MessageResponseOBJ
��I [
MsgRes
��\ b
)
��b c
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
GetAuditorTotalesPqrs
�� 4
(
��4 5
ref
��5 8
MsgRes
��9 ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
�� 7
)managment_prestadores_list_rechazosResult
�� =
>
��= >&
GetFacturasByRechazoList
��? W
(
��W X
int
��X [
id_dtll
��\ c
,
��c d
ref
��e h 
MessageResponseOBJ
��i {
MsgRes��| �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetFacturasByRechazoList
�� 7
(
��7 8
id_dtll
��8 ?
,
��? @
ref
��A D
MsgRes
��E K
)
��K L
;
��L M
}
�� 	
public
�� 
void
�� !
ActualizarEnvioPQRS
�� '
(
��' (
Int32
��( -
id_ecop_PQRS
��. :
,
��: ;
String
��< B
usuario
��C J
,
��J K
ref
��L O 
MessageResponseOBJ
��P b
MsgRes
��c i
)
��i j
{
�� 	
DACActualiza
�� 
.
�� !
ActualizarEnvioPQRS
�� ,
(
��, -
id_ecop_PQRS
��- 9
,
��9 :
usuario
��; B
,
��B C
ref
��D G
MsgRes
��H N
)
��N O
;
��O P
}
�� 	
public
�� 
ref_solucionador
�� #
getSolucionadorNombre
��  5
(
��5 6
string
��6 <
nombre
��= C
,
��C D
string
��E K
auxsolucionador
��L [
)
��[ \
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
getSolucionadorNombre
�� 4
(
��4 5
nombre
��5 ;
,
��; <
auxsolucionador
��= L
)
��L M
;
��M N
}
�� 	
public
�� 
ref_solucionador
�� "
TraerAuxSolucionador
��  4
(
��4 5
string
��5 ;
	nombreAux
��< E
)
��E F
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
TraerAuxSolucionador
�� 3
(
��3 4
	nombreAux
��4 =
)
��= >
;
��> ?
}
�� 	
public
�� 
List
�� 
<
�� #
Ref_PQRS_correo_envio
�� )
>
��) *$
ConsultaPQRSref_correo
��+ A
(
��A B
)
��B C
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
ConsultaPQRSref_correo
�� 5
(
��5 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� %
Ref_PQRS_categorizacion
�� +
>
��+ ,(
ConsultaPQRSCategorizacion
��- G
(
��G H
)
��H I
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
ConsultaPQRSCategorizacion
�� 9
(
��9 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� 3
%management_pqrs_tablero_controlResult
�� 9
>
��9 : 
GestiontableroPQRS
��; M
(
��M N
)
��N O
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
GestiontableroPQRS
�� 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� F
8management_pqrs_tablero_control_proyectadasFinalesResult
�� L
>
��L M0
"DatosTableroPqrsProyectadasFinales
��N p
(
��p q
)
��q r
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 0
"DatosTableroPqrsProyectadasFinales
�� A
(
��A B
)
��B C
;
��C D
}
�� 	
public
�� 
List
�� 
<
�� 5
'management_pqrs_proyectadasCierreResult
�� ;
>
��; </
!DatosTableroPqrsProyectadasCierre
��= ^
(
��^ _
)
��_ `
{
�� 	
return
�� 
DACConsulta
�� 
.
�� /
!DatosTableroPqrsProyectadasCierre
�� @
(
��@ A
)
��A B
;
��B C
}
�� 	
public
�� 
List
�� 
<
�� ?
1management_pqrs_tablero_control_proyectadasResult
�� E
>
��E F+
GestiontableroPQRSProyectadas
��G d
(
��d e
string
��e k
numCaso
��l s
,
��s t
string
��u {
numOpc��| �
,��� �
string��� �
numDocumento��� �
,��� �
DateTime��� �
?��� �
fechaInicial��� �
,��� �
DateTime��� �
?��� �

fechaFinal��� �
,��� �
int��� �
?��� �
idPqr��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
GestiontableroPQRSProyectadas
�� <
(
��< =
numCaso
��= D
,
��D E
numOpc
��F L
,
��L M
numDocumento
��N Z
,
��Z [
fechaInicial
��\ h
,
��h i

fechaFinal
��j t
,
��t u
idPqr
��v {
)
��{ |
;
��| }
}
�� 	
public
�� 
List
�� 
<
�� 6
(management_pqrs_TableroseguimientoResult
�� <
>
��< =,
GestiontableeroSeguimientoPqrs
��> \
(
��\ ]
string
��] c
usuario
��d k
,
��k l
string
��m s
solucionador��t �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
GestiontableeroSeguimientoPqrs
�� =
(
��= >
usuario
��> E
,
��E F
solucionador
��G S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� 
ref_solucionador
�� $
>
��$ %
getSolucionador
��& 5
(
��5 6
int
��6 9
idCiudad
��: B
)
��B C
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
getSolucionador
�� .
(
��. /
idCiudad
��/ 7
)
��7 8
;
��8 9
}
�� 	
public
�� 
List
�� 
<
�� 
ref_solucionador
�� $
>
��$ % 
getSolucionadorReg
��& 8
(
��8 9
int
��9 <

idRegional
��= G
)
��G H
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
getSolucionadorReg
�� 1
(
��1 2

idRegional
��2 <
)
��< =
;
��= >
}
�� 	
public
�� 
List
�� 
<
�� /
!management_ref_solucionadorResult
�� 5
>
��5 6'
getSolucionadorRegActivos
��7 P
(
��P Q
int
��Q T

idRegional
��U _
)
��_ `
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
getSolucionadorRegActivos
�� 8
(
��8 9

idRegional
��9 C
)
��C D
;
��D E
}
�� 	
public
�� 
List
�� 
<
�� 
ref_solucionador
�� $
>
��$ %%
getSolucionadorRegional
��& =
(
��= >
int
��> A
?
��A B

idRegional
��C M
)
��M N
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
getSolucionadorRegional
�� 6
(
��6 7

idRegional
��7 A
)
��A B
;
��B C
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_ciudades
��  
>
��  !
TotalCiudades
��" /
(
��/ 0
)
��0 1
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
TotalCiudades
�� ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
Ref_ciudades
�� 

CiudadesId
�� &
(
��& '
int
��' *
?
��* +
id
��, .
)
��. /
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 

CiudadesId
�� )
(
��) *
id
��* ,
)
��, -
;
��- .
}
�� 	
public
�� 
List
�� 
<
�� 
ref_solucionador
�� $
>
��$ %"
getSolucionadorTotal
��& :
(
��: ;
)
��; <
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
getSolucionadorTotal
�� 3
(
��3 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
List
�� 
<
�� 2
$Management_PQRS_solucionadoresResult
�� 8
>
��8 9 
getSolucionadorAux
��: L
(
��L M
)
��M N
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
getSolucionadorAux
�� 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
int
�� '
ActualizarUsuarioAsignado
�� ,
(
��, -
	ecop_PQRS
��- 6
OBJ
��7 :
,
��: ;
ref
��< ? 
MessageResponseOBJ
��@ R
MsgRes
��S Y
)
��Y Z
{
�� 	
return
�� 
DACActualiza
�� 
.
��  '
ActualizarUsuarioAsignado
��  9
(
��9 :
OBJ
��: =
,
��= >
ref
��? B
MsgRes
��C I
)
��I J
;
��J K
}
�� 	
public
�� 
int
�� )
ActualizarCategorizacionPQR
�� .
(
��. /
	ecop_PQRS
��/ 8
OBJ
��9 <
,
��< =
ref
��> A 
MessageResponseOBJ
��B T
MsgRes
��U [
)
��[ \
{
�� 	
return
�� 
DACActualiza
�� 
.
��  )
ActualizarCategorizacionPQR
��  ;
(
��; <
OBJ
��< ?
,
��? @
ref
��A D
MsgRes
��E K
)
��K L
;
��L M
}
�� 	
public
�� 
List
�� 
<
�� 8
*management_facturas_sinDocumentacionResult
�� >
>
��> ?&
ListaFacturasIncompletas
��@ X
(
��X Y
)
��Y Z
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
ListaFacturasIncompletas
�� 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
int
�� )
ActualizarAvanzarProyectada
�� .
(
��. /
	ecop_PQRS
��/ 8
OBJ
��9 <
,
��< =
ref
��> A 
MessageResponseOBJ
��B T
MsgRes
��U [
)
��[ \
{
�� 	
return
�� 
DACActualiza
�� 
.
��  )
ActualizarAvanzarProyectada
��  ;
(
��; <
OBJ
��< ?
,
��? @
ref
��A D
MsgRes
��E K
)
��K L
;
��L M
}
�� 	
public
�� 
int
�� (
ActualizarCerrarProyectada
�� -
(
��- .
	ecop_PQRS
��. 7
OBJ
��8 ;
,
��; <
ref
��= @ 
MessageResponseOBJ
��A S
MsgRes
��T Z
)
��Z [
{
�� 	
return
�� 
DACActualiza
�� 
.
��  (
ActualizarCerrarProyectada
��  :
(
��: ;
OBJ
��; >
,
��> ?
ref
��@ C
MsgRes
��D J
)
��J K
;
��K L
}
�� 	
public
�� 
int
�� *
ActualizarDatosReaperturaPQR
�� /
(
��/ 0
	ecop_PQRS
��0 9
OBJ
��: =
)
��= >
{
�� 	
return
�� 
DACActualiza
�� 
.
��  *
ActualizarDatosReaperturaPQR
��  <
(
��< =
OBJ
��= @
)
��@ A
;
��A B
}
�� 	
public
�� 
int
�� '
InsertarLogReaperturaPqrs
�� ,
(
��, -"
log_pqrs_reaperturas
��- A
obj
��B E
)
��E F
{
�� 	
return
�� 

DACInserta
�� 
.
�� '
InsertarLogReaperturaPqrs
�� 7
(
��7 8
obj
��8 ;
)
��; <
;
��< =
}
�� 	
public
�� 
int
�� /
!InsertarLogCierrePqrsSolucionador
�� 4
(
��4 5+
log_pqrs_cerradasSolucionador
��5 R
obj
��S V
)
��V W
{
�� 	
return
�� 

DACInserta
�� 
.
�� /
!InsertarLogCierrePqrsSolucionador
�� ?
(
��? @
obj
��@ C
)
��C D
;
��D E
}
�� 	
public
�� 
int
�� )
CargueMedicamentosRegulados
�� .
(
��. /&
ecop_pqrs_registroMasivo
��/ G
obj
��H K
,
��K L
List
��M Q
<
��Q R
	ecop_PQRS
��R [
>
��[ \
detalle
��] d
,
��d e
ref
��f i 
MessageResponseOBJ
��j |
MsgRes��} �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� )
CargueMedicamentosRegulados
�� 9
(
��9 :
obj
��: =
,
��= >
detalle
��? F
,
��F G
ref
��H K
MsgRes
��L R
)
��R S
;
��S T
}
�� 	
public
�� 
List
�� 
<
�� 
	ecop_PQRS
�� 
>
�� 
ListadoPqrsMasivo
�� 0
(
��0 1
int
��1 4
idMasivo
��5 =
)
��= >
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ListadoPqrsMasivo
�� 0
(
��0 1
idMasivo
��1 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
int
�� +
ActualizarAnalistaAsignadoPqr
�� 0
(
��0 1
	ecop_PQRS
��1 :
obj
��; >
)
��> ?
{
�� 	
return
�� 
DACActualiza
�� 
.
��  +
ActualizarAnalistaAsignadoPqr
��  =
(
��= >
obj
��> A
)
��A B
;
��B C
}
�� 	
public
�� 
int
�� +
PqrGuardarObservaciopnAuditor
�� 0
(
��0 1,
ecop_pqrs_observacionesAuditor
��1 O
obj
��P S
)
��S T
{
�� 	
return
�� 

DACInserta
�� 
.
�� +
PqrGuardarObservaciopnAuditor
�� ;
(
��; <
obj
��< ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
�� 8
*management_pqrs_observacionesAuditorResult
�� >
>
��> ?+
PqrsListaObservacionesAuditor
��@ ]
(
��] ^
int
��^ a
idPqrs
��b h
)
��h i
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
PqrsListaObservacionesAuditor
�� <
(
��< =
idPqrs
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
int
�� (
CargueMasivoQuienLlamoPqrs
�� -
(
��- .
List
��. 2
<
��2 3%
ecop_pqrs_a_quien_llamo
��3 J
>
��J K
detalle
��L S
,
��S T
ref
��U X 
MessageResponseOBJ
��Y k
MsgRes
��l r
)
��r s
{
�� 	
return
�� 

DACInserta
�� 
.
�� (
CargueMasivoQuienLlamoPqrs
�� 8
(
��8 9
detalle
��9 @
,
��@ A
ref
��B E
MsgRes
��F L
)
��L M
;
��M N
}
�� 	
public
�� 
List
�� 
<
�� 5
'management_pqrs_consolidadoMasivoResult
�� ;
>
��; <
PqrsListaMasivos
��= M
(
��M N
int
��N Q
?
��Q R
	idUsuario
��S \
)
��\ ]
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
PqrsListaMasivos
�� /
(
��/ 0
	idUsuario
��0 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
List
�� 
<
�� =
/management_pqrs_consolidadoMasivo_detalleResult
�� C
>
��C D%
PqrsListaMasivosDetalle
��E \
(
��\ ]
int
��] `
?
��` a
idMasivo
��b j
,
��j k
int
��l o
?
��o p
	idUsuario
��q z
)
��z {
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
PqrsListaMasivosDetalle
�� 6
(
��6 7
idMasivo
��7 ?
,
��? @
	idUsuario
��A J
)
��J K
;
��K L
}
�� 	
public
�� 
List
�� 
<
�� 5
'management_pqrs_sinArchivoInicialResult
�� ;
>
��; <*
listadoPqrsInicialSinArchivo
��= Y
(
��Y Z
int
��Z ]
?
��] ^
	idUsuario
��_ h
)
��h i
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
listadoPqrsInicialSinArchivo
�� ;
(
��; <
	idUsuario
��< E
)
��E F
;
��F G
}
�� 	
public
�� 
int
�� "
insertarDatosCorreos
�� '
(
��' ($
ecop_pqrs_envioCorreos
��( >
obj
��? B
)
��B C
{
�� 	
return
�� 

DACInserta
�� 
.
�� "
insertarDatosCorreos
�� 2
(
��2 3
obj
��3 6
)
��6 7
;
��7 8
}
�� 	
public
�� $
ecop_pqrs_envioCorreos
�� %"
LlamarPqrsCorreoById
��& :
(
��: ;
int
��; >
id
��? A
)
��A B
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
LlamarPqrsCorreoById
�� 3
(
��3 4
id
��4 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
int
�� -
ActualizarPasaArchivoPqrinicial
�� 2
(
��2 3
	ecop_PQRS
��3 <
obj
��= @
)
��@ A
{
�� 	
return
�� 
DACActualiza
�� 
.
��  -
ActualizarPasaArchivoPqrinicial
��  ?
(
��? @
obj
��@ C
)
��C D
;
��D E
}
�� 	
public
�� 
int
�� '
CerrarCasoPqrSolucionador
�� ,
(
��, -
	ecop_PQRS
��- 6
obj
��7 :
)
��: ;
{
�� 	
return
�� 
DACActualiza
�� 
.
��  '
CerrarCasoPqrSolucionador
��  9
(
��9 :
obj
��: =
)
��= >
;
��> ?
}
�� 	
public
�� 
	ecop_PQRS
�� "
buscarNumeroCasoPqrs
�� -
(
��- .
string
��. 4
numero_caso
��5 @
)
��@ A
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
buscarNumeroCasoPqrs
�� 3
(
��3 4
numero_caso
��4 ?
)
��? @
;
��@ A
}
�� 	
public
�� 6
(management_devolverFechaHabil_diasResult
�� 7!
DevolverDiasHabiles
��8 K
(
��K L
DateTime
��L T
?
��T U
fecha
��V [
,
��[ \
int
��] `
?
��` a
tipoSolicitud
��b o
)
��o p
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
DevolverDiasHabiles
�� 2
(
��2 3
fecha
��3 8
,
��8 9
tipoSolicitud
��: G
)
��G H
;
��H I
}
�� 	
public
�� /
!management_pqrs_detalleCasoResult
�� 0&
DetallePqrsReporteCorreo
��1 I
(
��I J
int
��J M
?
��M N
idPqr
��O T
)
��T U
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
DetallePqrsReporteCorreo
�� 7
(
��7 8
idPqr
��8 =
)
��= >
;
��> ?
}
�� 	
public
�� 
List
�� 
<
�� 7
)management_pqrs_PorcentajeAuditoresResult
�� =
>
��= >*
listadoPQRSAuditorPorcentaje
��? [
(
��[ \
string
��\ b
auditor
��c j
)
��j k
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
listadoPQRSAuditorPorcentaje
�� ;
(
��; <
auditor
��< C
)
��C D
;
��D E
}
�� 	
public
�� "
vw_auditores_totales
�� #
GetAuditorNombre
��$ 4
(
��4 5
string
��5 ;
nombre
��< B
)
��B C
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetAuditorNombre
�� /
(
��/ 0
nombre
��0 6
)
��6 7
;
��7 8
}
�� 	
public
�� '
vw_auditores_totales_pqrs
�� ("
GetAuditorNombrePqrs
��) =
(
��= >
string
��> D
nombre
��E K
)
��K L
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
GetAuditorNombrePqrs
�� 3
(
��3 4
nombre
��4 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� 
Ref_procesos
��  
>
��  !
GetProcesosGD
��" /
(
��/ 0
)
��0 1
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetProcesosGD
��! .
(
��. /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
List
�� 
<
�� )
Ref_gestion_tipo_documental
�� /
>
��/ 0+
ConsultaGestionTipoDocumental
��1 N
(
��N O
Int32
��O T
	idproceso
��U ^
)
��^ _
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
ConsultaGestionTipoDocumental
�� <
(
��< =
	idproceso
��= F
)
��F G
;
��G H
}
�� 	
public
�� #
vw_md_consolidado_fac
�� $
MD_CosolidadofAC
��% 5
(
��5 6
String
��6 <
numero_factura
��= K
)
��K L
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
MD_CosolidadofAC
�� /
(
��/ 0
numero_factura
��0 >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� #
vw_md_consolidado_fac
�� )
>
��) *%
MD_CosolidadofACDetalle
��+ B
(
��B C
String
��C I
numero_factura
��J X
)
��X Y
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
MD_CosolidadofACDetalle
�� 6
(
��6 7
numero_factura
��7 E
)
��E F
;
��F G
}
�� 	
public
�� 
List
�� 
<
�� #
vw_md_consolidado_fac
�� )
>
��) *
MD_CosolidadofAC2
��+ <
(
��< =
String
��= C
factura
��D K
)
��K L
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
MD_CosolidadofAC2
�� 0
(
��0 1
factura
��1 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� )
Ref_gestion_tipo_documental
�� /
>
��/ 0
ConsultaCodigoGD
��1 A
(
��A B)
Ref_gestion_tipo_documental
��B ]
objBusqueda
��^ i
,
��i j
ref
��k n!
MessageResponseOBJ��o �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaCodigoGD
�� /
(
��/ 0
objBusqueda
��0 ;
,
��; <
ref
��= @
MsgRes
��A G
)
��G H
;
��H I
}
�� 	
public
�� 
Int32
��  
InsertarGestionDoc
�� '
(
��' (+
GestionDocumentalMedicamentos
��( E
ObjobjGD
��F N
,
��N O
ref
��P S 
MessageResponseOBJ
��T f
MsgRes
��g m
)
��m n
{
�� 	
return
�� 

DACInserta
�� 
.
��  
InsertarGestionDoc
�� 0
(
��0 1
ObjobjGD
��1 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
Int32
�� *
InsertarGestionDocMedCalidad
�� 1
(
��1 2.
 GestionDocumentalMedicamentosCad
��2 R
ObjobjGD
��S [
,
��[ \
ref
��] ` 
MessageResponseOBJ
��a s
MsgRes
��t z
)
��z {
{
�� 	
return
�� 

DACInserta
�� 
.
�� *
InsertarGestionDocMedCalidad
�� :
(
��: ;
ObjobjGD
��; C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
void
�� $
InsertarGestionDocPQRS
�� *
(
��* +#
GestionDocumentalPQRS
��+ @
Obj
��A D
,
��D E
ref
��F I 
MessageResponseOBJ
��J \
MsgRes
��] c
)
��c d
{
�� 	

DACInserta
�� 
.
�� $
InsertarGestionDocPQRS
�� -
(
��- .
Obj
��. 1
,
��1 2
ref
��3 6
MsgRes
��7 =
)
��= >
;
��> ?
}
�� 	
public
�� 
void
�� .
 InsertarGestionDocVisitasCalidad
�� 4
(
��4 5-
GestionDocumentalVisitasCalidad
��5 T
Obj
��U X
,
��X Y
ref
��Z ] 
MessageResponseOBJ
��^ p
MsgRes
��q w
)
��w x
{
�� 	

DACInserta
�� 
.
�� .
 InsertarGestionDocVisitasCalidad
�� 7
(
��7 8
Obj
��8 ;
,
��; <
ref
��= @
MsgRes
��A G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� *
vw_g_documental_medicamentos
�� 0
>
��0 1
ConsultaFactura
��2 A
(
��A B
String
��B H
FacMedicamentos
��I X
)
��X Y
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaFactura
�� .
(
��. /
FacMedicamentos
��/ >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� *
vw_g_documental_medicamentos
�� 0
>
��0 1
ConsultaDocumento
��2 C
(
��C D
Decimal
��D K
DocMedicamentos
��L [
)
��[ \
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaDocumento
�� 0
(
��0 1
DocMedicamentos
��1 @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
��  
vw_fac_consolidado
�� &
>
��& '
ConsultaFactura2
��( 8
(
��8 9
String
��9 ?
FacMedicamentos
��@ O
)
��O P
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaFactura2
�� /
(
��/ 0
FacMedicamentos
��0 ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
��  
vw_fac_consolidado
�� &
>
��& ' 
ConsultaDocumento2
��( :
(
��: ;
String
��; A
DocMedicamentos
��B Q
)
��Q R
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
ConsultaDocumento2
�� 1
(
��1 2
DocMedicamentos
��2 A
)
��A B
;
��B C
}
�� 	
public
�� *
vw_g_documental_medicamentos
�� +)
ConsultaIdGestionDocumental
��, G
(
��G H
Int32
��H M#
id_gestion_documental
��N c
,
��c d
ref
��e h 
MessageResponseOBJ
��i {
MsgRes��| �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
ConsultaIdGestionDocumental
�� :
(
��: ;#
id_gestion_documental
��; P
,
��P Q
ref
��R U
MsgRes
��V \
)
��\ ]
;
��] ^
}
�� 	
public
�� 
List
�� 
<
�� *
vw_g_documental_medicamentos
�� 0
>
��0 1*
ConsultaIdGestionDocumental2
��2 N
(
��N O
Int32
��O T#
id_gestion_documental
��U j
,
��j k
ref
��l o!
MessageResponseOBJ��p �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
ConsultaIdGestionDocumental2
�� ;
(
��; <#
id_gestion_documental
��< Q
,
��Q R
ref
��S V
MsgRes
��W ]
)
��] ^
;
��^ _
}
�� 	
public
�� 
List
�� 
<
�� *
vw_g_documental_medicamentos
�� 0
>
��0 10
"ConsultaIdGestionDocumentalFormula
��2 T
(
��T U
String
��U [
formula
��\ c
,
��c d
ref
��e h 
MessageResponseOBJ
��i {
MsgRes��| �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 0
"ConsultaIdGestionDocumentalFormula
�� A
(
��A B
formula
��B I
,
��I J
ref
��K N
MsgRes
��O U
)
��U V
;
��V W
}
�� 	
public
�� 
List
�� 
<
�� 0
"vw_gestion_documental_med_cad_dtll
�� 6
>
��6 72
$ConsultaIdGestionDocumentalMDCalidad
��8 \
(
��\ ]
Int32
��] b)
id_indicadores_medicamentos
��c ~
,
��~ 
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 2
$ConsultaIdGestionDocumentalMDCalidad
�� C
(
��C D)
id_indicadores_medicamentos
��D _
,
��_ `
ref
��a d
MsgRes
��e k
)
��k l
;
��l m
}
�� 	
public
�� 
	ecop_PQRS
�� #
ConsultaPQRSbyNumCaso
�� .
(
��. /
string
��/ 5
numcaso
��6 =
)
��= >
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
ConsultaPQRSbyNumCaso
�� 4
(
��4 5
numcaso
��5 <
)
��< =
;
��= >
}
�� 	
public
�� #
GestionDocumentalPQRS
�� $$
ConsultaGestorPQRSbyId
��% ;
(
��; <
Int32
��< A
Id
��B D
)
��D E
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
ConsultaGestorPQRSbyId
�� 5
(
��5 6
Id
��6 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� #
GestionDocumentalPQRS
�� )
>
��) *2
$ConsultanumcasoGestionDocumentalPQRS
��+ O
(
��O P
string
��P V
numcaso
��W ^
)
��^ _
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 2
$ConsultanumcasoGestionDocumentalPQRS
�� C
(
��C D
numcaso
��D K
)
��K L
;
��L M
}
�� 	
public
�� 
void
�� '
EliminarDocumento_med_cal
�� -
(
��- .
Int32
��. 3
id
��4 6
,
��6 7
ref
��8 ; 
MessageResponseOBJ
��< N
MsgRes
��O U
)
��U V
{
�� 	

DACElimina
�� 
.
�� '
EliminarDocumento_med_cal
�� 0
(
��0 1
id
��1 3
,
��3 4
ref
��5 8
MsgRes
��9 ?
)
��? @
;
��@ A
}
�� 	
public
�� 
void
�� #
EliminarDocumento_med
�� )
(
��) *
Int32
��* /
id
��0 2
,
��2 3
ref
��4 7 
MessageResponseOBJ
��8 J
MsgRes
��K Q
)
��Q R
{
�� 	

DACElimina
�� 
.
�� #
EliminarDocumento_med
�� ,
(
��, -
id
��- /
,
��/ 0
ref
��1 4
MsgRes
��5 ;
)
��; <
;
��< =
}
�� 	
public
�� 
bool
�� 
EliminarDocPQRS
�� #
(
��# $
Int32
��$ )
id
��* ,
,
��, -
ref
��. 1 
MessageResponseOBJ
��2 D
MsgRes
��E K
)
��K L
{
�� 	
return
�� 

DACElimina
�� 
.
�� 
EliminarDocPQRS
�� -
(
��- .
id
��. 0
,
��0 1
ref
��2 5
MsgRes
��6 <
)
��< =
;
��= >
}
�� 	
public
�� 
void
�� "
InsertarLogActividad
�� (
(
��( )#
Log_GestionDocumental
��) >
Log
��? B
)
��B C
{
�� 	

DACInserta
�� 
.
�� "
InsertarLogActividad
�� +
(
��+ ,
Log
��, /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
List
�� 
<
�� +
GestionDocumentalMedicamentos
�� 1
>
��1 2
TraerPdf
��3 ;
(
��; <
)
��< =
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
TraerPdf
��! )
(
��) *
)
��* +
;
��+ ,
}
�� 	
public
�� 
String
�� #
ActualizarRutaByteMed
�� +
(
��+ ,+
GestionDocumentalMedicamentos
��, I
obj
��J M
,
��M N
ref
��O R 
MessageResponseOBJ
��S e
MsgRes
��f l
)
��l m
{
�� 	
return
�� 
DACActualiza
�� 
.
��  #
ActualizarRutaByteMed
��  5
(
��5 6
obj
��6 9
,
��9 :
ref
��; >
MsgRes
��? E
)
��E F
;
��F G
}
�� 	
public
�� 
String
�� (
ActualizarRutasDocsVisitas
�� 0
(
��0 1)
cronograma_visita_documento
��1 L
obj
��M P
,
��P Q
ref
��R U 
MessageResponseOBJ
��V h
MsgRes
��i o
)
��o p
{
�� 	
return
�� 
DACActualiza
�� 
.
��  (
ActualizarRutasDocsVisitas
��  :
(
��: ;
obj
��; >
,
��> ?
ref
��@ C
MsgRes
��D J
)
��J K
;
��K L
}
�� 	
public
�� 
void
�� 6
(ActualizarRutaDocumentoVisitasCronograma
�� <
(
��< =
string
��= C
ruta
��D H
,
��H I
int
��J M
?
��M N
idVisita
��O W
)
��W X
{
�� 	
DACActualiza
�� 
.
�� 6
(ActualizarRutaDocumentoVisitasCronograma
�� A
(
��A B
ruta
��B F
,
��F G
idVisita
��H P
)
��P Q
;
��Q R
}
�� 	
public
�� 
String
�� $
ActualizarRutaBytePQRS
�� ,
(
��, -$
GestionDocumentalPQRS2
��- C
obj
��D G
,
��G H
ref
��I L 
MessageResponseOBJ
��M _
MsgRes
��` f
)
��f g
{
�� 	
return
�� 
DACActualiza
�� 
.
��  $
ActualizarRutaBytePQRS
��  6
(
��6 7
obj
��7 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
int
�� '
insertarConteoAnalistaPQR
�� ,
(
��, -
int
��- 0

idAnalista
��1 ;
,
��; <
int
��= @
	idUsuario
��A J
)
��J K
{
�� 	
return
�� 

DACInserta
�� 
.
�� '
insertarConteoAnalistaPQR
�� 7
(
��7 8

idAnalista
��8 B
,
��B C
	idUsuario
��D M
)
��M N
;
��N O
}
�� 	
public
�� 
String
�� *
ActualizarRutaByteMedCalidad
�� 2
(
��2 3.
 GestionDocumentalMedicamentosCad
��3 S
obj
��T W
,
��W X
ref
��Y \ 
MessageResponseOBJ
��] o
MsgRes
��p v
)
��v w
{
�� 	
return
�� 
DACActualiza
�� 
.
��  *
ActualizarRutaByteMedCalidad
��  <
(
��< =
obj
��= @
,
��@ A
ref
��B E
MsgRes
��F L
)
��L M
;
��M N
}
�� 	
public
�� 
List
�� 
<
�� +
GestionDocumentalMedicamentos
�� 1
>
��1 2&
ConsultaGestionMedCargue
��3 K
(
��K L
)
��L M
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
ConsultaGestionMedCargue
�� 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� 1
#vw_g_documental_medicamentos_masivo
�� 7
>
��7 8%
GestionDocumentalmasivo
��9 P
(
��P Q
)
��Q R
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
GestionDocumentalmasivo
�� 6
(
��6 7
)
��7 8
;
��8 9
}
�� 	
public
�� 
List
�� 
<
�� *
management_masivo_pqrsResult
�� 0
>
��0 1&
GestionDocumentalmasivo2
��2 J
(
��J K
)
��K L
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GestionDocumentalmasivo2
�� 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
List
�� 
<
�� !
md_Ref_com_dirigido
�� '
>
��' (
GetDirigido
��) 4
(
��4 5
)
��5 6
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetDirigido
��! ,
(
��, -
)
��- .
;
��. /
}
�� 	
public
�� 
List
�� 
<
�� 
md_Ref_com_tipo
�� #
>
��# $
	GetMdTipo
��% .
(
��. /
)
��/ 0
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
	GetMdTipo
��! *
(
��* +
)
��+ ,
;
��, -
}
�� 	
public
�� 
List
�� 
<
��  
md_ref_tipo_visita
�� &
>
��& '
GetMdTipoVisita
��( 7
(
��7 8
)
��8 9
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetMdTipoVisita
��! 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
void
�� $
InsertarComunicaciones
�� *
(
��* +
md_comunicaciones
��+ <
OBJ
��= @
,
��@ A
ref
��B E 
MessageResponseOBJ
��F X
MsgRes
��Y _
)
��_ `
{
�� 	

DACInserta
�� 
.
�� $
InsertarComunicaciones
�� -
(
��- .
OBJ
��. 1
,
��1 2
ref
��3 6
MsgRes
��7 =
)
��= >
;
��> ?
}
�� 	
public
�� 
void
�� "
InsertarCronoVisitas
�� (
(
��( )
md_crono_visita
��) 8
OBJ
��9 <
,
��< =
ref
��> A 
MessageResponseOBJ
��B T
MsgRes
��U [
)
��[ \
{
�� 	

DACInserta
�� 
.
�� "
InsertarCronoVisitas
�� +
(
��+ ,
OBJ
��, /
,
��/ 0
ref
��1 4
MsgRes
��5 ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� 2
$ManagmentRefPuntosDispersacionResult
�� 8
>
��8 9
ConsultaListaPD
��: I
(
��I J
int
��J M
opc
��N Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaListaPD
�� .
(
��. /
opc
��/ 2
,
��2 3
ref
��4 7
MsgRes
��8 >
)
��> ?
;
��? @
}
�� 	
public
�� 0
"vw_gestion_documental_med_cad_dtll
�� 10
"ConsultaIdGestionDocumentalMDCalId
��2 T
(
��T U
Int32
��U Z6
'id_gestion_documental__medicamentos_cad��[ �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 0
"ConsultaIdGestionDocumentalMDCalId
�� A
(
��A B5
'id_gestion_documental__medicamentos_cad
��B i
,
��i j
ref
��k n
MsgRes
��o u
)
��u v
;
��v w
}
�� 	
public
�� 
List
�� 
<
�� ,
ManagmentFacMedicamentosResult
�� 2
>
��2 3#
CuentaFacMedicamentos
��4 I
(
��I J
String
��J P
factura
��Q X
,
��X Y
String
��Z `
formula
��a h
,
��h i
Int32
��j o
OPC
��p s
,
��s t
ref
��u x!
MessageResponseOBJ��y �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
CuentaFacMedicamentos
�� 4
(
��4 5
factura
��5 <
,
��< =
formula
��> E
,
��E F
OPC
��G J
,
��J K
ref
��L O
MsgRes
��P V
)
��V W
;
��W X
}
�� 	
public
�� 
List
�� 
<
�� /
!Managment_md_tablerocontrolResult
�� 5
>
��5 6%
CuentaFacTableroControl
��7 N
(
��N O
DateTime
��O W
fecha_inicial
��X e
,
��e f
DateTime
��g o
fecha_final
��p {
,
��{ |
ref��} �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
CuentaFacTableroControl
�� 6
(
��6 7
fecha_inicial
��7 D
,
��D E
fecha_final
��F Q
,
��Q R
ref
��S V
MsgRes
��W ]
)
��] ^
;
��^ _
}
�� 	
public
�� 
List
�� 
<
�� 7
)Managment_md_tablero_ConciliacionesResult
�� =
>
��= >3
%CuentaFacTableroControlConciliaciones
��? d
(
��d e
ref
��e h 
MessageResponseOBJ
��i {
MsgRes��| �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 3
%CuentaFacTableroControlConciliaciones
�� D
(
��D E
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
List
�� 
<
�� ?
1Managment_md_tablero_Conciliaciones_detalleResult
�� E
>
��E F7
)CuentaFacTableroControlConciliacionesdtll
��G p
(
��p q
ref
��q t!
MessageResponseOBJ��u �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 7
)CuentaFacTableroControlConciliacionesdtll
�� H
(
��H I
ref
��I L
MsgRes
��M S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� 3
%ManagmentFacMedicamentosDetalleResult
�� 9
>
��9 :*
CuentaFacMedicamentosDetalle
��; W
(
��W X
String
��X ^
factura
��_ f
,
��f g
String
��h n
formula
��o v
,
��v w
Int32
��x }
OPC��~ �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� *
CuentaFacMedicamentosDetalle
�� ;
(
��; <
factura
��< C
,
��C D
formula
��E L
,
��L M
OPC
��N Q
,
��Q R
ref
��S V
MsgRes
��W ]
)
��] ^
;
��^ _
}
�� 	
public
�� 
List
�� 
<
�� (
md_Ref_resultado_auditoria
�� .
>
��. /
GetResultadoAUD
��0 ?
(
��? @
)
��@ A
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetResultadoAUD
��! 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
Int32
�� 
InsertarGlosaMD
�� $
(
��$ %
md_glosa
��% -
OBJGlosa
��. 6
,
��6 7
ref
��8 ; 
MessageResponseOBJ
��< N
MsgRes
��O U
)
��U V
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarGlosaMD
�� -
(
��- .
OBJGlosa
��. 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� $
InsertarGlosaDetalleMD
�� +
(
��+ ,
md_glosa_detalle
��, <
OBJGlosaDetalle
��= L
,
��L M
ref
��N Q 
MessageResponseOBJ
��R d
MsgRes
��e k
)
��k l
{
�� 	
return
�� 

DACInserta
�� 
.
�� $
InsertarGlosaDetalleMD
�� 4
(
��4 5
OBJGlosaDetalle
��5 D
,
��D E
ref
��F I
MsgRes
��J P
)
��P Q
;
��Q R
}
�� 	
public
�� 
List
�� 
<
�� #
vw_glosa_medicamentos
�� )
>
��) *
ConsultaGlosa
��+ 8
(
��8 9
String
��9 ?
formula
��@ G
)
��G H
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaGlosa
�� ,
(
��, -
formula
��- 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
void
�� 
EliminarGlosa
�� !
(
��! "
Int32
��" '
id
��( *
,
��* +
ref
��, / 
MessageResponseOBJ
��0 B
MsgRes
��C I
)
��I J
{
�� 	

DACElimina
�� 
.
�� 
EliminarGlosa
�� $
(
��$ %
id
��% '
,
��' (
ref
��) ,
MsgRes
��- 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
Int32
�� 
InsertarIndicador
�� &
(
��& '
md_indicadores
��' 5
OBJIndicadores
��6 D
,
��D E
ref
��F I 
MessageResponseOBJ
��J \
MsgRes
��] c
)
��c d
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarIndicador
�� /
(
��/ 0
OBJIndicadores
��0 >
,
��> ?
ref
��@ C
MsgRes
��D J
)
��J K
;
��K L
}
�� 	
public
�� 
List
�� 
<
�� $
md_indicadores_detalle
�� *
>
��* +#
GetIndicadoresDetalle
��, A
(
��A B
Int32
��B G)
id_indicadores_medicamentos
��H c
)
��c d
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
GetIndicadoresDetalle
�� 4
(
��4 5)
id_indicadores_medicamentos
��5 P
)
��P Q
;
��Q R
}
�� 	
public
�� 
Int32
�� &
InsertarIndicadorDetalle
�� -
(
��- .$
md_indicadores_detalle
��. D

OBJDetalle
��E O
,
��O P
ref
��Q T 
MessageResponseOBJ
��U g
MsgRes
��h n
)
��n o
{
�� 	
return
�� 

DACInserta
�� 
.
�� &
InsertarIndicadorDetalle
�� 6
(
��6 7

OBJDetalle
��7 A
,
��A B
ref
��C F
MsgRes
��G M
)
��M N
;
��N O
}
�� 	
public
�� 
void
�� /
!ActualizarIndicadoresMedicamentos
�� 5
(
��5 6$
md_indicadores_detalle
��6 L
OBJIndicadoresMD
��M ]
,
��] ^
ref
��_ b 
MessageResponseOBJ
��c u
MsgRes
��v |
)
��| }
{
�� 	
DACActualiza
�� 
.
�� /
!ActualizarIndicadoresMedicamentos
�� :
(
��: ;
OBJIndicadoresMD
��; K
,
��K L
ref
��M P
MsgRes
��Q W
)
��W X
;
��X Y
}
�� 	
public
�� 
List
�� 
<
�� $
md_indicadores_detalle
�� *
>
��* +%
GetIndicadoresDetalleID
��, C
(
��C D
Int32
��D I!
id_md_ref_indicador
��J ]
,
��] ^
Int32
��_ d*
id_indicadores_medicamentos��e �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
GetIndicadoresDetalleID
�� 6
(
��6 7!
id_md_ref_indicador
��7 J
,
��J K)
id_indicadores_medicamentos
��L g
)
��g h
;
��h i
}
�� 	
public
�� 
List
�� 
<
�� .
 vw_indicador_detalle_sin_cumplir
�� 4
>
��4 5&
GetIndicadoresSinCumplir
��6 N
(
��N O
Int32
��O T)
id_indicadores_medicamentos
��U p
)
��p q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetIndicadoresSinCumplir
�� 7
(
��7 8)
id_indicadores_medicamentos
��8 S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� .
 Managment_md_Ref_indicadorResult
�� 4
>
��4 5#
DetalleRefIndicadores
��6 K
(
��K L
Int32
��L Q)
id_indicadores_medicamentos
��R m
,
��m n
Int32
��o t
opc
��u x
)
��x y
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
DetalleRefIndicadores
�� 4
(
��4 5)
id_indicadores_medicamentos
��5 P
,
��P Q
opc
��R U
)
��U V
;
��V W
}
�� 	
public
�� 
List
�� 
<
�� -
ManagmentReporIndicadorMDResult
�� 3
>
��3 4 
ReporteIndicadores
��5 G
(
��G H
Int32
��H M)
id_indicadores_medicamentos
��N i
)
��i j
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
ReporteIndicadores
�� 1
(
��1 2)
id_indicadores_medicamentos
��2 M
)
��M N
;
��N O
}
�� 	
public
�� 
void
�� %
ActualizarIndicadoresMD
�� +
(
��+ ,
md_indicadores
��, :
OBJIndicadoresMD
��; K
,
��K L
ref
��M P 
MessageResponseOBJ
��Q c
MsgRes
��d j
)
��j k
{
�� 	
DACActualiza
�� 
.
�� %
ActualizarIndicadoresMD
�� 0
(
��0 1
OBJIndicadoresMD
��1 A
,
��A B
ref
��C F
MsgRes
��G M
)
��M N
;
��N O
}
�� 	
public
�� !
vw_total_md_detalle
�� " 
Total_DetalleIndMD
��# 5
(
��5 6
Int32
��6 ;)
id_indicadores_medicamentos
��< W
)
��W X
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
Total_DetalleIndMD
�� 1
(
��1 2)
id_indicadores_medicamentos
��2 M
)
��M N
;
��N O
}
�� 	
public
�� 
List
�� 
<
�� !
vw_table_gestion_MD
�� '
>
��' (
ConsultaGestionMd
��) :
(
��: ;
)
��; <
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
ConsultaGestionMd
�� 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
List
�� 
<
�� "
md_Ref_tipo_hallazgo
�� (
>
��( )
TipoHallazgo
��* 6
(
��6 7
)
��7 8
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
TipoHallazgo
�� +
(
��+ ,
)
��, -
;
��- .
}
�� 	
public
�� 
Int32
�� &
InsertarIndicadorGestion
�� -
(
��- .$
md_indicadores_gestion
��. D

OBJGestion
��E O
,
��O P
ref
��Q T 
MessageResponseOBJ
��U g
MsgRes
��h n
)
��n o
{
�� 	
return
�� 

DACInserta
�� 
.
�� &
InsertarIndicadorGestion
�� 6
(
��6 7

OBJGestion
��7 A
,
��A B
ref
��C F
MsgRes
��G M
)
��M N
;
��N O
}
�� 	
public
�� 
void
�� +
ActualizarIndicadoresMDEstado
�� 1
(
��1 2
md_indicadores
��2 @
OBJIndicadoresMD
��A Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
{
�� 	
DACActualiza
�� 
.
�� +
ActualizarIndicadoresMDEstado
�� 6
(
��6 7
OBJIndicadoresMD
��7 G
,
��G H
ref
��I L
MsgRes
��M S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� 
md_Ref_consultas
�� $
>
��$ %
GetRefConsulta
��& 4
(
��4 5
)
��5 6
{
�� 	
return
�� 
DACComonClass
��  
.
��  !
GetRefConsulta
��! /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� *
Managment_md_ConsultasResult
�� 0
>
��0 1)
CuentaConsultasMedicamentos
��2 M
(
��M N
Int32
��N S
opc
��T W
,
��W X
DateTime
��Y a
fecha_inicial
��b o
,
��o p
DateTime
��q y
fecha_final��z �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
CuentaConsultasMedicamentos
�� :
(
��: ;
opc
��; >
,
��> ?
fecha_inicial
��@ M
,
��M N
fecha_final
��O Z
,
��Z [
ref
��\ _
MsgRes
��` f
)
��f g
;
��g h
}
�� 	
public
�� 
List
�� 
<
�� 
md_Ref_proveedor
�� $
>
��$ %!
GetMD_Ref_proveedor
��& 9
(
��9 :
)
��: ;
{
�� 	
return
�� 
DACComonClass
��  
.
��  !!
GetMD_Ref_proveedor
��! 4
(
��4 5
)
��5 6
;
��6 7
}
�� 	
public
�� 
IEnumerable
�� 
<
�� )
vw_md_Ref_indicador_datalle
�� 6
>
��6 7%
GetVwIndicadoresDetalle
��8 O
(
��O P
Int32
��P U)
id_indicadores_medicamentos
��V q
)
��q r
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
GetVwIndicadoresDetalle
�� 6
(
��6 7)
id_indicadores_medicamentos
��7 R
)
��R S
;
��S T
}
�� 	
public
�� 
List
�� 
<
�� (
md_ref_puntos_dispensacion
�� .
>
��. /
PuntosDispercion
��0 @
(
��@ A
)
��A B
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
PuntosDispercion
�� /
(
��/ 0
)
��0 1
;
��1 2
}
�� 	
public
�� 
List
�� 
<
�� )
vw_indicadores_medicamentos
�� /
>
��/ 0"
IndicadoresProvvedor
��1 E
(
��E F
String
��F L

Proveeedor
��M W
)
��W X
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
IndicadoresProvvedor
�� 3
(
��3 4

Proveeedor
��4 >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� +
vw_obligaciones_contractuales
�� 1
>
��1 2#
ObligacionesProveedor
��3 H
(
��H I
String
��I O
	Proveedor
��P Y
)
��Y Z
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
ObligacionesProveedor
�� 4
(
��4 5
	Proveedor
��5 >
)
��> ?
;
��? @
}
�� 	
public
�� 
Int32
�� "
InsertarObligaciones
�� )
(
��) *+
md_obligaciones_contractuales
��* G*
OBJObligacionesContractuales
��H d
,
��d e
ref
��f i 
MessageResponseOBJ
��j |
MsgRes��} �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� "
InsertarObligaciones
�� 2
(
��2 3*
OBJObligacionesContractuales
��3 O
,
��O P
ref
��Q T
MsgRes
��U [
)
��[ \
;
��\ ]
}
�� 	
public
�� 
List
�� 
<
�� 1
#Managment_md_Ref_obligacionesResult
�� 7
>
��7 8$
DetalleRefObligaciones
��9 O
(
��O P
Int32
��P U+
id_obligaciones_contractuales
��V s
,
��s t
Int32
��u z
opc
��{ ~
)
��~ 
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
DetalleRefObligaciones
�� 5
(
��5 6+
id_obligaciones_contractuales
��6 S
,
��S T
opc
��U X
)
��X Y
;
��Y Z
}
�� 	
public
�� 
Int32
�� )
InsertarObligacionesDetalle
�� 0
(
��0 13
%md_obligaciones_contractuales_detalle
��1 V

OBJDetalle
��W a
,
��a b
ref
��c f 
MessageResponseOBJ
��g y
MsgRes��z �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� )
InsertarObligacionesDetalle
�� 9
(
��9 :

OBJDetalle
��: D
,
��D E
ref
��F I
MsgRes
��J P
)
��P Q
;
��Q R
}
�� 	
public
�� .
 vw_total_md_obligaciones_detalle
�� /)
Total_DetalleObligacionesMD
��0 K
(
��K L
Int32
��L Q+
id_obligaciones_contractuales
��R o
)
��o p
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
Total_DetalleObligacionesMD
�� :
(
��: ;+
id_obligaciones_contractuales
��; X
)
��X Y
;
��Y Z
}
�� 	
public
�� 
void
�� &
ActualizarObligacionesMD
�� ,
(
��, -+
md_obligaciones_contractuales
��- J*
OBJObligacionesContractuales
��K g
,
��g h
ref
��i l 
MessageResponseOBJ
��m 
MsgRes��� �
)��� �
{
�� 	
DACActualiza
�� 
.
�� &
ActualizarObligacionesMD
�� 1
(
��1 2*
OBJObligacionesContractuales
��2 N
,
��N O
ref
��P S
MsgRes
��T Z
)
��Z [
;
��[ \
}
�� 	
public
�� 
void
�� -
ActualizarObligacionesDetalleMD
�� 3
(
��3 43
%md_obligaciones_contractuales_detalle
��4 Y1
#OBJObligacionesContractualesDetalle
��Z }
,
��} ~
ref�� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
DACActualiza
�� 
.
�� -
ActualizarObligacionesDetalleMD
�� 8
(
��8 91
#OBJObligacionesContractualesDetalle
��9 \
,
��\ ]
ref
��^ a
MsgRes
��b h
)
��h i
;
��i j
}
�� 	
public
�� 
List
�� 
<
�� 3
%md_obligaciones_contractuales_detalle
�� 9
>
��9 :&
GetObligacionesDetalleID
��; S
(
��S T
Int32
��T Y$
id_md_ref_obligaciones
��Z p
,
��p q
Int32
��r w,
id_obligaciones_contractuales��x �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetObligacionesDetalleID
�� 7
(
��7 8$
id_md_ref_obligaciones
��8 N
,
��N O+
id_obligaciones_contractuales
��P m
)
��m n
;
��n o
}
�� 	
public
�� 
Int32
�� ,
InsertarHerramientaTecnologica
�� 3
(
��3 4 
md_herramienta_tec
��4 F
OBJHerramienta
��G U
,
��U V
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 

DACInserta
�� 
.
�� ,
InsertarHerramientaTecnologica
�� <
(
��< =
OBJHerramienta
��= K
,
��K L
ref
��M P
MsgRes
��Q W
)
��W X
;
��X Y
}
�� 	
public
�� 
Int32
�� 
InsertarDetallet1
�� &
(
��& '
List
��' +
<
��+ ,+
md_herramienta_tec_detalle_t1
��, I
>
��I J

OBJDetalle
��K U
,
��U V
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarDetallet1
�� /
(
��/ 0

OBJDetalle
��0 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
Int32
�� 
InsertarDetallet2
�� &
(
��& '
List
��' +
<
��+ ,+
md_herramienta_tec_detalle_t2
��, I
>
��I J

OBJDetalle
��K U
,
��U V
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarDetallet2
�� /
(
��/ 0

OBJDetalle
��0 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
Int32
�� 
InsertarDetallet3
�� &
(
��& '
List
��' +
<
��+ ,+
md_herramienta_tec_detalle_t3
��, I
>
��I J

OBJDetalle
��K U
,
��U V
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarDetallet3
�� /
(
��/ 0

OBJDetalle
��0 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
Int32
�� 
InsertarDetallet4
�� &
(
��& '
List
��' +
<
��+ ,+
md_herramienta_tec_detalle_t4
��, I
>
��I J

OBJDetalle
��K U
,
��U V
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
InsertarDetallet4
�� /
(
��/ 0

OBJDetalle
��0 :
,
��: ;
ref
��< ?
MsgRes
��@ F
)
��F G
;
��G H
}
�� 	
public
�� 
List
�� 
<
�� *
vw_herramientas_tecnologicas
�� 0
>
��0 1.
 IndicadoresProvvedorHerramientas
��2 R
(
��R S
Int32
��S X

Proveeedor
��Y c
)
��c d
{
�� 	
return
�� 
DACConsulta
�� 
.
�� .
 IndicadoresProvvedorHerramientas
�� ?
(
��? @

Proveeedor
��@ J
)
��J K
;
��K L
}
�� 	
public
�� 
List
�� 
<
�� 
md_ref_tabla1
�� !
>
��! "

ref_tabla1
��# -
(
��- .
)
��. /
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 

ref_tabla1
�� )
(
��) *
)
��* +
;
��+ ,
}
�� 	
public
�� 
List
�� 
<
�� 
vw_md_crono
�� 
>
��   
ConsultaCronograma
��! 3
(
��3 4
)
��4 5
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
ConsultaCronograma
�� 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� #
vw_md_crono_auditores
�� )
>
��) *
GetUsuarioCronoId
��+ <
(
��< =
String
��= C
usuario
��D K
,
��K L
ref
��M P 
MessageResponseOBJ
��Q c
MsgRes
��d j
)
��j k
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetUsuarioCronoId
�� 0
(
��0 1
usuario
��1 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
List
�� 
<
�� (
md_ref_puntos_dispensacion
�� .
>
��. /#
GetPuntosDispensacion
��0 E
(
��E F
)
��F G
{
�� 	
return
�� 
DACComonClass
��  
.
��  !#
GetPuntosDispensacion
��! 6
(
��6 7
)
��7 8
;
��8 9
}
�� 	
public
�� 
List
�� 
<
�� #
md_ref_puntos_control
�� )
>
��) *
GetpuntoControl
��+ :
(
��: ;
)
��; <
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetpuntoControl
�� .
(
��. /
)
��/ 0
;
��0 1
}
�� 	
public
�� 
List
�� 
<
�� 4
&Managment_md_Ref_crono_auditoresResult
�� :
>
��: ;)
ConsultaListaCronoAuditores
��< W
(
��W X
int
��X [
opc1
��\ `
,
��` a
Int32
��b g
?
��g h
opc2
��i m
,
��m n
ref
��o r!
MessageResponseOBJ��s �
MsgRes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
ConsultaListaCronoAuditores
�� :
(
��: ;
opc1
��; ?
,
��? @
opc2
��A E
,
��E F
ref
��G J
MsgRes
��K Q
)
��Q R
;
��R S
}
�� 	
public
�� 
Int32
�� *
InsertarInterventoriaGeneral
�� 1
(
��1 2&
md_interventoria_general
��2 J%
OBJInterventoriaGeneral
��K b
,
��b c
ref
��d g 
MessageResponseOBJ
��h z
MsgRes��{ �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� *
InsertarInterventoriaGeneral
�� :
(
��: ;%
OBJInterventoriaGeneral
��; R
,
��R S
ref
��T W
MsgRes
��X ^
)
��^ _
;
��_ `
}
�� 	
public
�� 
List
�� 
<
�� -
Managment_md_Ref_General1Result
�� 3
>
��3 4-
DetalleRefInterventoriaGeneral1
��5 T
(
��T U
Int32
��U Z)
id_md_interventoria_general
��[ v
,
��v w
Int32
��x }
opc��~ �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� -
DetalleRefInterventoriaGeneral1
�� >
(
��> ?)
id_md_interventoria_general
��? Z
,
��Z [
opc
��\ _
)
��_ `
;
��` a
}
�� 	
public
�� 
List
�� 
<
�� -
Managment_md_Ref_General2Result
�� 3
>
��3 4-
DetalleRefInterventoriaGeneral2
��5 T
(
��T U
Int32
��U Z)
id_md_interventoria_general
��[ v
,
��v w
Int32
��x }
opc��~ �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� -
DetalleRefInterventoriaGeneral2
�� >
(
��> ?)
id_md_interventoria_general
��? Z
,
��Z [
opc
��\ _
)
��_ `
;
��` a
}
�� 	
public
�� 
List
�� 
<
�� -
Managment_md_Ref_General3Result
�� 3
>
��3 4-
DetalleRefInterventoriaGeneral3
��5 T
(
��T U
Int32
��U Z)
id_md_interventoria_general
��[ v
,
��v w
Int32
��x }
opc��~ �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� -
DetalleRefInterventoriaGeneral3
�� >
(
��> ?)
id_md_interventoria_general
��? Z
,
��Z [
opc
��\ _
)
��_ `
;
��` a
}
�� 	
public
�� 
List
�� 
<
�� -
Managment_md_Ref_General4Result
�� 3
>
��3 4-
DetalleRefInterventoriaGeneral4
��5 T
(
��T U
Int32
��U Z)
id_md_interventoria_general
��[ v
,
��v w
Int32
��x }
opc��~ �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� -
DetalleRefInterventoriaGeneral4
�� >
(
��> ?)
id_md_interventoria_general
��? Z
,
��Z [
opc
��\ _
)
��_ `
;
��` a
}
�� 	
public
�� 
Int32
�� %
InsertarGeneral1Detalle
�� ,
(
��, -/
!md_interventoria_general_detalle1
��- N
OBJDetallleG1
��O \
,
��\ ]
ref
��^ a 
MessageResponseOBJ
��b t
MsgRes
��u {
)
��{ |
{
�� 	
return
�� 

DACInserta
�� 
.
�� %
InsertarGeneral1Detalle
�� 5
(
��5 6
OBJDetallleG1
��6 C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
Int32
�� %
InsertarGeneral2Detalle
�� ,
(
��, -/
!md_interventoria_general_detalle2
��- N
OBJDetallleG2
��O \
,
��\ ]
ref
��^ a 
MessageResponseOBJ
��b t
MsgRes
��u {
)
��{ |
{
�� 	
return
�� 

DACInserta
�� 
.
�� %
InsertarGeneral2Detalle
�� 5
(
��5 6
OBJDetallleG2
��6 C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
Int32
�� %
InsertarGeneral3Detalle
�� ,
(
��, -/
!md_interventoria_general_detalle3
��- N
OBJDetallleG3
��O \
,
��\ ]
ref
��^ a 
MessageResponseOBJ
��b t
MsgRes
��u {
)
��{ |
{
�� 	
return
�� 

DACInserta
�� 
.
�� %
InsertarGeneral3Detalle
�� 5
(
��5 6
OBJDetallleG3
��6 C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
Int32
�� %
InsertarGeneral4Detalle
�� ,
(
��, -/
!md_interventoria_general_detalle4
��- N
OBJDetallleG4
��O \
,
��\ ]
ref
��^ a 
MessageResponseOBJ
��b t
MsgRes
��u {
)
��{ |
{
�� 	
return
�� 

DACInserta
�� 
.
�� %
InsertarGeneral4Detalle
�� 5
(
��5 6
OBJDetallleG4
��6 C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
List
�� 
<
�� 
md_ref_tabla2
�� !
>
��! "

ref_tabla2
��# -
(
��- .
)
��. /
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 

ref_tabla2
�� )
(
��) *
)
��* +
;
��+ ,
}
�� 	
public
�� 
List
�� 
<
�� 
md_ref_tabla3
�� !
>
��! "

ref_tabla3
��# -
(
��- .
)
��. /
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 

ref_tabla3
�� )
(
��) *
)
��* +
;
��+ ,
}
�� 	
public
�� 
List
�� 
<
�� 
md_ref_tabla4
�� !
>
��! "

ref_tabla4
��# -
(
��- .
)
��. /
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 

ref_tabla4
�� )
(
��) *
)
��* +
;
��+ ,
}
�� 	
public
�� 
List
�� 
<
�� 
vw_tabla1_categ
�� #
>
��# $

Tabla1Catg
��% /
(
��/ 0
)
��0 1
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 

Tabla1Catg
�� )
(
��) *
)
��* +
;
��+ ,
}
�� 	
public
�� 
List
�� 
<
�� 
vw_md_detalle_T1
�� $
>
��$ %
Tabla1Detalle
��& 3
(
��3 4
Int32
��4 9
id_cat
��: @
,
��@ A
Int32
��B G 
id_herramienta_tec
��H Z
)
��Z [
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
Tabla1Detalle
�� ,
(
��, -
id_cat
��- 3
,
��3 4 
id_herramienta_tec
��5 G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� 
vw_md_detalle_T2
�� $
>
��$ %
Tabla2Detalle
��& 3
(
��3 4
Int32
��4 9
id_cat
��: @
,
��@ A
Int32
��B G 
id_herramienta_tec
��H Z
)
��Z [
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
Tabla2Detalle
�� ,
(
��, -
id_cat
��- 3
,
��3 4 
id_herramienta_tec
��5 G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� 
vw_md_detalle_T3
�� $
>
��$ %
Tabla3Detalle
��& 3
(
��3 4
Int32
��4 9
id_cat
��: @
,
��@ A
Int32
��B G 
id_herramienta_tec
��H Z
)
��Z [
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
Tabla3Detalle
�� ,
(
��, -
id_cat
��- 3
,
��3 4 
id_herramienta_tec
��5 G
)
��G H
;
��H I
}
�� 	
public
�� 
List
�� 
<
�� 
vw_md_detalle_T4
�� $
>
��$ %
Tabla4Detalle
��& 3
(
��3 4
Int32
��4 9
id_cat
��: @
,
��@ A
Int32
��B G 
id_herramienta_tec
��H Z
)
��Z [
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
Tabla4Detalle
�� ,
(
��, -
id_cat
��- 3
,
��3 4 
id_herramienta_tec
��5 G
)
��G H
;
��H I
}
�� 	
public
�� 
vw_md_total_T1
�� 
	totalesT1
�� '
(
��' (
Int32
��( -
id
��. 0
)
��0 1
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
	totalesT1
�� (
(
��( )
id
��) +
)
��+ ,
;
��, -
}
�� 	
public
�� 
vw_md_total_T2
�� 
	totalesT2
�� '
(
��' (
Int32
��( -
id
��. 0
)
��0 1
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
	totalesT2
�� (
(
��( )
id
��) +
)
��+ ,
;
��, -
}
�� 	
public
�� 
vw_md_total_T3
�� 
	totalesT3
�� '
(
��' (
Int32
��( -
id
��. 0
)
��0 1
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
	totalesT3
�� (
(
��( )
id
��) +
)
��+ ,
;
��, -
}
�� 	
public
�� 
vw_md_total_T4
�� 
	totalesT4
�� '
(
��' (
Int32
��( -
id
��. 0
)
��0 1
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
	totalesT4
�� (
(
��( )
id
��) +
)
��+ ,
;
��, -
}
�� 	
public
�� 
void
�� !
ActualizarDetallet1
�� '
(
��' (+
md_herramienta_tec_detalle_t1
��( E
OBJDetalleT
��F Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
{
�� 	
DACActualiza
�� 
.
�� !
ActualizarDetallet1
�� ,
(
��, -
OBJDetalleT
��- 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
void
�� !
ActualizarDetallet2
�� '
(
��' (+
md_herramienta_tec_detalle_t2
��( E
OBJDetalleT
��F Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
{
�� 	
DACActualiza
�� 
.
�� !
ActualizarDetallet2
�� ,
(
��, -
OBJDetalleT
��- 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
void
�� !
ActualizarDetallet3
�� '
(
��' (+
md_herramienta_tec_detalle_t3
��( E
OBJDetalleT
��F Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
{
�� 	
DACActualiza
�� 
.
�� !
ActualizarDetallet3
�� ,
(
��, -
OBJDetalleT
��- 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
void
�� !
ActualizarDetallet4
�� '
(
��' (+
md_herramienta_tec_detalle_t4
��( E
OBJDetalleT
��F Q
,
��Q R
ref
��S V 
MessageResponseOBJ
��W i
MsgRes
��j p
)
��p q
{
�� 	
DACActualiza
�� 
.
�� !
ActualizarDetallet4
�� ,
(
��, -
OBJDetalleT
��- 8
,
��8 9
ref
��: =
MsgRes
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
void
��  
ActualizarGeneral1
�� &
(
��& ' 
md_herramienta_tec
��' 9
OBJDetalleT
��: E
,
��E F
ref
��G J 
MessageResponseOBJ
��K ]
MsgRes
��^ d
)
��d e
{
�� 	
DACActualiza
�� 
.
��  
ActualizarGeneral1
�� +
(
��+ ,
OBJDetalleT
��, 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
void
��  
ActualizarGeneral2
�� &
(
��& ' 
md_herramienta_tec
��' 9
OBJDetalleT
��: E
,
��E F
ref
��G J 
MessageResponseOBJ
��K ]
MsgRes
��^ d
)
��d e
{
�� 	
DACActualiza
�� 
.
��  
ActualizarGeneral2
�� +
(
��+ ,
OBJDetalleT
��, 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
void
��  
ActualizarGeneral3
�� &
(
��& ' 
md_herramienta_tec
��' 9
OBJDetalleT
��: E
,
��E F
ref
��G J 
MessageResponseOBJ
��K ]
MsgRes
��^ d
)
��d e
{
�� 	
DACActualiza
�� 
.
��  
ActualizarGeneral3
�� +
(
��+ ,
OBJDetalleT
��, 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
void
��  
ActualizarGeneral4
�� &
(
��& ' 
md_herramienta_tec
��' 9
OBJDetalleT
��: E
,
��E F
ref
��G J 
MessageResponseOBJ
��K ]
MsgRes
��^ d
)
��d e
{
�� 	
DACActualiza
�� 
.
��  
ActualizarGeneral4
�� +
(
��+ ,
OBJDetalleT
��, 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� /
!vw_total_md_interventoria_detalle
�� 01
#Total_DetalleInterventoriaGeneralMD
��1 T
(
��T U
Int32
��U Z)
id_md_interventoria_general
��[ v
)
��v w
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 1
#Total_DetalleInterventoriaGeneralMD
�� B
(
��B C)
id_md_interventoria_general
��C ^
)
��^ _
;
��_ `
}
�� 	
public
�� 
void
�� .
 ActualizarInterventoriaGeneralMD
�� 4
(
��4 5&
md_interventoria_general
��5 M%
OBJInterventoriaGeneral
��N e
,
��e f
ref
��g j 
MessageResponseOBJ
��k }
MsgRes��~ �
)��� �
{
�� 	
DACActualiza
�� 
.
�� .
 ActualizarInterventoriaGeneralMD
�� 9
(
��9 :%
OBJInterventoriaGeneral
��: Q
,
��Q R
ref
��S V
MsgRes
��W ]
)
��] ^
;
��^ _
}
�� 	
public
�� 
List
�� 
<
�� /
!md_interventoria_general_detalle1
�� 5
>
��5 6(
GetInterventoriaDetalle1ID
��7 Q
(
��Q R
Int32
��R W%
id_md_ref_inte_general1
��X o
,
��o p
Int32
��q v*
id_md_interventoria_general��w �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
GetInterventoriaDetalle1ID
�� 9
(
��9 :%
id_md_ref_inte_general1
��: Q
,
��Q R)
id_md_interventoria_general
��S n
)
��n o
;
��o p
}
�� 	
public
�� 
List
�� 
<
�� /
!md_interventoria_general_detalle2
�� 5
>
��5 6(
GetInterventoriaDetalle2ID
��7 Q
(
��Q R
Int32
��R W%
id_md_ref_inte_general2
��X o
,
��o p
Int32
��q v*
id_md_interventoria_general��w �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
GetInterventoriaDetalle2ID
�� 9
(
��9 :%
id_md_ref_inte_general2
��: Q
,
��Q R)
id_md_interventoria_general
��S n
)
��n o
;
��o p
}
�� 	
public
�� 
List
�� 
<
�� /
!md_interventoria_general_detalle3
�� 5
>
��5 6(
GetInterventoriaDetalle3ID
��7 Q
(
��Q R
Int32
��R W%
id_md_ref_inte_general3
��X o
,
��o p
Int32
��q v*
id_md_interventoria_general��w �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
GetInterventoriaDetalle3ID
�� 9
(
��9 :%
id_md_ref_inte_general3
��: Q
,
��Q R)
id_md_interventoria_general
��S n
)
��n o
;
��o p
}
�� 	
public
�� 
List
�� 
<
�� /
!md_interventoria_general_detalle4
�� 5
>
��5 6(
GetInterventoriaDetalle4ID
��7 Q
(
��Q R
Int32
��R W%
id_md_ref_inte_general4
��X o
,
��o p
Int32
��q v*
id_md_interventoria_general��w �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
GetInterventoriaDetalle4ID
�� 9
(
��9 :%
id_md_ref_inte_general4
��: Q
,
��Q R)
id_md_interventoria_general
��S n
)
��n o
;
��o p
}
�� 	
public
�� 
void
�� 6
(ActualizarInterventoriaGeneralDetalle1MD
�� <
(
��< =/
!md_interventoria_general_detalle1
��= ^
OBJDetallleG1
��_ l
,
��l m
ref
��n q!
MessageResponseOBJ��r �
MsgRes��� �
)��� �
{
�� 	
DACActualiza
�� 
.
�� 6
(ActualizarInterventoriaGeneralDetalle1MD
�� A
(
��A B
OBJDetallleG1
��B O
,
��O P
ref
��Q T
MsgRes
��U [
)
��[ \
;
��\ ]
}
�� 	
public
�� 
void
�� 6
(ActualizarInterventoriaGeneralDetalle2MD
�� <
(
��< =/
!md_interventoria_general_detalle2
��= ^
OBJDetallleG2
��_ l
,
��l m
ref
��n q!
MessageResponseOBJ��r �
MsgRes��� �
)��� �
{
�� 	
DACActualiza
�� 
.
�� 6
(ActualizarInterventoriaGeneralDetalle2MD
�� A
(
��A B
OBJDetallleG2
��B O
,
��O P
ref
��Q T
MsgRes
��U [
)
��[ \
;
��\ ]
}
�� 	
public
�� 
void
�� 6
(ActualizarInterventoriaGeneralDetalle3MD
�� <
(
��< =/
!md_interventoria_general_detalle3
��= ^
OBJDetallleG3
��_ l
,
��l m
ref
��n q!
MessageResponseOBJ��r �
MsgRes��� �
)��� �
{
�� 	
DACActualiza
�� 
.
�� 6
(ActualizarInterventoriaGeneralDetalle3MD
�� A
(
��A B
OBJDetallleG3
��B O
,
��O P
ref
��Q T
MsgRes
��U [
)
��[ \
;
��\ ]
}
�� 	
public
�� 
void
�� 6
(ActualizarInterventoriaGeneralDetalle4MD
�� <
(
��< =/
!md_interventoria_general_detalle4
��= ^
OBJDetallleG4
��_ l
,
��l m
ref
��n q!
MessageResponseOBJ��r �
MsgRes��� �
)��� �
{
�� 	
DACActualiza
�� 
.
�� 6
(ActualizarInterventoriaGeneralDetalle4MD
�� A
(
��A B
OBJDetallleG4
��B O
,
��O P
ref
��Q T
MsgRes
��U [
)
��[ \
;
��\ ]
}
�� 	
public
�� 
List
�� 
<
�� )
vw_md_interventoria_general
�� /
>
��/ 0+
InterventoriaGeneralProveedor
��1 N
(
��N O
String
��O U
	Proveedor
��V _
)
��_ `
{
�� 	
return
�� 
DACConsulta
�� 
.
�� +
InterventoriaGeneralProveedor
�� <
(
��< =
	Proveedor
��= F
)
��F G
;
��G H
}
�� 	
public
�� 
Int32
�� %
InsertarCargueCuentasMd
�� ,
(
��, -
md_cargue_cuentas
��- >
OBJCargueCuentas
��? O
,
��O P
ref
��Q T 
MessageResponseOBJ
��U g
MsgRes
��h n
)
��n o
{
�� 	
return
�� 

DACInserta
�� 
.
�� %
InsertarCargueCuentasMd
�� 5
(
��5 6
OBJCargueCuentas
��6 F
,
��F G
ref
��H K
MsgRes
��L R
)
��R S
;
��S T
}
�� 	
public
�� 
Int32
�� +
InsertarSeguimientoPendientes
�� 2
(
��2 3'
md_seguimiento_pendientes
��3 L&
OBJSeguimientoPendientes
��M e
,
��e f
ref
��g j 
MessageResponseOBJ
��k }
MsgRes��~ �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� +
InsertarSeguimientoPendientes
�� ;
(
��; <&
OBJSeguimientoPendientes
��< T
,
��T U
ref
��V Y
MsgRes
��Z `
)
��` a
;
��a b
}
�� 	
public
�� 
Int32
�� 
?
�� 2
$InsertarSeguimientoPendientesDetalle
�� :
(
��: ;/
!md_seguimiento_pendientes_detalle
��; \-
OBJSeguimientoPendientesDetalle
��] |
,
��| }
ref��~ �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� 2
$InsertarSeguimientoPendientesDetalle
�� B
(
��B C-
OBJSeguimientoPendientesDetalle
��C b
,
��b c
ref
��d g
MsgRes
��h n
)
��n o
;
��o p
}
�� 	
public
�� 
List
�� 
<
�� ;
-Managment_md_Ref_seguimiento_pendientesResult
�� A
>
��A B-
DetalleRefSeguimientoPendientes
��C b
(
��b c
Int32
��c h+
id_md_seguimiento_pendientes��i �
,��� �
Int32��� �
opc��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� -
DetalleRefSeguimientoPendientes
�� >
(
��> ?*
id_md_seguimiento_pendientes
��? [
,
��[ \
opc
��] `
)
��` a
;
��a b
}
�� 	
public
�� -
vw_total_md_seguimiento_detalle
�� .2
$Total_DetalleSeguimientoPendientesMD
��/ S
(
��S T
Int32
��T Y*
id_md_seguimiento_pendientes
��Z v
)
��v w
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 2
$Total_DetalleSeguimientoPendientesMD
�� C
(
��C D*
id_md_seguimiento_pendientes
��D `
)
��` a
;
��a b
}
�� 	
public
�� 
void
�� /
!ActualizarSeguimientoPendientesMD
�� 5
(
��5 6'
md_seguimiento_pendientes
��6 O&
OBJSeguimientoPendientes
��P h
,
��h i
ref
��j m!
MessageResponseOBJ��n �
MsgRes��� �
)��� �
{
�� 	
DACActualiza
�� 
.
�� /
!ActualizarSeguimientoPendientesMD
�� :
(
��: ;&
OBJSeguimientoPendientes
��; S
,
��S T
ref
��U X
MsgRes
��Y _
)
��_ `
;
��` a
}
�� 	
public
�� 
List
�� 
<
�� /
!md_seguimiento_pendientes_detalle
�� 5
>
��5 6/
!GetSeguimientoPendientesDetalleID
��7 X
(
��X Y
Int32
��Y ^.
 id_md_ref_seguimiento_pendientes
��_ 
,�� �
Int32��� �,
id_md_seguimiento_pendientes��� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� /
!GetSeguimientoPendientesDetalleID
�� @
(
��@ A.
 id_md_ref_seguimiento_pendientes
��A a
,
��a b*
id_md_seguimiento_pendientes
��c 
)�� �
;��� �
}
�� 	
public
�� 
void
�� 6
(ActualizarSeguimientoPendientesDetalleMD
�� <
(
��< =/
!md_seguimiento_pendientes_detalle
��= ^-
OBJSeguimientoPendientesDetalle
��_ ~
,
��~ 
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
DACActualiza
�� 
.
�� 6
(ActualizarSeguimientoPendientesDetalleMD
�� A
(
��A B-
OBJSeguimientoPendientesDetalle
��B a
,
��a b
ref
��c f
MsgRes
��g m
)
��m n
;
��n o
}
�� 	
public
�� 
List
�� 
<
�� *
vw_md_seguimiento_pendientes
�� 0
>
��0 1,
SeguimientoPendientesProveedor
��2 P
(
��P Q
String
��Q W
	Proveedor
��X a
)
��a b
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
SeguimientoPendientesProveedor
�� =
(
��= >
	Proveedor
��> G
)
��G H
;
��H I
}
�� 	
public
�� 
Int32
�� ,
InsertarConsolidadoFacturacion
�� 3
(
��3 4
List
��4 8
<
��8 9(
md_consolidado_facturacion
��9 S
>
��S T

OBJDetalle
��U _
,
��_ `
ref
��a d 
MessageResponseOBJ
��e w
MsgRes
��x ~
)
��~ 
{
�� 	
return
�� 

DACInserta
�� 
.
�� ,
InsertarConsolidadoFacturacion
�� <
(
��< =

OBJDetalle
��= G
,
��G H
ref
��I L
MsgRes
��M S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� %
vw_gestionDocumentalCad
�� +
>
��+ ,)
GestionDocumentalMedCalidad
��- H
(
��H I
Int32
��I N
id
��O Q
,
��Q R
Int32
��S X
id2
��Y \
)
��\ ]
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
GestionDocumentalMedCalidad
�� :
(
��: ;
id
��; =
,
��= >
id2
��? B
)
��B C
;
��C D
}
�� 	
public
�� 
Int32
�� (
InsertarHerramientaGestion
�� /
(
��/ 0(
md_herramienta_tec_gestion
��0 J

OBJGestion
��K U
,
��U V
ref
��W Z 
MessageResponseOBJ
��[ m
MsgRes
��n t
)
��t u
{
�� 	
return
�� 

DACInserta
�� 
.
�� (
InsertarHerramientaGestion
�� 8
(
��8 9

OBJGestion
��9 C
,
��C D
ref
��E H
MsgRes
��I O
)
��O P
;
��P Q
}
�� 	
public
�� 
List
�� 
<
�� -
vw__md_herramientaT_sin_cumplir
�� 3
>
��3 4'
GetHerramientasSincumplir
��5 N
(
��N O
Int32
��O T$
id_herramienta_tec_med
��U k
,
��k l
Int32
��m r
tabla
��s x
)
��x y
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
GetHerramientasSincumplir
�� 8
(
��8 9$
id_herramienta_tec_med
��9 O
,
��O P
tabla
��Q V
)
��V W
;
��W X
}
�� 	
public
�� 
List
�� 
<
�� 2
$ManagmentReportNotifiAuditoriaResult
�� 8
>
��8 9$
ReportNotificacionAudi
��: P
(
��P Q
Int32
��Q V
id
��W Y
)
��Y Z
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
ReportNotificacionAudi
�� 5
(
��5 6
id
��6 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
Int32
�� .
 Insertardispensacion_ambulatoria
�� 5
(
��5 6)
md_dispensacion_ambulatoria
��6 Q

OBJDetalle
��R \
,
��\ ]
ref
��^ a 
MessageResponseOBJ
��b t
MsgRes
��u {
)
��{ |
{
�� 	
return
�� 

DACInserta
�� 
.
�� .
 Insertardispensacion_ambulatoria
�� >
(
��> ?

OBJDetalle
��? I
,
��I J
ref
��K N
MsgRes
��O U
)
��U V
;
��V W
}
�� 	
public
�� 
Int32
�� /
!Insertardispensacion_Hospitalaria
�� 6
(
��6 7*
md_dispensacion_hospitalaria
��7 S

OBJDetalle
��T ^
,
��^ _
ref
��` c 
MessageResponseOBJ
��d v
MsgRes
��w }
)
��} ~
{
�� 	
return
�� 

DACInserta
�� 
.
�� /
!Insertardispensacion_Hospitalaria
�� ?
(
��? @

OBJDetalle
��@ J
,
��J K
ref
��L O
MsgRes
��P V
)
��V W
;
��W X
}
�� 	
public
�� 
Int32
�� 5
'Insertardispensacion_ambulatoriaDetalle
�� <
(
��< =1
#md_dispensacion_ambulatoria_detalle
��= `

OBJDetalle
��a k
,
��k l
ref
��m p!
MessageResponseOBJ��q �
MsgRes��� �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� 5
'Insertardispensacion_ambulatoriaDetalle
�� E
(
��E F

OBJDetalle
��F P
,
��P Q
ref
��R U
MsgRes
��V \
)
��\ ]
;
��] ^
}
�� 	
public
�� 
Int32
�� 6
(Insertardispensacion_HospitalariaDetalle
�� =
(
��= >2
$md_dispensacion_hospitalaria_detalle
��> b

OBJDetalle
��c m
,
��m n
ref
��o r!
MessageResponseOBJ��s �
MsgRes��� �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� 6
(Insertardispensacion_HospitalariaDetalle
�� F
(
��F G

OBJDetalle
��G Q
,
��Q R
ref
��S V
MsgRes
��W ]
)
��] ^
;
��^ _
}
�� 	
public
�� 
List
�� 
<
�� (
md_ref_dispens_ambulatoria
�� .
>
��. /(
RefDispersacionAmbulatoria
��0 J
(
��J K
)
��K L
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
RefDispersacionAmbulatoria
�� 9
(
��9 :
)
��: ;
;
��; <
}
�� 	
public
�� 
List
�� 
<
�� )
md_ref_dispens_hospitalaria
�� /
>
��/ 0)
RefDispersacionHospitalaria
��1 L
(
��L M
)
��M N
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
RefDispersacionHospitalaria
�� :
(
��: ;
)
��; <
;
��< =
}
�� 	
public
�� 
List
�� 
<
�� 0
"Managment_md_Ref_ambulatorioResult
�� 6
>
��6 7#
DetalleRefAmbulatorio
��8 M
(
��M N
Int32
��N S,
id_md_dispensacion_ambulatoria
��T r
)
��r s
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
DetalleRefAmbulatorio
�� 4
(
��4 5,
id_md_dispensacion_ambulatoria
��5 S
)
��S T
;
��T U
}
�� 	
public
�� 
List
�� 
<
�� 1
#Managment_md_Ref_hospitalarioResult
�� 7
>
��7 8$
DetalleRefHospitalario
��9 O
(
��O P
Int32
��P U-
id_md_dispensacion_Hospitalaria
��V u
)
��u v
{
�� 	
return
�� 
DACConsulta
�� 
.
�� $
DetalleRefHospitalario
�� 5
(
��5 6-
id_md_dispensacion_Hospitalaria
��6 U
)
��U V
;
��V W
}
�� 	
public
�� 
List
�� 
<
�� 1
#md_dispensacion_ambulatoria_detalle
�� 7
>
��7 8%
GetAmbulatorioDetalleID
��9 P
(
��P Q
Int32
��Q V+
id_md_ref_dispens_ambulatoria
��W t
,
��t u
Int32
��v {-
id_md_dispensacion_ambulatoria��| �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
GetAmbulatorioDetalleID
�� 6
(
��6 7+
id_md_ref_dispens_ambulatoria
��7 T
,
��T U,
id_md_dispensacion_ambulatoria
��V t
)
��t u
;
��u v
}
�� 	
public
�� 
List
�� 
<
�� 2
$md_dispensacion_hospitalaria_detalle
�� 8
>
��8 9&
GetHospitalarioDetalleID
��: R
(
��R S
Int32
��S X,
id_md_ref_dispens_hospitalaria
��Y w
,
��w x
Int32
��y ~.
id_md_dispensacion_hospitalaria�� �
)��� �
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
GetHospitalarioDetalleID
�� 7
(
��7 8,
id_md_ref_dispens_hospitalaria
��8 V
,
��V W-
id_md_dispensacion_hospitalaria
��X w
)
��w x
;
��x y
}
�� 	
public
�� 
void
�� /
!ActualizarDispersacionAmbulatorio
�� 5
(
��5 61
#md_dispensacion_ambulatoria_detalle
��6 Y
OBJMD
��Z _
,
��_ `
ref
��a d 
MessageResponseOBJ
��e w
MsgRes
��x ~
)
��~ 
{
�� 	
DACActualiza
�� 
.
�� /
!ActualizarDispersacionAmbulatorio
�� :
(
��: ;
OBJMD
��; @
,
��@ A
ref
��B E
MsgRes
��F L
)
��L M
;
��M N
}
�� 	
public
�� 
void
�� 3
%ActualizarDispersacionHospitalizacion
�� 9
(
��9 :2
$md_dispensacion_hospitalaria_detalle
��: ^
OBJMD
��_ d
,
��d e
ref
��f i 
MessageResponseOBJ
��j |
MsgRes��} �
)��� �
{
�� 	
DACActualiza
�� 
.
�� 3
%ActualizarDispersacionHospitalizacion
�� >
(
��> ?
OBJMD
��? D
,
��D E
ref
��F I
MsgRes
��J P
)
��P Q
;
��Q R
}
�� 	
public
�� 
List
�� 
<
�� )
vw_dispensacion_ambulatoria
�� /
>
��/ 0"
AmbulatorioProvvedor
��1 E
(
��E F
String
��F L

Proveeedor
��M W
)
��W X
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
AmbulatorioProvvedor
�� 3
(
��3 4

Proveeedor
��4 >
)
��> ?
;
��? @
}
�� 	
public
�� 
List
�� 
<
�� *
vw_dispensacion_hospitalaria
�� 0
>
��0 1#
hospitalarioProvvedor
��2 G
(
��G H
String
��H N

Proveeedor
��O Y
)
��Y Z
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
hospitalarioProvvedor
�� 4
(
��4 5

Proveeedor
��5 ?
)
��? @
;
��@ A
}
�� 	
public
�� 
void
�� %
ActualizarAmbulatoriaMD
�� +
(
��+ ,)
md_dispensacion_ambulatoria
��, G
OBJMD
��H M
,
��M N
ref
��O R 
MessageResponseOBJ
��S e
MsgRes
��f l
)
��l m
{
�� 	
DACActualiza
�� 
.
�� %
ActualizarAmbulatoriaMD
�� 0
(
��0 1
OBJMD
��1 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� 
void
�� &
ActualizarHospitalariaMD
�� ,
(
��, -*
md_dispensacion_hospitalaria
��- I
OBJMD
��J O
,
��O P
ref
��Q T 
MessageResponseOBJ
��U g
MsgRes
��h n
)
��n o
{
�� 	
DACActualiza
�� 
.
�� &
ActualizarHospitalariaMD
�� 1
(
��1 2
OBJMD
��2 7
,
��7 8
ref
��9 <
MsgRes
��= C
)
��C D
;
��D E
}
�� 	
public
�� 
md_control_gastos
��  
control_gastosMes
��! 2
(
��2 3
Int32
��3 8
mes
��9 <
,
��< =
String
��> D
año
��E H
)
��H I
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
control_gastosMes
�� 0
(
��0 1
mes
��1 4
,
��4 5
año
��6 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
Int32
�� #
Insertarcontrol_gasto
�� *
(
��* +
md_control_gastos
��+ <

OBJDetalle
��= G
,
��G H
ref
��I L 
MessageResponseOBJ
��M _
MsgRes
��` f
)
��f g
{
�� 	
return
�� 

DACInserta
�� 
.
�� #
Insertarcontrol_gasto
�� 3
(
��3 4

OBJDetalle
��4 >
,
��> ?
ref
��@ C
MsgRes
��D J
)
��J K
;
��K L
}
�� 	
public
�� 
void
�� %
ActualizarControlGastos
�� +
(
��+ ,
md_control_gastos
��, =
OBJMD
��> C
,
��C D
ref
��E H 
MessageResponseOBJ
��I [
MsgRes
��\ b
)
��b c
{
�� 	
DACActualiza
�� 
.
�� %
ActualizarControlGastos
�� 0
(
��0 1
OBJMD
��1 6
,
��6 7
ref
��8 ;
MsgRes
��< B
)
��B C
;
��C D
}
�� 	
public
�� !
vw_md_control_gasto
�� "!
control_gastosTotal
��# 6
(
��6 7
Int32
��7 <
mes
��= @
)
��@ A
{
�� 	
return
�� 
DACConsulta
�� 
.
�� !
control_gastosTotal
�� 2
(
��2 3
mes
��3 6
)
��6 7
;
��7 8
}
�� 	
public
�� 
List
�� 
<
�� 7
)Managment_md_control_gastos_tableroResult
�� =
>
��= >"
tableroControlGastos
��? S
(
��S T
int
��T W
opc
��X [
,
��[ \
int
��] `
año
��a d
)
��d e
{
�� 	
return
�� 
DACConsulta
�� 
.
�� "
tableroControlGastos
�� 3
(
��3 4
opc
��4 7
,
��7 8
año
��9 <
)
��< =
;
��= >
}
�� 	
public
�� 
List
�� 
<
�� 8
*Managment_md_control_gastos_tablero2Result
�� >
>
��> ?#
tableroControlGastos2
��@ U
(
��U V
int
��V Y
opc
��Z ]
,
��] ^
int
��_ b
año
��c f
)
��f g
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
tableroControlGastos2
�� 4
(
��4 5
opc
��5 8
,
��8 9
año
��: =
)
��= >
;
��> ?
}
�� 	
public
�� %
vw_md_total_ambulatoria
�� &&
Total_DetalleAmbulatoria
��' ?
(
��? @
Int32
��@ E,
id_md_dispensacion_ambulatoria
��F d
)
��d e
{
�� 	
return
�� 
DACConsulta
�� 
.
�� &
Total_DetalleAmbulatoria
�� 7
(
��7 8,
id_md_dispensacion_ambulatoria
��8 V
)
��V W
;
��W X
}
�� 	
public
�� &
vw_md_total_hospitalaria
�� ''
Total_DetalleHospitalaria
��( A
(
��A B
Int32
��B G-
id_md_dispensacion_hospitalaria
��H g
)
��g h
{
�� 	
return
�� 
DACConsulta
�� 
.
�� '
Total_DetalleHospitalaria
�� 8
(
��8 9-
id_md_dispensacion_hospitalaria
��9 X
)
��X Y
;
��Y Z
}
�� 	
public
�� 
int
�� "
CarguePrefacturaBase
�� '
(
��' ((
md_prefacturas_cargue_base
��( B

carguebase
��C M
,
��M N
List
��O S
<
��S T$
md_prefacturas_detalle
��T j
>
��j k
listaDetalle
��l x
)
��x y
{
�� 	
return
�� 

DACInserta
�� 
.
�� "
CarguePrefacturaBase
�� 2
(
��2 3

carguebase
��3 =
,
��= >
listaDetalle
��? K
)
��K L
;
��L M
}
�� 	
public
�� 
int
�� #
CarguePrefacturaLista
�� (
(
��( )
List
��) -
<
��- .$
md_prefacturas_detalle
��. D
>
��D E
listadoCargue
��F S
)
��S T
{
�� 	
return
�� 

DACInserta
�� 
.
�� #
CarguePrefacturaLista
�� 3
(
��3 4
listadoCargue
��4 A
)
��A B
;
��B C
}
�� 	
public
�� 
int
�� 
CargueLUPEBase
�� !
(
��! "!
md_Lupe_cargue_base
��" 5

carguebase
��6 @
,
��@ A
List
��B F
<
��F G)
md_lupe_cargue_base_detalle
��G b
>
��b c
listadoCargue
��d q
)
��q r
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
CargueLUPEBase
�� ,
(
��, -

carguebase
��- 7
,
��7 8
listadoCargue
��9 F
)
��F G
;
��G H
}
�� 	
public
�� 
int
�� )
ActualizarVigenciaHastaLupe
�� .
(
��. /!
md_Lupe_cargue_base
��/ B
obj
��C F
)
��F G
{
�� 	
return
�� 
DACActualiza
�� 
.
��  )
ActualizarVigenciaHastaLupe
��  ;
(
��; <
obj
��< ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
�� 7
)management_listadoPrefacturasCruzanResult
�� =
>
��= >,
listadoSiCruzanPrefacturasLupe
��? ]
(
��] ^
int
��^ a
idBase
��b h
)
��h i
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
listadoSiCruzanPrefacturasLupe
�� =
(
��= >
idBase
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
List
�� 
<
�� 9
+management_listadoPrefacturasNoCruzanResult
�� ?
>
��? @,
listadoNoCruzanPrefacturasLupe
��A _
(
��_ `
int
��` c
idBase
��d j
)
��j k
{
�� 	
return
�� 
DACConsulta
�� 
.
�� ,
listadoNoCruzanPrefacturasLupe
�� =
(
��= >
idBase
��> D
)
��D E
;
��E F
}
�� 	
public
�� 
List
�� 
<
�� +
management_lupe_carguesResult
�� 1
>
��1 2
listadoCargueLupe
��3 D
(
��D E
)
��E F
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
listadoCargueLupe
�� 0
(
��0 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
List
�� 
<
�� <
.management_lupe_cargues_intermediacionesResult
�� B
>
��B C/
!listadoCargueLupeIntermediaciones
��D e
(
��e f
int
��f i
idLupe
��j p
)
��p q
{
�� 	
return
�� 
DACConsulta
�� 
.
�� /
!listadoCargueLupeIntermediaciones
�� @
(
��@ A
idLupe
��A G
)
��G H
;
��H I
}
�� 	
public
�� 
int
�� 
EliminarLupe
�� 
(
��  
int
��  #
idLupe
��$ *
,
��* +
string
��, 2
usuarioElimina
��3 A
)
��A B
{
�� 	
return
�� 

DACElimina
�� 
.
�� 
EliminarLupe
�� *
(
��* +
idLupe
��+ 1
,
��1 2
usuarioElimina
��3 A
)
��A B
;
��B C
}
�� 	
public
�� 
int
�� +
EliminarMedicamentosRegulados
�� 0
(
��0 1
int
��1 4
idMed
��5 :
,
��: ;
string
��< B
usuarioElimina
��C Q
)
��Q R
{
�� 	
return
�� 

DACElimina
�� 
.
�� +
EliminarMedicamentosRegulados
�� ;
(
��; <
idMed
��< A
,
��A B
usuarioElimina
��C Q
)
��Q R
;
��R S
}
�� 	
public
�� !
md_Lupe_cargue_base
�� "
UltimoCargueLupe
��# 3
(
��3 4
int
��4 7
?
��7 8
idPrestador
��9 D
)
��D E
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
UltimoCargueLupe
�� /
(
��/ 0
idPrestador
��0 ;
)
��; <
;
��< =
}
�� 	
public
�� 
int
�� 
CargueLUPELista
�� "
(
��" #
List
��# '
<
��' ()
md_lupe_cargue_base_detalle
��( C
>
��C D
listadoCargue
��E R
,
��R S
int
��T W
id_cargueBase
��X e
)
��e f
{
�� 	
return
�� 

DACInserta
�� 
.
�� 
CargueLUPELista
�� -
(
��- .
listadoCargue
��. ;
,
��; <
id_cargueBase
��= J
)
��J K
;
��K L
}
�� 	
public
�� 
int
�� )
CargueLUPEListaReaprobacion
�� .
(
��. /
List
��/ 3
<
��3 46
(md_lupe_cargue_base_detalle_reaprobacion
��4 \
>
��\ ]
listadoCargue
��^ k
,
��k l
int
��m p
idCargue
��q y
,
��y z
int
��{ ~
idPrefactura�� �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� )
CargueLUPEListaReaprobacion
�� 9
(
��9 :
listadoCargue
��: G
,
��G H
idCargue
��I Q
,
��Q R
idPrefactura
��S _
)
��_ `
;
��` a
}
�� 	
public
�� 
int
�� ,
InsertarReparobacioPrefacturas
�� 1
(
��1 2
List
��2 6
<
��6 7/
!md_prefacturas_reaprobacionMasiva
��7 X
>
��X Y
listadoCargue
��Z g
,
��g h
int
��i l
idPrefacturaBase
��m }
)
��} ~
{
�� 	
return
�� 

DACInserta
�� 
.
�� ,
InsertarReparobacioPrefacturas
�� <
(
��< =
listadoCargue
��= J
,
��J K
idPrefacturaBase
��L \
)
��\ ]
;
��] ^
}
�� 	
public
�� 
int
�� .
 InsertarDesaparobacioPrefacturas
�� 3
(
��3 4
List
��4 8
<
��8 90
"md_prefacturas_desaprobacionMasiva
��9 [
>
��[ \
listadoCargue
��] j
,
��j k
int
��l o
idPrefacturaBase��p �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� .
 InsertarDesaparobacioPrefacturas
�� >
(
��> ?
listadoCargue
��? L
,
��L M
idPrefacturaBase
��N ^
)
��^ _
;
��_ `
}
�� 	
public
�� 
int
�� -
InsertarCierrePrefacturasMasivo
�� 2
(
��2 3
List
��3 7
<
��7 8)
md_prefacturas_cierreMasivo
��8 S
>
��S T
listadoCargue
��U b
,
��b c
int
��d g
idPrefacturaBase
��h x
)
��x y
{
�� 	
return
�� 

DACInserta
�� 
.
�� -
InsertarCierrePrefacturasMasivo
�� =
(
��= >
listadoCargue
��> K
,
��K L
idPrefacturaBase
��M ]
)
��] ^
;
��^ _
}
�� 	
public
�� 
List
�� 
<
�� A
3management_prefacturas_notificacionOPLNoPasanResult
�� G
>
��G H5
'ListaDatoaReportePrefacturasaOPLNoPasan
��I p
(
��p q
int
��q t
?
��t u
idCargue
��v ~
)
��~ 
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 5
'ListaDatoaReportePrefacturasaOPLNoPasan
�� F
(
��F G
idCargue
��G O
)
��O P
;
��P Q
}
�� 	
public
�� 
List
�� 
<
�� ?
1management_prefacturas_notificacionOPLPasanResult
�� E
>
��E F3
%ListaDatoaReportePrefacturasaOPLPasan
��G l
(
��l m
int
��m p
?
��p q
idCargue
��r z
)
��z {
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 3
%ListaDatoaReportePrefacturasaOPLPasan
�� D
(
��D E
idCargue
��E M
)
��M N
;
��N O
}
�� 	
public
�� 
List
�� 
<
�� E
7management_prefacturas_listaMedicamentosReguladosResult
�� K
>
��K L(
ListaMedicamentosRegulados
��M g
(
��g h
)
��h i
{
�� 	
return
�� 
DACConsulta
�� 
.
�� (
ListaMedicamentosRegulados
�� 9
(
��9 :
)
��: ;
;
��; <
}
�� 	
public
�� 
int
�� *
CargueLupeIntermediacionBase
�� /
(
��/ 0$
md_lupe_intermediacion
��0 F
obj
��G J
,
��J K
int
��L O
idCargueBase
��P \
)
��\ ]
{
�� 	
return
�� 

DACInserta
�� 
.
�� *
CargueLupeIntermediacionBase
�� :
(
��: ;
obj
��; >
,
��> ?
idCargueBase
��@ L
)
��L M
;
��M N
}
�� 	
public
�� 
int
�� +
CargueLupeIntermediacionLista
�� 0
(
��0 1
List
��1 5
<
��5 6)
md_lupe_intermediacion_dtll
��6 Q
>
��Q R
listadoCargue
��S `
)
��` a
{
�� 	
return
�� 

DACInserta
�� 
.
�� +
CargueLupeIntermediacionLista
�� ;
(
��; <
listadoCargue
��< I
)
��I J
;
��J K
}
�� 	
public
�� 
void
�� 

CargueLupe
�� 
(
�� !
md_Lupe_cargue_base
�� 2

carguebase
��3 =
,
��= >
List
��? C
<
��C D)
md_lupe_cargue_base_detalle
��D _
>
��_ `
carguedetalle
��a n
,
��n o
List
��p t
<
��t u*
md_lupe_intermediacion_dtll��u �
>��� � 
Intermediaciones��� �
,��� �
ref��� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	

DACInserta
�� 
.
�� 

CargueLupe
�� !
(
��! "

carguebase
��" ,
,
��, -
carguedetalle
��. ;
,
��; <
Intermediaciones
��= M
,
��M N
ref
��O R
MsgRes
��S Y
)
��Y Z
;
��Z [
}
�� 	
public
�� 
int
�� )
CargueMedicamentosRegulados
�� .
(
��. /'
md_medicamentos_regulados
��/ H
obj
��I L
,
��L M
List
��N R
<
��R S/
!md_medicamentos_regulados_detalle
��S t
>
��t u
detalle
��v }
,
��} ~
ref�� �"
MessageResponseOBJ��� �
MsgRes��� �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� )
CargueMedicamentosRegulados
�� 9
(
��9 :
obj
��: =
,
��= >
detalle
��? F
,
��F G
ref
��H K
MsgRes
��L R
)
��R S
;
��S T
}
�� 	
public
�� 
int
�� )
CargueMedicamentosDatosOPLS
�� .
(
��. /"
md_medicamentos_OPLS
��/ C
obj
��D G
,
��G H
List
��I M
<
��M N*
md_medicamentos_OPLS_detalle
��N j
>
��j k
detalle
��l s
,
��s t
ref
��u x!
MessageResponseOBJ��y �
MsgRes��� �
)��� �
{
�� 	
return
�� 

DACInserta
�� 
.
�� )
CargueMedicamentosDatosOPLS
�� 9
(
��9 :
obj
��: =
,
��= >
detalle
��? F
,
��F G
ref
��H K
MsgRes
��L R
)
��R S
;
��S T
}
�� 	
public
�� 
List
�� 
<
�� (
md_prefacturas_cargue_base
�� .
>
��. / 
GetPrefacturasList
��0 B
(
��B C
)
��C D
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
GetPrefacturasList
�� 1
(
��1 2
)
��2 3
;
��3 4
}
�� 	
public
�� 
List
�� 
<
�� 3
%management_validadorPrefacturasResult
�� 9
>
��9 :#
GetPrefacturasListado
��; P
(
��P Q
int
��Q T
?
��T U
rol
��V Y
,
��Y Z
string
��[ a
usuario
��b i
)
��i j
{
�� 	
return
�� 
DACConsulta
�� 
.
�� #
GetPrefacturasListado
�� 4
(
��4 5
rol
��5 8
,
��8 9
usuario
��: A
)
��A B
;
��B C
}
�� 	
public
�� 
List
�� 
<
�� 9
+management_validadorCarguePrefacturasResult
�� ?
>
��? @)
GetPrefacturasListadoCargue
��A \
(
��\ ]
int
��] `
?
��` a
rol
��b e
,
��e f
string
��g m
usuario
��n u
)
��u v
{
�� 	
return
�� 
DACConsulta
�� 
.
�� )
GetPrefacturasListadoCargue
�� :
(
��: ;
rol
��; >
,
��> ?
usuario
��@ G
)
��G H
;
��H I
}
�� 	
public
�� 5
'management_prefacturasDatosCargueResult
�� 6%
DatosPrefacturaIdCargue
��7 N
(
��N O
int
��O R
idCargue
��S [
)
��[ \
{
�� 	
return
�� 
DACConsulta
�� 
.
�� %
DatosPrefacturaIdCargue
�� 6
(
��6 7
idCargue
��7 ?
)
��? @
;
��@ A
}
�� 	
public
�� 
List
�� 
<
�� <
.management_consolidadoInicialPrefacturasResult
�� B
>
��B C5
'GetPrefacturasListadoConsolidadoInicial
��D k
(
��k l
int
��l o
?
��o p
	idUsuario
��q z
)
��z {
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 5
'GetPrefacturasListadoConsolidadoInicial
�� F
(
��F G
	idUsuario
��G P
)
��P Q
;
��Q R
}
�� 	
public
�� 
int
�� 0
"CargarLoteMedicamentosDispensacion
�� 5
(
��5 6&
medicamentos_dispen_lote
��6 N
obj
��O R
)
��R S
{
�� 	
return
�� 

DACInserta
�� 
.
�� 0
"CargarLoteMedicamentosDispensacion
�� @
(
��@ A
obj
��A D
)
��D E
;
��E F
}
�� 	
public
�� 
int
�� 2
$eliminarLoteMedicamentosDispensacion
�� 7
(
��7 8
int
��8 ;
lote
��< @
)
��@ A
{
�� 	
return
�� 

DACElimina
�� 
.
�� 2
$eliminarLoteMedicamentosDispensacion
�� B
(
��B C
lote
��C G
)
��G H
;
��H I
}
�� 	
public
�� (
md_prefacturas_cargue_base
�� )
GetPrefacturas
��* 8
(
��8 9
int
��9 <
id
��= ?
)
��? @
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetPrefacturas
�� -
(
��- .
id
��. 0
)
��0 1
;
��1 2
}
�� 	
public
�� .
 log_prefacturas_estadoValidacion
�� //
!GetLogEstadoValidacionPrefacturas
��0 Q
(
��Q R
int
��R U
?
��U V
id
��W Y
)
��Y Z
{
�� 	
return
�� 
DACConsulta
�� 
.
�� /
!GetLogEstadoValidacionPrefacturas
�� @
(
��@ A
id
��A C
)
��C D
;
��D E
}
�� 	
public
�� 
List
�� 
<
�� .
 log_prefacturas_estadoValidacion
�� 4
>
��4 54
&GetLogEstadoValidacionPrefacturasFases
��6 \
(
��\ ]
int
��] `
?
��` a
id
��b d
,
��d e
int
��f i
?
��i j
fase
��k o
)
��o p
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 4
&GetLogEstadoValidacionPrefacturasFases
�� E
(
��E F
id
��F H
,
��H I
fase
��J N
)
��N O
;
��O P
}
�� 	
public
�� 2
$log_control_validaciones_prefacturas
�� 3
GetLogPrefacturas
��4 E
(
��E F
int
��F I
?
��I J
idCargue
��K S
)
��S T
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetLogPrefacturas
�� 0
(
��0 1
idCargue
��1 9
)
��9 :
;
��: ;
}
�� 	
public
�� 
int
�� '
ActualizarFasePrefacturas
�� ,
(
��, -
int
��- 0
?
��0 1

cargueBase
��2 <
,
��< =
int
��> A
?
��A B
fase
��C G
)
��G H
{
�� 	
return
�� 
DACActualiza
�� 
.
��  '
ActualizarFasePrefacturas
��  9
(
��9 :

cargueBase
��: D
,
��D E
fase
��F J
)
��J K
;
��K L
}
�� 	
public
�� 
int
�� /
!ActualizarEnValidacionPrefacturas
�� 4
(
��4 5
int
��5 8
?
��8 9

cargueBase
��: D
,
��D E
int
��F I
?
��I J
estado
��K Q
)
��Q R
{
�� 	
return
�� 
DACActualiza
�� 
.
��  /
!ActualizarEnValidacionPrefacturas
��  A
(
��A B

cargueBase
��B L
,
��L M
estado
��N T
)
��T U
;
��U V
}
�� 	
public
�� 
int
�� (
LogErroresFasesPrefacturas
�� -
(
��- .(
log_prefacturas_errorFases
��. H
obj
��I L
)
��L M
{
�� 	
return
�� 

DACInserta
�� 
.
�� (
LogErroresFasesPrefacturas
�� 8
(
��8 9
obj
��9 <
)
��< =
;
��= >
}
�� 	
public
�� 
List
�� 
<
�� $
md_prefacturas_detalle
�� *
>
��* + 
GetPrefacturasById
��, >
(
��> ?
string
��? E
numeroPrefactura
��F V
)
��V W
{
�� 	
return
�� 
DACConsulta
�� 
.
��  
GetPrefacturasById
�� 1
(
��1 2
numeroPrefactura
��2 B
)
��B C
;
��C D
}
�� 	
public
�� $
md_prefacturas_detalle
�� %
GetPrefacturasID
��& 6
(
��6 7
int
��7 :
?
��: ;
id_detprefactura
��< L
)
��L M
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetPrefacturasID
�� /
(
��/ 0
id_detprefactura
��0 @
)
��@ A
;
��A B
}
�� 	
public
�� 
List
�� 
<
�� /
!ManagmentReportePrefacturasResult
�� 5
>
��5 6
GetRptPrefacturas
��7 H
(
��H I
int
��I L
idcargue
��M U
)
��U V
{
�� 	
return
�� 
DACConsulta
�� 
.
�� 
GetRptPrefacturas
�� 0
(
��0 1
idcargue
��1 9
)
��9 :
;
��: ;
}
�� 	
public
� �  
void
� �  "
ActualizarPrefactura
� �  (
(
� � ( )$
md_prefacturas_detalle
� � ) ?
obj
� � @ C
)
� � C D
{
� �  	
DACActualiza
� �  
.
� �  "
ActualizarPrefactura
� �  -
(
� � - .
obj
� � . 1
)
� � 1 2
;
� � 2 3
}
� �  	
public
� �  
int
� �  '
ActualizarPrefacturaLista
� �  ,
(
� � , -
List
� � - 1
<
� � 1 2
int
� � 2 5
>
� � 5 6
ListaIds
� � 7 ?
,
� � ? @
string
� � A G
observaciones
� � H U
,
� � U V
double
� � W ]
nuevo_valor
� � ^ i
)
� � i j
{
� �  	
return
� �  
DACActualiza
� �  
.
� �   '
ActualizarPrefacturaLista
� �   9
(
� � 9 :
ListaIds
� � : B
,
� � B C
observaciones
� � D Q
,
� � Q R
nuevo_valor
� � S ^
)
� � ^ _
;
� � _ `
}
� �  	
public
� �  
int
� �  #
DesaprobarPrefacturas
� �  (
(
� � ( )
List
� � ) -
<
� � - .
int
� � . 1
>
� � 1 2
ListaIds
� � 3 ;
,
� � ; <
string
� � = C&
observacionDesaprobacion
� � D \
)
� � \ ]
{
� �  	
return
� �  
DACActualiza
� �  
.
� �   #
DesaprobarPrefacturas
� �   5
(
� � 5 6
ListaIds
� � 6 >
,
� � > ?&
observacionDesaprobacion
� � @ X
)
� � X Y
;
� � Y Z
}
� �  	
public
� �  
int
� �  0
"guardarLogDesaprobacionPrefacturas
� �  5
(
� � 5 6
List
� � 6 :
<
� � : ;+
log_prefacturas_desaprobacion
� � ; X
>
� � X Y
lista
� � Z _
)
� � _ `
{
� �  	
return
� �  

DACInserta
� �  
.
� �  0
"guardarLogDesaprobacionPrefacturas
� �  @
(
� � @ A
lista
� � A F
)
� � F G
;
� � G H
}
� �  	
public
� �  
int
� �  -
guardarLogAprobacionPrefacturas
� �  2
(
� � 2 3
List
� � 3 7
<
� � 7 8(
log_prefacturas_aprobacion
� � 8 R
>
� � R S
lista
� � T Y
)
� � Y Z
{
� �  	
return
� �  

DACInserta
� �  
.
� �  -
guardarLogAprobacionPrefacturas
� �  =
(
� � = >
lista
� � > C
)
� � C D
;
� � D E
}
� �  	
public
� �  
int
� �  (
GuardarLogAprobacionMasiva
� �  -
(
� � - ..
 log_prefacturas_aprobacionMasiva
� � . N
obj
� � O R
)
� � R S
{
� �  	
return
� �  

DACInserta
� �  
.
� �  (
GuardarLogAprobacionMasiva
� �  8
(
� � 8 9
obj
� � 9 <
)
� � < =
;
� � = >
}
� �  	
public
� �  
int
� �  7
)GuardarLogControl_validacionesPrefacturas
� �  <
(
� � < =2
$log_control_validaciones_prefacturas
� � = a
obj
� � b e
)
� � e f
{
� �  	
return
� �  

DACInserta
� �  
.
� �  7
)GuardarLogControl_validacionesPrefacturas
� �  G
(
� � G H
obj
� � H K
)
� � K L
;
� � L M
}
� �  	
public
� �  
int
� �  +
GuardarLogDesaprobacionMasiva
� �  0
(
� � 0 11
#log_prefacturas_desaprobacionMasiva
� � 1 T
obj
� � U X
)
� � X Y
{
� �  	
return
� �  

DACInserta
� �  
.
� �  +
GuardarLogDesaprobacionMasiva
� �  ;
(
� � ; <
obj
� � < ?
)
� � ? @
;
� � @ A
}
� �  	
public
� �  
int
� �  ,
TraerUltimoCargueLogAprobacion
� �  1
(
� � 1 2
)
� � 2 3
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  ,
TraerUltimoCargueLogAprobacion
� �  =
(
� � = >
)
� � > ?
;
� � ? @
}
� �  	
public
� �  
int
� �  /
!TraerUltimoCargueLogDesaprobacion
� �  4
(
� � 4 5
)
� � 5 6
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  /
!TraerUltimoCargueLogDesaprobacion
� �  @
(
� � @ A
)
� � A B
;
� � B C
}
� �  	
public
� �  
int
� �  -
GuardarLogDatosAprobacionMasiva
� �  2
(
� � 2 3
int
� � 3 6
?
� � 6 7
idCargue
� � 8 @
,
� � @ A
int
� � B E
?
� � E F
idLog
� � G L
,
� � L M
string
� � N T
usuarioDigita
� � U b
)
� � b c
{
� �  	
return
� �  

DACInserta
� �  
.
� �  -
GuardarLogDatosAprobacionMasiva
� �  =
(
� � = >
idCargue
� � > F
,
� � F G
idLog
� � H M
,
� � M N
usuarioDigita
� � O \
)
� � \ ]
;
� � ] ^
}
� �  	
public
� �  
int
� �  0
"GuardarLogDatosDesaprobacionMasiva
� �  5
(
� � 5 6
int
� � 6 9
?
� � 9 :
idCargue
� � ; C
,
� � C D
int
� � E H
?
� � H I
idLog
� � J O
,
� � O P
string
� � Q W
usuarioDigita
� � X e
)
� � e f
{
� �  	
return
� �  

DACInserta
� �  
.
� �  0
"GuardarLogDatosDesaprobacionMasiva
� �  @
(
� � @ A
idCargue
� � A I
,
� � I J
idLog
� � K P
,
� � P Q
usuarioDigita
� � R _
)
� � _ `
;
� � ` a
}
� �  	
public
� �  
void
� �  ,
ActualizarPrefacturaListaTotal
� �  2
(
� � 2 3
int
� � 3 6
idCargue
� � 7 ?
,
� � ? @
string
� � A G
observaciones
� � H U
,
� � U V
double
� � W ]
nuevo_valor
� � ^ i
)
� � i j
{
� �  	
DACActualiza
� �  
.
� �  ,
ActualizarPrefacturaListaTotal
� �  7
(
� � 7 8
idCargue
� � 8 @
,
� � @ A
observaciones
� � B O
,
� � O P
nuevo_valor
� � Q \
)
� � \ ]
;
� � ] ^
}
� �  	
public
� �  
void
� �  0
"InsertarCargueMasivoDispensaciones
� �  6
(
� � 6 7&
dispensacion_cargue_base
� � 7 O
obj
� � P S
,
� � S T
List
� � U Y
<
� � Y Z+
dispensacion_cargue_base_dtll
� � Z w
>
� � w x
lista
� � y ~
,
� � ~ 
ref� � � �"
MessageResponseOBJ� � � �
MsgRes� � � �
)� � � �
{
� �  	

DACInserta
� �  
.
� �  0
"InsertarCargueMasivoDispensaciones
� �  9
(
� � 9 :
obj
� � : =
,
� � = >
lista
� � ? D
,
� � D E
ref
� � F I
MsgRes
� � J P
)
� � P Q
;
� � Q R
}
� �  	
public
� �  
List
� �  
<
� �  /
!ManagmentocargueconsolidadoResult
� �  5
>
� � 5 6!
CuentaConsolidadoMD
� � 7 J
(
� � J K
String
� � K Q
numero_factura
� � R `
,
� � ` a
String
� � b h
numero_formula
� � i w
,
� � w x
DateTime� � y �
fecha_inicial� � � �
,� � � �
DateTime� � � �
fecha_final� � � �
,� � � �
ref� � � �"
MessageResponseOBJ� � � �
MsgRes� � � �
)� � � �
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  !
CuentaConsolidadoMD
� �  2
(
� � 2 3
numero_factura
� � 3 A
,
� � A B
numero_formula
� � C Q
,
� � Q R
fecha_inicial
� � S `
,
� � ` a
fecha_final
� � b m
,
� � m n
ref
� � o r
MsgRes
� � s y
)
� � y z
;
� � z {
}
� �  	
public
� �  
Int32
� �  +
InsertarFFMMGlosaConciliacion
� �  2
(
� � 2 3#
md_glosa_conciliacion
� � 3 H
OBJ
� � I L
,
� � L M
ref
� � N Q 
MessageResponseOBJ
� � R d
MsgRes
� � e k
)
� � k l
{
� �  	
return
� �  

DACInserta
� �  
.
� �  +
InsertarFFMMGlosaConciliacion
� �  ;
(
� � ; <
OBJ
� � < ?
,
� � ? @
ref
� � A D
MsgRes
� � E K
)
� � K L
;
� � L M
}
� �  	
public
� �  &
vw_md_glosa_conciliacion
� �  '!
ConsultaGlosaDtllId
� � ( ;
(
� � ; <
Int32
� � < A!
id_md_glosa_detalle
� � B U
)
� � U V
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  !
ConsultaGlosaDtllId
� �  2
(
� � 2 3!
id_md_glosa_detalle
� � 3 F
)
� � F G
;
� � G H
}
� �  	
public
� �  
int
� �  ,
GuardarMedicamentosFacturacion
� �  1
(
� � 1 2&
medicamentos_facturacion
� � 2 J
Obj
� � K N
,
� � N O
List
� � P T
<
� � T U+
medicamentos_facturacion_dtll
� � U r
>
� � r s
Result
� � t z
,
� � z {
ref
� � | "
MessageResponseOBJ� � � �
MsgRes� � � �
)� � � �
{
� �  	
return
� �  

DACInserta
� �  
.
� �  ,
GuardarMedicamentosFacturacion
� �  <
(
� � < =
Obj
� � = @
,
� � @ A
Result
� � B H
,
� � H I
ref
� � J M
MsgRes
� � N T
)
� � T U
;
� � U V
}
� �  	
public
� �  
List
� �  
<
� �  5
'ManagementMedicamentosFacturacionResult
� �  ;
>
� � ; <"
GetListMdFacturacion
� � = Q
(
� � Q R
)
� � R S
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  "
GetListMdFacturacion
� �  3
(
� � 3 4
)
� � 4 5
;
� � 5 6
}
� �  	
public
� �  
List
� �  
<
� �  8
*managemente_medicamentos_facturacionResult
� �  >
>
� � > ?)
Getmedicamentos_facturacion
� � @ [
(
� � [ \
int
� � \ _
mes
� � ` c
,
� � c d
int
� � e h
año
� � i l
,
� � l m
int
� � n q
regional
� � r z
)
� � z {
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  )
Getmedicamentos_facturacion
� �  :
(
� � : ;
mes
� � ; >
,
� � > ?
año
� � @ C
,
� � C D
regional
� � E M
)
� � M N
;
� � N O
}
� �  	
public
� �  
List
� �  
<
� �  5
'ManagementMedicamentosFacturacionResult
� �  ;
>
� � ; <,
GetMedicamentosFacturacionList
� � = [
(
� � [ \
int
� � \ _
?
� � _ `
	mesinicio
� � a j
,
� � j k
int
� � l o
?
� � o p

añoinicio
� � q z
,
� � z {
int
� � | 
?� �  �
mesfinal� � � �
,� � � �
int� � � �
?� � � �
añofin� � � �
,� � � �
string� � � �
	Prestador� � � �
,� � � �
string� � � �
regional� � � �
)� � � �
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  ,
GetMedicamentosFacturacionList
� �  =
(
� � = >
	mesinicio
� � > G
,
� � G H

añoinicio
� � I R
,
� � R S
mesfinal
� � T \
,
� � \ ]
añofin
� � ^ d
,
� � d e
	Prestador
� � f o
,
� � o p
regional
� � q y
)
� � y z
;
� � z {
}
� �  	
public
� �  
List
� �  
<
� �  6
(Managment_ReportePrefacturas_totalResult
� �  <
>
� � < =!
GetPrefacturasTotal
� � > Q
(
� � Q R
)
� � R S
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  !
GetPrefacturasTotal
� �  2
(
� � 2 3
)
� � 3 4
;
� � 4 5
}
� �  	
public
� �  
List
� �  
<
� �  @
2Managment_ReportePrefacturas_cerrar_abiertasResult
� �  F
>
� � F G*
GetPrefacturasCerrarAbiertas
� � H d
(
� � d e
)
� � e f
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  *
GetPrefacturasCerrarAbiertas
� �  ;
(
� � ; <
)
� � < =
;
� � = >
}
� �  	
public
� �  
List
� �  
<
� �  ?
1management_prefacturas_consolidado_abiertasResult
� �  E
>
� � E F$
GetPrefacturasAbiertas
� � G ]
(
� � ] ^
int
� � ^ a
?
� � a b

cargueBase
� � c m
)
� � m n
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  $
GetPrefacturasAbiertas
� �  5
(
� � 5 6

cargueBase
� � 6 @
)
� � @ A
;
� � A B
}
� �  	
public
� �  
List
� �  
<
� �  ?
1management_prefacturas_consolidado_cerradasResult
� �  E
>
� � E F$
GetPrefacturasCerradas
� � G ]
(
� � ] ^
int
� � ^ a
?
� � a b

cargueBase
� � c m
)
� � m n
{
� �  	
return
� �  
DACConsulta
� �  
.
� �  $
GetPrefacturasCerradas
� �  5
(
� � 5 6

cargueBase
� � 6 @
)
� � @ A
;
� � A B
}
� �  	
public
� �  
List
� �  
<
� �  @
2Managment_ReportePrefacturas_cerrar_cerradasResult
� �  F
>
� � F G*
GetPrefacturasCerrarCerradas
� � H d
(
� � d e
)
� � e f
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! *
GetPrefacturasCerrarCerradas
�!�! ;
(
�!�!; <
)
�!�!< =
;
�!�!= >
}
�!�! 	
public
�!�! 
int
�!�! (
ActualizarPrefacturaCerrar
�!�! -
(
�!�!- .$
md_prefacturas_detalle
�!�!. D
obj
�!�!E H
)
�!�!H I
{
�!�! 	
return
�!�! 
DACActualiza
�!�! 
.
�!�!  (
ActualizarPrefacturaCerrar
�!�!  :
(
�!�!: ;
obj
�!�!; >
)
�!�!> ?
;
�!�!? @
}
�!�! 	
public
�!�! 
int
�!�! &
GuardarPrefacturaCerrada
�!�! +
(
�!�!+ ,,
md_prefacturas_cargue_cerradas
�!�!, J
obj
�!�!K N
)
�!�!N O
{
�!�! 	
return
�!�! 

DACInserta
�!�! 
.
�!�! &
GuardarPrefacturaCerrada
�!�! 6
(
�!�!6 7
obj
�!�!7 :
)
�!�!: ;
;
�!�!; <
}
�!�! 	
public
�!�! 
void
�!�! &
EliminarCarguePrefactura
�!�! ,
(
�!�!, -
int
�!�!- 0
idCargue
�!�!1 9
,
�!�!9 :
ref
�!�!; > 
MessageResponseOBJ
�!�!? Q
MsgRes
�!�!R X
)
�!�!X Y
{
�!�! 	

DACElimina
�!�! 
.
�!�! &
EliminarCarguePrefactura
�!�! /
(
�!�!/ 0
idCargue
�!�!0 8
,
�!�!8 9
ref
�!�!: =
MsgRes
�!�!> D
)
�!�!D E
;
�!�!E F
}
�!�! 	
public
�!�! 
void
�!�!  
EliminarCargueLUPE
�!�! &
(
�!�!& '
int
�!�!' *
idCargue
�!�!+ 3
,
�!�!3 4
ref
�!�!5 8 
MessageResponseOBJ
�!�!9 K
MsgRes
�!�!L R
)
�!�!R S
{
�!�! 	

DACElimina
�!�! 
.
�!�!  
EliminarCargueLUPE
�!�! )
(
�!�!) *
idCargue
�!�!* 2
,
�!�!2 3
ref
�!�!4 7
MsgRes
�!�!8 >
)
�!�!> ?
;
�!�!? @
}
�!�! 	
public
�!�! 
void
�!�! +
EliminarMedicamentosRegulados
�!�! 1
(
�!�!1 2
int
�!�!2 5
idCargue
�!�!6 >
,
�!�!> ?
ref
�!�!@ C 
MessageResponseOBJ
�!�!D V
MsgRes
�!�!W ]
)
�!�!] ^
{
�!�! 	

DACElimina
�!�! 
.
�!�! +
EliminarMedicamentosRegulados
�!�! 4
(
�!�!4 5
idCargue
�!�!5 =
,
�!�!= >
ref
�!�!? B
MsgRes
�!�!C I
)
�!�!I J
;
�!�!J K
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�! (
md_prefacturas_cargue_base
�!�! .
>
�!�!. /*
ConsultaExistenciaPrefactura
�!�!0 L
(
�!�!L M
int
�!�!M P
regional
�!�!Q Y
,
�!�!Y Z
string
�!�![ a
numPrefactura
�!�!b o
,
�!�!o p
int
�!�!q t
idPrestador�!�!u �
)�!�!� �
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! *
ConsultaExistenciaPrefactura
�!�! ;
(
�!�!; <
regional
�!�!< D
,
�!�!D E
numPrefactura
�!�!F S
,
�!�!S T
idPrestador
�!�!U `
)
�!�!` a
;
�!�!a b
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�! !
ref_referencia_pago
�!�! '
>
�!�!' (#
GetReferenciaPagoList
�!�!) >
(
�!�!> ?
)
�!�!? @
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! #
GetReferenciaPagoList
�!�! 4
(
�!�!4 5
)
�!�!5 6
;
�!�!6 7
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�!  
indicador_regional
�!�! &
>
�!�!& ',
ConsultarIndicadorRegionalList
�!�!( F
(
�!�!F G
ref
�!�!G J 
MessageResponseOBJ
�!�!K ]
MsgRes
�!�!^ d
)
�!�!d e
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! ,
ConsultarIndicadorRegionalList
�!�! =
(
�!�!= >
ref
�!�!> A
MsgRes
�!�!B H
)
�!�!H I
;
�!�!I J
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�! 

vw_visitas
�!�! 
>
�!�! '
ConsultaCronogramaVisitas
�!�!  9
(
�!�!9 :
Int32
�!�!: ?
?
�!�!? @
idcronograma
�!�!A M
,
�!�!M N
ref
�!�!O R 
MessageResponseOBJ
�!�!S e
MsgRta
�!�!f l
)
�!�!l m
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! '
ConsultaCronogramaVisitas
�!�! 8
(
�!�!8 9
idcronograma
�!�!9 E
,
�!�!E F
ref
�!�!G J
MsgRta
�!�!K Q
)
�!�!Q R
;
�!�!R S
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�! :
,Management_Consulta_Cronograma_VisitasResult
�!�! @
>
�!�!@ A4
&ConsultaCronogramaVisitasProcedimiento
�!�!B h
(
�!�!h i
int
�!�!i l

tipoFiltro
�!�!m w
,
�!�!w x
string
�!�!y 
Auditor�!�!� �
)�!�!� �
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! 4
&ConsultaCronogramaVisitasProcedimiento
�!�! E
(
�!�!E F

tipoFiltro
�!�!F P
,
�!�!P Q
Auditor
�!�!R Y
)
�!�!Y Z
;
�!�!Z [
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�! '
cronograma_visita_detalle
�!�! -
>
�!�!- .-
ConsultaCronogramaVisitaDetalle
�!�!/ N
(
�!�!N O
int
�!�!O R
idcronograma
�!�!S _
)
�!�!_ `
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! -
ConsultaCronogramaVisitaDetalle
�!�! >
(
�!�!> ?
idcronograma
�!�!? K
)
�!�!K L
;
�!�!L M
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�! 1
#cronograma_visita_detalle_indicador
�!�! 7
>
�!�!7 80
"ConsultaCronogramaVisitaDetalleInd
�!�!9 [
(
�!�![ \
int
�!�!\ _
idcronograma
�!�!` l
)
�!�!l m
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! 0
"ConsultaCronogramaVisitaDetalleInd
�!�! A
(
�!�!A B
idcronograma
�!�!B N
)
�!�!N O
;
�!�!O P
}
�!�! 	
public
�!�!  
cronograma_visitas
�!�! !
getvisitabyid
�!�!" /
(
�!�!/ 0
Int32
�!�!0 5
idvisita
�!�!6 >
,
�!�!> ?
ref
�!�!@ C 
MessageResponseOBJ
�!�!D V
MsgRta
�!�!W ]
)
�!�!] ^
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! 
getvisitabyid
�!�! ,
(
�!�!, -
idvisita
�!�!- 5
,
�!�!5 6
ref
�!�!7 :
MsgRta
�!�!; A
)
�!�!A B
;
�!�!B C
}
�!�! 	
public
�!�! 
void
�!�! '
InsertarCronogramaVisitas
�!�! -
(
�!�!- . 
cronograma_visitas
�!�!. @
objcronograma
�!�!A N
,
�!�!N O
ref
�!�!P S 
MessageResponseOBJ
�!�!T f
MsgRes
�!�!g m
)
�!�!m n
{
�!�! 	

DACInserta
�!�! 
.
�!�! '
InsertarCronogramaVisitas
�!�! 0
(
�!�!0 1
objcronograma
�!�!1 >
,
�!�!> ?
ref
�!�!@ C
MsgRes
�!�!D J
)
�!�!J K
;
�!�!K L
}
�!�! 	
public
�!�! 
void
�!�! )
ActualizarCronogramaVisitas
�!�! /
(
�!�!/ 0 
cronograma_visitas
�!�!0 B
objcronograma
�!�!C P
,
�!�!P Q
ref
�!�!R U 
MessageResponseOBJ
�!�!V h
MsgRes
�!�!i o
)
�!�!o p
{
�!�! 	
DACActualiza
�!�! 
.
�!�! )
ActualizarCronogramaVisitas
�!�! 4
(
�!�!4 5
objcronograma
�!�!5 B
,
�!�!B C
ref
�!�!D G
MsgRes
�!�!H N
)
�!�!N O
;
�!�!O P
}
�!�! 	
public
�!�! 
void
�!�! #
insertardetallevisita
�!�! )
(
�!�!) *
int
�!�!* -
id_cronograma
�!�!. ;
,
�!�!; <
int
�!�!= @
id_regional
�!�!A L
,
�!�!L M
int
�!�!N Q
id_indicador
�!�!R ^
,
�!�!^ _
List
�!�!` d
<
�!�!d e
item_capitulo
�!�!e r
>
�!�!r s
listadoitems�!�!t �
,�!�!� �
ref�!�!� �"
MessageResponseOBJ�!�!� �
MsgRes�!�!� �
)�!�!� �
{
�!�! 	

DACInserta
�!�! 
.
�!�! #
insertardetallevisita
�!�! ,
(
�!�!, -
id_cronograma
�!�!- :
,
�!�!: ;
id_regional
�!�!< G
,
�!�!G H
id_indicador
�!�!I U
,
�!�!U V
listadoitems
�!�!W c
,
�!�!c d
ref
�!�!e h
MsgRes
�!�!i o
)
�!�!o p
;
�!�!p q
}
�!�! 	
public
�!�! 
void
�!�! *
insertarcalificacionesvisita
�!�! 0
(
�!�!0 1
int
�!�!1 4
idcronograma
�!�!5 A
,
�!�!A B
string
�!�!C I
[
�!�!I J
]
�!�!J K
calificaciones
�!�!L Z
,
�!�!Z [
ref
�!�!\ _ 
MessageResponseOBJ
�!�!` r
MsgRes
�!�!s y
)
�!�!y z
{
�!�! 	

DACInserta
�!�! 
.
�!�! *
insertarcalificacionesvisita
�!�! 3
(
�!�!3 4
idcronograma
�!�!4 @
,
�!�!@ A
calificaciones
�!�!B P
,
�!�!P Q
ref
�!�!R U
MsgRes
�!�!V \
)
�!�!\ ]
;
�!�!] ^
}
�!�! 	
public
�!�! 
int
�!�! ,
insertardetallevisitaindicador
�!�! 1
(
�!�!1 2
List
�!�!2 6
<
�!�!6 7 
capitulo_indicador
�!�!7 I
>
�!�!I J
	capitulos
�!�!K T
,
�!�!T U
int
�!�!V Y
idcronograma
�!�!Z f
,
�!�!f g
int
�!�!h k
idprestador
�!�!l w
,
�!�!w x
ref
�!�!y |!
MessageResponseOBJ�!�!} �
MsgRes�!�!� �
)�!�!� �
{
�!�! 	
return
�!�! 

DACInserta
�!�! 
.
�!�! ,
insertardetallevisitaindicador
�!�! <
(
�!�!< =
	capitulos
�!�!= F
,
�!�!F G
idcronograma
�!�!H T
,
�!�!T U
idprestador
�!�!V a
,
�!�!a b
ref
�!�!c f
MsgRes
�!�!g m
)
�!�!m n
;
�!�!n o
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�! 
	capitulos
�!�! 
>
�!�! 
GetCapitulosList
�!�! /
(
�!�!/ 0
)
�!�!0 1
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! 
GetCapitulosList
�!�! /
(
�!�!/ 0
)
�!�!0 1
;
�!�!1 2
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�!  
capitulo_indicador
�!�! &
>
�!�!& '%
GetCapitulosByIndicador
�!�!( ?
(
�!�!? @
Int32
�!�!@ E
?
�!�!E F
idindicador
�!�!G R
,
�!�!R S
Int32
�!�!T Y

idregioanl
�!�!Z d
,
�!�!d e
ref
�!�!f i 
MessageResponseOBJ
�!�!j |
MsgRes�!�!} �
)�!�!� �
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! %
GetCapitulosByIndicador
�!�! 6
(
�!�!6 7
idindicador
�!�!7 B
,
�!�!B C

idregioanl
�!�!D N
,
�!�!N O
ref
�!�!P S
MsgRes
�!�!T Z
)
�!�!Z [
;
�!�![ \
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�! 2
$ManagementCalidadDtllIndicadorResult
�!�! 8
>
�!�!8 9.
 GetCapitulosEvaluadosByIndicador
�!�!: Z
(
�!�!Z [
Int32
�!�![ `
?
�!�!` a
idindicador
�!�!b m
,
�!�!m n
Int32
�!�!o t

idregioanl
�!�!u 
,�!�! �
ref�!�!� �"
MessageResponseOBJ�!�!� �
MsgRes�!�!� �
)�!�!� �
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! .
 GetCapitulosEvaluadosByIndicador
�!�! ?
(
�!�!? @
idindicador
�!�!@ K
,
�!�!K L

idregioanl
�!�!M W
,
�!�!W X
ref
�!�!Y \
MsgRes
�!�!] c
)
�!�!c d
;
�!�!d e
}
�!�! 	
public
�!�!  
capitulo_indicador
�!�! !"
getcapbyindicadorcap
�!�!" 6
(
�!�!6 7
int
�!�!7 :

idcapitulo
�!�!; E
,
�!�!E F
int
�!�!G J
idindicador
�!�!K V
,
�!�!V W
int
�!�!X [

idregional
�!�!\ f
)
�!�!f g
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! "
getcapbyindicadorcap
�!�! 3
(
�!�!3 4

idcapitulo
�!�!4 >
,
�!�!> ?
idindicador
�!�!@ K
,
�!�!K L

idregional
�!�!M W
)
�!�!W X
;
�!�!X Y
}
�!�! 	
public
�!�! 
List
�!�! 
<
�!�! 
indicadores
�!�! 
>
�!�!  "
ConsultarIndicadores
�!�!! 5
(
�!�!5 6
int
�!�!6 9
?
�!�!9 :
idindicador
�!�!; F
,
�!�!F G
ref
�!�!H K 
MessageResponseOBJ
�!�!L ^
MegRes
�!�!_ e
)
�!�!e f
{
�!�! 	
return
�!�! 
DACConsulta
�!�! 
.
�!�! "
ConsultarIndicadores
�!�! 3
(
�!�!3 4
idindicador
�!�!4 ?
,
�!�!? @
ref
�!�!A D
MegRes
�!�!E K
)
�!�!K L
;
�!�!L M
}
�!�! 	
public
�!�! 
item_capitulo
�!�! 
Getitemcapbyid
�!�! +
(
�!�!+ ,
Int32
�!�!, 1
IdItem
�!�!2 8
)
�!�!8 9
{
�!�! 	
return
�"�" 
DACConsulta
�"�" 
.
�"�" 
Getitemcapbyid
�"�" -
(
�"�"- .
IdItem
�"�". 4
)
�"�"4 5
;
�"�"5 6
}
�"�" 	
public
�"�" 
List
�"�" 
<
�"�" 
item_capitulo
�"�" !
>
�"�"! " 
Getitemcapbyindcap
�"�"# 5
(
�"�"5 6
Int32
�"�"6 ;

idregional
�"�"< F
,
�"�"F G
Int32
�"�"H M
idindicador
�"�"N Y
,
�"�"Y Z
Int32
�"�"[ `
?
�"�"` a
idcap
�"�"b g
)
�"�"g h
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�"  
Getitemcapbyindcap
�"�" 1
(
�"�"1 2

idregional
�"�"2 <
,
�"�"< =
idindicador
�"�"> I
,
�"�"I J
idcap
�"�"K P
)
�"�"P Q
;
�"�"Q R
}
�"�" 	
public
�"�" 
List
�"�" 
<
�"�" '
cronograma_visita_detalle
�"�" -
>
�"�"- .$
Getdetalllescronograma
�"�"/ E
(
�"�"E F
int
�"�"F I
idcronograma
�"�"J V
)
�"�"V W
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�" $
Getdetalllescronograma
�"�" 5
(
�"�"5 6
idcronograma
�"�"6 B
)
�"�"B C
;
�"�"C D
}
�"�" 	
public
�"�" 
bool
�"�" $
ActualizarItemCapitulo
�"�" *
(
�"�"* +
item_capitulo
�"�"+ 8
objitem
�"�"9 @
)
�"�"@ A
{
�"�" 	
return
�"�" 
DACActualiza
�"�" 
.
�"�"  $
ActualizarItemCapitulo
�"�"  6
(
�"�"6 7
objitem
�"�"7 >
)
�"�"> ?
;
�"�"? @
}
�"�" 	
public
�"�" 
	capitulos
�"�" 
Getcapitulobyid
�"�" (
(
�"�"( )
Int32
�"�") .

idcapitulo
�"�"/ 9
)
�"�"9 :
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�" 
Getcapitulobyid
�"�" .
(
�"�". /

idcapitulo
�"�"/ 9
)
�"�"9 :
;
�"�": ;
}
�"�" 	
public
�"�" 
bool
�"�" "
InsertarItemCapitulo
�"�" (
(
�"�"( )
item_capitulo
�"�") 6
itemcapitulo
�"�"7 C
)
�"�"C D
{
�"�" 	
return
�"�" 

DACInserta
�"�" 
.
�"�" "
InsertarItemCapitulo
�"�" 2
(
�"�"2 3
itemcapitulo
�"�"3 ?
)
�"�"? @
;
�"�"@ A
}
�"�" 	
public
�"�" 
bool
�"�" 
InsertaCapitulo
�"�" #
(
�"�"# $
	capitulos
�"�"$ -
capitulo
�"�". 6
)
�"�"6 7
{
�"�" 	
return
�"�" 

DACInserta
�"�" 
.
�"�" 
InsertaCapitulo
�"�" -
(
�"�"- .
capitulo
�"�". 6
)
�"�"6 7
;
�"�"7 8
}
�"�" 	
public
�"�" 
bool
�"�" )
ActualizarCapituloIndicador
�"�" /
(
�"�"/ 0
Int32
�"�"0 5!
idcapituloIndicador
�"�"6 I
,
�"�"I J
int
�"�"K N
pesogen
�"�"O V
)
�"�"V W
{
�"�" 	
return
�"�" 
DACActualiza
�"�" 
.
�"�"  )
ActualizarCapituloIndicador
�"�"  ;
(
�"�"; <!
idcapituloIndicador
�"�"< O
,
�"�"O P
pesogen
�"�"Q X
)
�"�"X Y
;
�"�"Y Z
}
�"�" 	
public
�"�" "
Ref_RIPS_Prestadores
�"�" #
getPrestador
�"�"$ 0
(
�"�"0 1
double
�"�"1 7
codprestador
�"�"8 D
,
�"�"D E
ref
�"�"F I 
MessageResponseOBJ
�"�"J \
MsgRes
�"�"] c
)
�"�"c d
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�" 
getPrestador
�"�" +
(
�"�"+ ,
codprestador
�"�", 8
,
�"�"8 9
ref
�"�": =
MsgRes
�"�"> D
)
�"�"D E
;
�"�"E F
}
�"�" 	
public
�"�" 
List
�"�" 
<
�"�" %
ref_usuario_responsable
�"�" +
>
�"�"+ ,*
ConsultResponsablesbyusuario
�"�"- I
(
�"�"I J
Int32
�"�"J O
	idusuario
�"�"P Y
,
�"�"Y Z
ref
�"�"[ ^ 
MessageResponseOBJ
�"�"_ q
MsgRes
�"�"r x
)
�"�"x y
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�" *
ConsultResponsablesbyusuario
�"�" ;
(
�"�"; <
	idusuario
�"�"< E
,
�"�"E F
ref
�"�"G J
MsgRes
�"�"K Q
)
�"�"Q R
;
�"�"R S
}
�"�" 	
public
�"�" 
List
�"�" 
<
�"�" 
sis_usuario
�"�" 
>
�"�"  
LstResponsables
�"�"! 0
(
�"�"0 1
)
�"�"1 2
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�" 
LstResponsables
�"�" .
(
�"�". /
)
�"�"/ 0
;
�"�"0 1
}
�"�" 	
public
�"�" 
List
�"�" 
<
�"�" !
calidad_prestadores
�"�" '
>
�"�"' ( 
getprestadoresList
�"�") ;
(
�"�"; <
)
�"�"< =
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�"  
getprestadoresList
�"�" 1
(
�"�"1 2
)
�"�"2 3
;
�"�"3 4
}
�"�" 	
public
�"�" !
calidad_prestadores
�"�" "
getPresadorById
�"�"# 2
(
�"�"2 3
int
�"�"3 6
idprestador
�"�"7 B
)
�"�"B C
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�" 
getPresadorById
�"�" .
(
�"�". /
idprestador
�"�"/ :
)
�"�": ;
;
�"�"; <
}
�"�" 	
public
�"�" 
List
�"�" 
<
�"�" &
calidad_ref_especialidad
�"�" ,
>
�"�", -$
GetRefEspecialidadList
�"�". D
(
�"�"D E
)
�"�"E F
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�" $
GetRefEspecialidadList
�"�" 5
(
�"�"5 6
)
�"�"6 7
;
�"�"7 8
}
�"�" 	
public
�"�" 
List
�"�" 
<
�"�" !
calidad_ref_regimen
�"�" '
>
�"�"' ( 
GetRefRegimentList
�"�") ;
(
�"�"; <
)
�"�"< =
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�"  
GetRefRegimentList
�"�" 1
(
�"�"1 2
)
�"�"2 3
;
�"�"3 4
}
�"�" 	
public
�"�" 
List
�"�" 
<
�"�" 
Ref_clase_persona
�"�" %
>
�"�"% &!
GetClasePersonaList
�"�"' :
(
�"�": ;
)
�"�"; <
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�" !
GetClasePersonaList
�"�" 2
(
�"�"2 3
)
�"�"3 4
;
�"�"4 5
}
�"�" 	
public
�"�" 
List
�"�" 
<
�"�" ,
vw_calidad_prestador_indicador
�"�" 2
>
�"�"2 3)
GetListIndicadoresPrestador
�"�"4 O
(
�"�"O P
int
�"�"P S
id_prestador
�"�"T `
)
�"�"` a
{
�"�" 	
return
�"�" 
DACConsulta
�"�" 
.
�"�" )
GetListIndicadoresPrestador
�"�" :
(
�"�": ;
id_prestador
�"�"; G
)
�"�"G H
;
�"�"H I
}
�"�" 	
public
�"�" 
void
�"�" 
InsertarPrestador
�"�" %
(
�"�"% &!
calidad_prestadores
�"�"& 9
Obj
�"�": =
,
�"�"= >
ref
�"�"? B 
MessageResponseOBJ
�"�"C U
MsgRes
�"�"V \
)
�"�"\ ]
{
�"�" 	

DACInserta
�"�" 
.
�"�" 
InsertarPrestador
�"�" (
(
�"�"( )
Obj
�"�") ,
,
�"�", -
ref
�"�". 1
MsgRes
�"�"2 8
)
�"�"8 9
;
�"�"9 :
}
�"�" 	
public
�"�" 
void
�"�" 
InsertarVisita
�"�" "
(
�"�"" # 
cronograma_visitas
�"�"# 5
ObjCronocrama
�"�"6 C
,
�"�"C D
ref
�"�"E H 
MessageResponseOBJ
�"�"I [
MsgRes
�"�"\ b
)
�"�"b c
{
�"�" 	

DACInserta
�"�" 
.
�"�" 
InsertarVisita
�"�" %
(
�"�"% &
ObjCronocrama
�"�"& 3
,
�"�"3 4
ref
�"�"5 8
MsgRes
�"�"9 ?
)
�"�"? @
;
�"�"@ A
}
�"�" 	
public
�"�" 
void
�"�" (
insertaRegionalPrestadores
�"�" .
(
�"�". /
Int32
�"�"/ 4

idregional
�"�"5 ?
,
�"�"? @
List
�"�"A E
<
�"�"E F
int
�"�"F I
>
�"�"I J
prestadores
�"�"K V
)
�"�"V W
{
�"�" 	

DACInserta
�"�" 
.
�"�" (
insertaRegionalPrestadores
�"�" 1
(
�"�"1 2

idregional
�"�"2 <
,
�"�"< =
prestadores
�"�"> I
)
�"�"I J
;
�"�"J K
}
�"�" 	
public
�"�" 
void
�"�" (
InsertarCapitulosPrestador
�"�" .
(
�"�". /
Int32
�"�"/ 4

idregional
�"�"5 ?
,
�"�"? @
Int32
�"�"A F
idindicador
�"�"G R
,
�"�"R S
List
�"�"T X
<
�"�"X Y
int
�"�"Y \
>
�"�"\ ]
	capitulos
�"�"^ g
)
�"�"g h
{
�"�" 	

DACInserta
�"�" 
.
�"�" (
InsertarCapitulosPrestador
�"�" 1
(
�"�"1 2

idregional
�"�"2 <
,
�"�"< =
idindicador
�"�"> I
,
�"�"I J
	capitulos
�"�"K T
)
�"�"T U
;
�"�"U V
}
�"�" 	
public
�"�" 
void
�"�" 
EliminarCapitulo
�"�" $
(
�"�"$ %
int
�"�"% (

idcapitulo
�"�") 3
)
�"�"3 4
{
�"�" 	

DACElimina
�"�" 
.
�"�" 
EliminarCapitulo
�"�" '
(
�"�"' (

idcapitulo
�"�"( 2
)
�"�"2 3
;
�"�"3 4
}
�"�" 	
public
�"�" 
void
�"�" 
EliminarVisita
�"�" "
(
�"�"" #
Int32
�"�"# (
idvisita
�"�") 1
,
�"�"1 2%
log_eliminacion_visitas
�"�"3 J
obj
�"�"K N
,
�"�"N O
ref
�"�"P S 
MessageResponseOBJ
�"�"T f
MsgRes
�"�"g m
)
�"�"m n
{
�"�" 	

DACElimina
�"�" 
.
�"�" 
EliminarVisita
�"�" %
(
�"�"% &
idvisita
�"�"& .
,
�"�". /
obj
�"�"0 3
,
�"�"3 4
ref
�"�"5 8
MsgRes
�"�"9 ?
)
�"�"? @
;
�"�"@ A
}
�"�" 	
public
�"�" 
void
�"�" &
EliminarEvaluacionVisita
�"�" ,
(
�"�", -
Int32
�"�"- 2
idvisita
�"�"3 ;
,
�"�"; <%
log_eliminacion_visitas
�"�"= T
obj
�"�"U X
,
�"�"X Y
ref
�"�"Z ] 
MessageResponseOBJ
�"�"^ p
MsgRes
�"�"q w
)
�"�"w x
{
�"�" 	

DACElimina
�"�" 
.
�"�" &
EliminarEvaluacionVisita
�"�" /
(
�"�"/ 0
idvisita
�"�"0 8
,
�"�"8 9
obj
�"�": =
,
�"�"= >
ref
�"�"? B
MsgRes
�"�"C I
)
�"�"I J
;
�"�"J K
}
�"�" 	
public
�"�" 
void
�"�" 
EliminarEgreso
�"�" "
(
�"�"" #
Int32
�"�"# (
id
�"�") +
,
�"�"+ ,
ref
�"�"- 0 
MessageResponseOBJ
�"�"1 C
MsgRes
�"�"D J
)
�"�"J K
{
�"�" 	

DACElimina
�"�" 
.
�"�" 
EliminarEgreso
�"�" %
(
�"�"% &
id
�"�"& (
,
�"�"( )
ref
�"�"* -
MsgRes
�"�". 4
)
�"�"4 5
;
�"�"5 6
}
�"�" 	
public
�"�" 
Int32
�"�" #
InsertarCargueRanking
�"�" *
(
�"�"* +$
calidad_cargue_ranking
�"�"+ A
ranking
�"�"B I
)
�"�"I J
{
�"�" 	
return
�"�" 

DACInserta
�"�" 
.
�"�" #
InsertarCargueRanking
�"�" 3
(
�"�"3 4
ranking
�"�"4 ;
)
�"�"; <
;
�"�"< =
}
�"�" 	
public
�#�# 
void
�#�# *
InsertarDetalleCargueRanking
�#�# 0
(
�#�#0 1
List
�#�#1 5
<
�#�#5 6,
calidad_detalle_cargue_ranking
�#�#6 T
>
�#�#T U
detalleranking
�#�#V d
)
�#�#d e
{
�#�# 	

DACInserta
�#�# 
.
�#�# *
InsertarDetalleCargueRanking
�#�# 3
(
�#�#3 4
detalleranking
�#�#4 B
)
�#�#B C
;
�#�#C D
}
�#�# 	
public
�#�# 
void
�#�# #
InsertarNovedadVisita
�#�# )
(
�#�#) *)
cronograma_visita_novedades
�#�#* E
obj
�#�#F I
,
�#�#I J
ref
�#�#K N 
MessageResponseOBJ
�#�#O a
MsgRes
�#�#b h
)
�#�#h i
{
�#�# 	

DACInserta
�#�# 
.
�#�# #
InsertarNovedadVisita
�#�# ,
(
�#�#, -
obj
�#�#- 0
,
�#�#0 1
ref
�#�#2 5
MsgRes
�#�#6 <
)
�#�#< =
;
�#�#= >
}
�#�# 	
public
�#�# 
void
�#�# :
,InsertarMasivamenteReportesEvaluacionVisitas
�#�# @
(
�#�#@ A
List
�#�#A E
<
�#�#E F?
1cronograma_visitas_reportesDoc_evaluacion_calidad
�#�#F w
>
�#�#w x
obj
�#�#y |
,
�#�#| }
ref�#�#~ �"
MessageResponseOBJ�#�#� �
MsgRes�#�#� �
)�#�#� �
{
�#�# 	

DACInserta
�#�# 
.
�#�# :
,InsertarMasivamenteReportesEvaluacionVisitas
�#�# C
(
�#�#C D
obj
�#�#D G
,
�#�#G H
ref
�#�#I L
MsgRes
�#�#M S
)
�#�#S T
;
�#�#T U
}
�#�# 	
public
�#�# 5
'Management_get_info_visita_por_idResult
�#�# 6
GetInfoVisitaById
�#�#7 H
(
�#�#H I
int
�#�#I L
idCronograma
�#�#M Y
)
�#�#Y Z
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# 
GetInfoVisitaById
�#�# 0
(
�#�#0 1
idCronograma
�#�#1 =
)
�#�#= >
;
�#�#> ?
}
�#�# 	
public
�#�# 
void
�#�# !
actualizarPrestador
�#�# '
(
�#�#' (!
calidad_prestadores
�#�#( ;
Obj
�#�#< ?
,
�#�#? @
int
�#�#A D
idprestador
�#�#E P
)
�#�#P Q
{
�#�# 	
DACActualiza
�#�# 
.
�#�# !
actualizarPrestador
�#�# ,
(
�#�#, -
Obj
�#�#- 0
,
�#�#0 1
idprestador
�#�#2 =
)
�#�#= >
;
�#�#> ?
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# $
Ref_calidad_municipios
�#�# *
>
�#�#* +
GetMunicipiosDane
�#�#, =
(
�#�#= >
)
�#�#> ?
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# 
GetMunicipiosDane
�#�# 0
(
�#�#0 1
)
�#�#1 2
;
�#�#2 3
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# 

vw_visitas
�#�# 
>
�#�# 
GetListVisitas
�#�#  .
(
�#�#. /
Int32
�#�#/ 4
?
�#�#4 5
	id_visita
�#�#6 ?
,
�#�#? @
Int32
�#�#A F
?
�#�#F G
id_prestador
�#�#H T
,
�#�#T U
string
�#�#V \
numcontrato
�#�#] h
)
�#�#h i
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# 
GetListVisitas
�#�# -
(
�#�#- .
	id_visita
�#�#. 7
,
�#�#7 8
id_prestador
�#�#9 E
,
�#�#E F
numcontrato
�#�#G R
)
�#�#R S
;
�#�#S T
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# .
 ref_cronograma_visitas_novedades
�#�# 4
>
�#�#4 5 
GetListTipoNovedad
�#�#6 H
(
�#�#H I
)
�#�#I J
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�#  
GetListTipoNovedad
�#�# 1
(
�#�#1 2
)
�#�#2 3
;
�#�#3 4
}
�#�# 	
public
�#�# 
void
�#�#  
GuardarActaVisitas
�#�# &
(
�#�#& ')
cronograma_visita_documento
�#�#' B
obj
�#�#C F
,
�#�#F G
ref
�#�#H K 
MessageResponseOBJ
�#�#L ^
MsgRes
�#�#_ e
)
�#�#e f
{
�#�# 	

DACInserta
�#�# 
.
�#�#  
GuardarActaVisitas
�#�# )
(
�#�#) *
obj
�#�#* -
,
�#�#- .
ref
�#�#/ 2
MsgRes
�#�#3 9
)
�#�#9 :
;
�#�#: ;
}
�#�# 	
public
�#�# )
cronograma_visita_documento
�#�# *
Getactavisita
�#�#+ 8
(
�#�#8 9
int
�#�#9 <
idvisita
�#�#= E
)
�#�#E F
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# 
Getactavisita
�#�# ,
(
�#�#, -
idvisita
�#�#- 5
)
�#�#5 6
;
�#�#6 7
}
�#�# 	
public
�#�# =
/management_cronograma_visita_documento_idResult
�#�# >
Getactavisita2
�#�#? M
(
�#�#M N
int
�#�#N Q
idvisita
�#�#R Z
)
�#�#Z [
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# 
Getactavisita2
�#�# -
(
�#�#- .
idvisita
�#�#. 6
)
�#�#6 7
;
�#�#7 8
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# B
4management_cronograma_visita_documento_sinRutaResult
�#�# H
>
�#�#H I"
GetactavisitaSinRuta
�#�#J ^
(
�#�#^ _
)
�#�#_ `
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# "
GetactavisitaSinRuta
�#�# 3
(
�#�#3 4
)
�#�#4 5
;
�#�#5 6
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# #
vw_visitas_documentos
�#�# )
>
�#�#) *
GetActasVisitas
�#�#+ :
(
�#�#: ;
int
�#�#; >
regional
�#�#? G
,
�#�#G H
int
�#�#I L
mes
�#�#M P
,
�#�#P Q
int
�#�#R U
año
�#�#V Y
)
�#�#Y Z
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# 
GetActasVisitas
�#�# .
(
�#�#. /
regional
�#�#/ 7
,
�#�#7 8
mes
�#�#9 <
,
�#�#< =
año
�#�#> A
)
�#�#A B
;
�#�#B C
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# 2
$ManagementConsultaGnralVisitasResult
�#�# 8
>
�#�#8 9'
GetConsultageneralVisitas
�#�#: S
(
�#�#S T
int
�#�#T W
regional
�#�#X `
,
�#�#` a
int
�#�#b e
	prestador
�#�#f o
,
�#�#o p
DateTime
�#�#q y
fecha
�#�#z 
,�#�# �
string�#�#� �
nit�#�#� �
,�#�#� �
string�#�#� �
codsap�#�#� �
)�#�#� �
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# '
GetConsultageneralVisitas
�#�# 8
(
�#�#8 9
regional
�#�#9 A
,
�#�#A B
	prestador
�#�#C L
,
�#�#L M
fecha
�#�#N S
,
�#�#S T
nit
�#�#U X
,
�#�#X Y
codsap
�#�#Z `
)
�#�#` a
;
�#�#a b
}
�#�# 	
public
�#�# '
cronograma_visita_detalle
�#�# ()
Getresultadovisitaindicador
�#�#) D
(
�#�#D E
int
�#�#E H
idvisita
�#�#I Q
,
�#�#Q R
int
�#�#S V
idindicador
�#�#W b
)
�#�#b c
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# )
Getresultadovisitaindicador
�#�# :
(
�#�#: ;
idvisita
�#�#; C
,
�#�#C D
idindicador
�#�#E P
)
�#�#P Q
;
�#�#Q R
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# /
!cronograma_visitas_calificaciones
�#�# 5
>
�#�#5 6%
GetCalificacionesVisita
�#�#7 N
(
�#�#N O
int
�#�#O R
id_cronograma
�#�#S `
)
�#�#` a
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# %
GetCalificacionesVisita
�#�# 6
(
�#�#6 7
id_cronograma
�#�#7 D
)
�#�#D E
;
�#�#E F
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# )
analisis_caso_salud_publica
�#�# /
>
�#�#/ 0-
ConsultaEvolucionAnalisisSaludP
�#�#1 P
(
�#�#P Q
Int32
�#�#Q V
idconcurrencia
�#�#W e
,
�#�#e f
Int32
�#�#g l
?
�#�#l m

IdAnalisis
�#�#n x
,
�#�#x y
ref
�#�#z }!
MessageResponseOBJ�#�#~ �
MsgRes�#�#� �
)�#�#� �
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# -
ConsultaEvolucionAnalisisSaludP
�#�# >
(
�#�#> ?
idconcurrencia
�#�#? M
,
�#�#M N

IdAnalisis
�#�#O Y
,
�#�#Y Z
ref
�#�#[ ^
MsgRes
�#�#_ e
)
�#�#e f
;
�#�#f g
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# #
analisis_caso_alertas
�#�# )
>
�#�#) *)
ConsultaAnalisisCasoAlertas
�#�#+ F
(
�#�#F G
Int32
�#�#G L
?
�#�#L M
idconcurrencia
�#�#N \
,
�#�#\ ]
Int32
�#�#^ c
?
�#�#c d

IdAnalisis
�#�#e o
,
�#�#o p
ref
�#�#q t!
MessageResponseOBJ�#�#u �
MsgRes�#�#� �
)�#�#� �
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# )
ConsultaAnalisisCasoAlertas
�#�# :
(
�#�#: ;
idconcurrencia
�#�#; I
,
�#�#I J

IdAnalisis
�#�#K U
,
�#�#U V
ref
�#�#W Z
MsgRes
�#�#[ a
)
�#�#a b
;
�#�#b c
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# $
analisis_caso_muestreo
�#�# *
>
�#�#* +*
ConsultaAnalisisCasoMuestreo
�#�#, H
(
�#�#H I
Int32
�#�#I N
?
�#�#N O
idconcurrencia
�#�#P ^
,
�#�#^ _
Int32
�#�#` e
?
�#�#e f

IdAnalisis
�#�#g q
,
�#�#q r
ref
�#�#s v!
MessageResponseOBJ�#�#w �
MsgRes�#�#� �
)�#�#� �
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# *
ConsultaAnalisisCasoMuestreo
�#�# ;
(
�#�#; <
idconcurrencia
�#�#< J
,
�#�#J K

IdAnalisis
�#�#L V
,
�#�#V W
ref
�#�#X [
MsgRes
�#�#\ b
)
�#�#b c
;
�#�#c d
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# 1
#ecop_concurrencia_eventos_en_asalud
�#�# 7
>
�#�#7 8,
ConsultaAnalisisEventosenSalud
�#�#9 W
(
�#�#W X
Int32
�#�#X ]
idconcurrencia
�#�#^ l
,
�#�#l m
Int32
�#�#n s
?
�#�#s t

IdAnalisis
�#�#u 
,�#�# �
ref�#�#� �"
MessageResponseOBJ�#�#� �
MsgRes�#�#� �
)�#�#� �
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# ,
ConsultaAnalisisEventosenSalud
�#�# =
(
�#�#= >
idconcurrencia
�#�#> L
,
�#�#L M

IdAnalisis
�#�#N X
,
�#�#X Y
ref
�#�#Z ]
MsgRes
�#�#^ d
)
�#�#d e
;
�#�#e f
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# 8
*ecop_concurrencia_eventos_en_salud_detalle
�#�# >
>
�#�#> ?3
%ConsultaAnalisisEventosenSaludDetalle
�#�#@ e
(
�#�#e f9
*ecop_concurrencia_eventos_en_salud_detalle�#�#f �
OBJ�#�#� �
,�#�#� �
ref�#�#� �"
MessageResponseOBJ�#�#� �
MsgRes�#�#� �
)�#�#� �
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# 3
%ConsultaAnalisisEventosenSaludDetalle
�#�# D
(
�#�#D E
OBJ
�#�#E H
,
�#�#H I
ref
�#�#J M
MsgRes
�#�#N T
)
�#�#T U
;
�#�#U V
}
�#�# 	
public
�#�# 
List
�#�# 
<
�#�# 8
*ecop_concurrencia_eventos_en_salud_detalle
�#�# >
>
�#�#> ?.
 GetAnalisisEventosenSaludDetalle
�#�#@ `
(
�#�#` a
int
�#�#a d

idanalisis
�#�#e o
)
�#�#o p
{
�#�# 	
return
�#�# 
DACConsulta
�#�# 
.
�#�# .
 GetAnalisisEventosenSaludDetalle
�#�# ?
(
�#�#? @

idanalisis
�#�#@ J
)
�#�#J K
;
�#�#K L
}
�#�# 	
public
�#�# 
int
�#�# #
InsertarAnalisisCasos
�#�# (
(
�#�#( )$
analisis_caso_original
�#�#) ?
Analisis
�#�#@ H
,
�#�#H I
ref
�#�#J M 
MessageResponseOBJ
�#�#N `
MsgRes
�#�#a g
)
�#�#g h
{
�#�# 	
return
�#�# 

DACInserta
�#�# 
.
�#�# #
InsertarAnalisisCasos
�#�# 3
(
�#�#3 4
Analisis
�#�#4 <
,
�#�#< =
ref
�#�#> A
MsgRes
�#�#B H
)
�#�#H I
;
�#�#I J
}
�#�# 	
public
�#�# 
int
�#�# &
InsertarAnalisisMuestreo
�#�# +
(
�#�#+ ,$
analisis_caso_muestreo
�#�#, B
AnalisisMuestreo
�#�#C S
,
�#�#S T
ref
�#�#U X 
MessageResponseOBJ
�#�#Y k
MsgRes
�#�#l r
)
�#�#r s
{
�#�# 	
return
�$�$ 

DACInserta
�$�$ 
.
�$�$ &
InsertarAnalisisMuestreo
�$�$ 6
(
�$�$6 7
AnalisisMuestreo
�$�$7 G
,
�$�$G H
ref
�$�$I L
MsgRes
�$�$M S
)
�$�$S T
;
�$�$T U
}
�$�$ 	
public
�$�$ 
int
�$�$ )
InsertarAnalisisCasosAlerta
�$�$ .
(
�$�$. /#
analisis_caso_alertas
�$�$/ D
AnalisisAlerta
�$�$E S
,
�$�$S T
ref
�$�$U X 
MessageResponseOBJ
�$�$Y k
MsgRes
�$�$l r
)
�$�$r s
{
�$�$ 	
return
�$�$ 

DACInserta
�$�$ 
.
�$�$ )
InsertarAnalisisCasosAlerta
�$�$ 9
(
�$�$9 :
AnalisisAlerta
�$�$: H
,
�$�$H I
ref
�$�$J M
MsgRes
�$�$N T
)
�$�$T U
;
�$�$U V
}
�$�$ 	
public
�$�$ 
void
�$�$ %
ActualizarAnalisisCasos
�$�$ +
(
�$�$+ ,$
analisis_caso_original
�$�$, B
Analisis
�$�$C K
,
�$�$K L
ref
�$�$M P 
MessageResponseOBJ
�$�$Q c
MsgRes
�$�$d j
)
�$�$j k
{
�$�$ 	
DACActualiza
�$�$ 
.
�$�$ %
ActualizarAnalisisCasos
�$�$ 0
(
�$�$0 1
Analisis
�$�$1 9
,
�$�$9 :
ref
�$�$; >
MsgRes
�$�$? E
)
�$�$E F
;
�$�$F G
}
�$�$ 	
public
�$�$ 
void
�$�$ (
ActualizarAnalisisMuestreo
�$�$ .
(
�$�$. /$
analisis_caso_muestreo
�$�$/ E
	AnalisisM
�$�$F O
,
�$�$O P
ref
�$�$Q T 
MessageResponseOBJ
�$�$U g
MsgRes
�$�$h n
)
�$�$n o
{
�$�$ 	
DACActualiza
�$�$ 
.
�$�$ (
ActualizarAnalisisMuestreo
�$�$ 3
(
�$�$3 4
	AnalisisM
�$�$4 =
,
�$�$= >
ref
�$�$? B
MsgRes
�$�$C I
)
�$�$I J
;
�$�$J K
}
�$�$ 	
public
�$�$ 
void
�$�$ '
ActualizarAnalisisAlertas
�$�$ -
(
�$�$- .#
analisis_caso_alertas
�$�$. C
	AnalisisA
�$�$D M
,
�$�$M N
ref
�$�$O R 
MessageResponseOBJ
�$�$S e
MsgRes
�$�$f l
)
�$�$l m
{
�$�$ 	
DACActualiza
�$�$ 
.
�$�$ '
ActualizarAnalisisAlertas
�$�$ 2
(
�$�$2 3
	AnalisisA
�$�$3 <
,
�$�$< =
ref
�$�$> A
MsgRes
�$�$B H
)
�$�$H I
;
�$�$I J
}
�$�$ 	
public
�$�$ 
int
�$�$ )
InsertarAnalisisCasosSaludP
�$�$ .
(
�$�$. /)
analisis_caso_salud_publica
�$�$/ J
Analisis
�$�$K S
,
�$�$S T
ref
�$�$U X 
MessageResponseOBJ
�$�$Y k
MsgRes
�$�$l r
)
�$�$r s
{
�$�$ 	
return
�$�$ 

DACInserta
�$�$ 
.
�$�$ )
InsertarAnalisisCasosSaludP
�$�$ 9
(
�$�$9 :
Analisis
�$�$: B
,
�$�$B C
ref
�$�$D G
MsgRes
�$�$H N
)
�$�$N O
;
�$�$O P
}
�$�$ 	
public
�$�$ 
void
�$�$ 0
"ActualizarAnalisisCasoSaludPublica
�$�$ 6
(
�$�$6 7)
analisis_caso_salud_publica
�$�$7 R
analisis
�$�$S [
,
�$�$[ \
ref
�$�$] ` 
MessageResponseOBJ
�$�$a s
MsgRes
�$�$t z
)
�$�$z {
{
�$�$ 	
DACActualiza
�$�$ 
.
�$�$ 0
"ActualizarAnalisisCasoSaludPublica
�$�$ ;
(
�$�$; <
analisis
�$�$< D
,
�$�$D E
ref
�$�$F I
MsgRes
�$�$J P
)
�$�$P Q
;
�$�$Q R
}
�$�$ 	
public
�$�$ 
void
�$�$ *
InsertarAnalisisCasosEventos
�$�$ 0
(
�$�$0 11
#ecop_concurrencia_eventos_en_asalud
�$�$1 T
Analisis
�$�$U ]
,
�$�$] ^
List
�$�$_ c
<
�$�$c d9
*ecop_concurrencia_eventos_en_salud_detalle�$�$d �
>�$�$� �

otrosiList�$�$� �
,�$�$� �
ref�$�$� �"
MessageResponseOBJ�$�$� �
MsgRes�$�$� �
)�$�$� �
{
�$�$ 	

DACInserta
�$�$ 
.
�$�$ *
InsertarAnalisisCasosEventos
�$�$ 3
(
�$�$3 4
Analisis
�$�$4 <
,
�$�$< =

otrosiList
�$�$> H
,
�$�$H I
ref
�$�$J M
MsgRes
�$�$N T
)
�$�$T U
;
�$�$U V
}
�$�$ 	
public
�$�$ 
void
�$�$ -
InsertarAnalisisCasosEventosDet
�$�$ 3
(
�$�$3 41
#ecop_concurrencia_eventos_en_asalud
�$�$4 W
Analisis
�$�$X `
,
�$�$` a
List
�$�$b f
<
�$�$f g9
*ecop_concurrencia_eventos_en_salud_detalle�$�$g �
>�$�$� �

otrosiList�$�$� �
,�$�$� �
ref�$�$� �"
MessageResponseOBJ�$�$� �
MsgRes�$�$� �
)�$�$� �
{
�$�$ 	

DACInserta
�$�$ 
.
�$�$ -
InsertarAnalisisCasosEventosDet
�$�$ 6
(
�$�$6 7
Analisis
�$�$7 ?
,
�$�$? @

otrosiList
�$�$A K
,
�$�$K L
ref
�$�$M P
MsgRes
�$�$Q W
)
�$�$W X
;
�$�$X Y
}
�$�$ 	
public
�$�$ 
Int32
�$�$ 1
#InsertarAnalisisCasosEventosDetalle
�$�$ 8
(
�$�$8 98
*ecop_concurrencia_eventos_en_salud_detalle
�$�$9 c
OBJ
�$�$d g
,
�$�$g h
ref
�$�$i l 
MessageResponseOBJ
�$�$m 
MsgRes�$�$� �
)�$�$� �
{
�$�$ 	
return
�$�$ 

DACInserta
�$�$ 
.
�$�$ 1
#InsertarAnalisisCasosEventosDetalle
�$�$ A
(
�$�$A B
OBJ
�$�$B E
,
�$�$E F
ref
�$�$G J
MsgRes
�$�$K Q
)
�$�$Q R
;
�$�$R S
}
�$�$ 	
public
�$�$ 
void
�$�$ ,
ActualizarAnalisisEventosSalud
�$�$ 2
(
�$�$2 31
#ecop_concurrencia_eventos_en_asalud
�$�$3 V
Analisis
�$�$W _
,
�$�$_ `
ref
�$�$a d 
MessageResponseOBJ
�$�$e w
MsgRes
�$�$x ~
)
�$�$~ 
{
�$�$ 	
DACActualiza
�$�$ 
.
�$�$ ,
ActualizarAnalisisEventosSalud
�$�$ 7
(
�$�$7 8
Analisis
�$�$8 @
,
�$�$@ A
ref
�$�$B E
MsgRes
�$�$F L
)
�$�$L M
;
�$�$M N
}
�$�$ 	
public
�$�$ 
List
�$�$ 
<
�$�$ 2
$ManagmentReporteAnalisisCasoSPResult
�$�$ 8
>
�$�$8 9#
ReporteAnalisisCasoSP
�$�$: O
(
�$�$O P
Int32
�$�$P U
idconcurrencia
�$�$V d
,
�$�$d e
Int32
�$�$f k

idanalisis
�$�$l v
)
�$�$v w
{
�$�$ 	
return
�$�$ 
DACConsulta
�$�$ 
.
�$�$ #
ReporteAnalisisCasoSP
�$�$ 4
(
�$�$4 5
idconcurrencia
�$�$5 C
,
�$�$C D

idanalisis
�$�$E O
)
�$�$O P
;
�$�$P Q
}
�$�$ 	
public
�$�$ 
List
�$�$ 
<
�$�$ 3
%ManagmentReporteAnalisisCasoOrgResult
�$�$ 9
>
�$�$9 :)
ReporteAnalisisCasoOriginal
�$�$; V
(
�$�$V W
Int32
�$�$W \
idConcurrencia
�$�$] k
,
�$�$k l
Int32
�$�$m r

idAnalisis
�$�$s }
,
�$�$} ~
ref�$�$ �"
MessageResponseOBJ�$�$� �
MsgRes�$�$� �
)�$�$� �
{
�$�$ 	
return
�$�$ 
DACConsulta
�$�$ 
.
�$�$ )
ReporteAnalisisCasoOriginal
�$�$ :
(
�$�$: ;
idConcurrencia
�$�$; I
,
�$�$I J

idAnalisis
�$�$K U
,
�$�$U V
ref
�$�$W Z
MsgRes
�$�$[ a
)
�$�$a b
;
�$�$b c
}
�$�$ 	
public
�$�$ 
List
�$�$ 
<
�$�$ .
 ManagmentReporteAnalisisESResult
�$�$ 4
>
�$�$4 5!
ReporteEventosSalud
�$�$6 I
(
�$�$I J
Int32
�$�$J O
IdConcurrencia
�$�$P ^
,
�$�$^ _
Int32
�$�$` e

Idanalisis
�$�$f p
)
�$�$p q
{
�$�$ 	
return
�$�$ 
DACConsulta
�$�$ 
.
�$�$ !
ReporteEventosSalud
�$�$ 2
(
�$�$2 3
IdConcurrencia
�$�$3 A
,
�$�$A B

Idanalisis
�$�$C M
)
�$�$M N
;
�$�$N O
}
�$�$ 	
public
�$�$ 
List
�$�$ 
<
�$�$ '
vw_tablero_analisis_casos
�$�$ -
>
�$�$- .*
ConsultaTableroAnalisisCasos
�$�$/ K
(
�$�$K L
ref
�$�$L O 
MessageResponseOBJ
�$�$P b
MsgRes
�$�$c i
)
�$�$i j
{
�$�$ 	
return
�$�$ 
DACConsulta
�$�$ 
.
�$�$ *
ConsultaTableroAnalisisCasos
�$�$ ;
(
�$�$; <
ref
�$�$< ?
MsgRes
�$�$@ F
)
�$�$F G
;
�$�$G H
}
�$�$ 	
public
�$�$ 
void
�$�$ +
Insertargestionanalisisdecaso
�$�$ 1
(
�$�$1 2$
GestionAnalisisDeCasos
�$�$2 H
Analisis
�$�$I Q
,
�$�$Q R
ref
�$�$S V 
MessageResponseOBJ
�$�$W i
MsgRes
�$�$j p
)
�$�$p q
{
�$�$ 	

DACInserta
�$�$ 
.
�$�$ +
Insertargestionanalisisdecaso
�$�$ 4
(
�$�$4 5
Analisis
�$�$5 =
,
�$�$= >
ref
�$�$? B
MsgRes
�$�$C I
)
�$�$I J
;
�$�$J K
}
�$�$ 	
public
�$�$ 
void
�$�$ -
Actualizargestionanalisisdecaso
�$�$ 3
(
�$�$3 4$
GestionAnalisisDeCasos
�$�$4 J
Analisis
�$�$K S
,
�$�$S T
ref
�$�$U X 
MessageResponseOBJ
�$�$Y k
MsgRes
�$�$l r
)
�$�$r s
{
�$�$ 	
DACActualiza
�$�$ 
.
�$�$ -
Actualizargestionanalisisdecaso
�$�$ 8
(
�$�$8 9
Analisis
�$�$9 A
,
�$�$A B
ref
�$�$C F
MsgRes
�$�$G M
)
�$�$M N
;
�$�$N O
}
�$�$ 	
public
�$�$ $
GestionAnalisisDeCasos
�$�$ % 
GetAnalisisGestion
�$�$& 8
(
�$�$8 9
Int32
�$�$9 >
?
�$�$> ?
idtipoanalisis
�$�$@ N
,
�$�$N O
Int32
�$�$P U
?
�$�$U V
	idanalsis
�$�$W `
,
�$�$` a
ref
�$�$b e 
MessageResponseOBJ
�$�$f x
MsgRes
�$�$y 
)�$�$ �
{
�$�$ 	
return
�$�$ 
DACConsulta
�$�$ 
.
�$�$  
GetAnalisisGestion
�$�$ 1
(
�$�$1 2
idtipoanalisis
�$�$2 @
,
�$�$@ A
	idanalsis
�$�$B K
,
�$�$K L
ref
�$�$M P
MsgRes
�$�$Q W
)
�$�$W X
;
�$�$X Y
}
�$�$ 	
public
�$�$ 
List
�$�$ 
<
�$�$ &
vw_analisis_caso_alertas
�$�$ ,
>
�$�$, -"
GetIdAnalisisAlertas
�$�$. B
(
�$�$B C
Int32
�$�$C H
id_concurrencia
�$�$I X
,
�$�$X Y
ref
�$�$Z ] 
MessageResponseOBJ
�$�$^ p
MsgRes
�$�$q w
)
�$�$w x
{
�$�$ 	
return
�$�$ 
DACConsulta
�$�$ 
.
�$�$ "
GetIdAnalisisAlertas
�$�$ 3
(
�$�$3 4
id_concurrencia
�$�$4 C
,
�$�$C D
ref
�$�$E H
MsgRes
�$�$I O
)
�$�$O P
;
�$�$P Q
}
�$�$ 	
public
�$�$ 
List
�$�$ 
<
�$�$ '
vw_analisis_caso_muestreo
�$�$ -
>
�$�$- .#
GetIdAnalisisMuestras
�$�$/ D
(
�$�$D E
Int32
�$�$E J
id_concurrencia
�$�$K Z
,
�$�$Z [
ref
�$�$\ _ 
MessageResponseOBJ
�$�$` r
MsgRes
�$�$s y
)
�$�$y z
{
�$�$ 	
return
�$�$ 
DACConsulta
�$�$ 
.
�$�$ #
GetIdAnalisisMuestras
�$�$ 4
(
�$�$4 5
id_concurrencia
�$�$5 D
,
�$�$D E
ref
�$�$F I
MsgRes
�$�$J P
)
�$�$P Q
;
�$�$Q R
}
�$�$ 	
public
�$�$ 
void
�$�$ 
InsertarUrgencias
�$�$ %
(
�$�$% &
List
�$�$& *
<
�$�$* + 
urg_cargue_base_ok
�$�$+ =
>
�$�$= >
ListUrgencias
�$�$? L
,
�$�$L M
ref
�$�$N Q 
MessageResponseOBJ
�$�$R d
MsgRes
�$�$e k
)
�$�$k l
{
�$�$ 	

DACInserta
�$�$ 
.
�$�$ 
InsertarUrgencias
�$�$ (
(
�$�$( )
ListUrgencias
�$�$) 6
,
�$�$6 7
ref
�$�$8 ;
MsgRes
�$�$< B
)
�$�$B C
;
�$�$C D
}
�$�$ 	
public
�$�$ 
void
�$�$ (
InsertarAuditoriaUrgencias
�$�$ .
(
�$�$. /%
urg_auditoria_urgencias
�$�$/ F
aud_urgencias
�$�$G T
,
�$�$T U
ref
�$�$V Y 
MessageResponseOBJ
�$�$Z l
MsgRes
�$�$m s
)
�$�$s t
{
�$�$ 	

DACInserta
�$�$ 
.
�$�$ (
InsertarAuditoriaUrgencias
�$�$ 1
(
�$�$1 2
aud_urgencias
�$�$2 ?
,
�$�$? @
ref
�$�$A D
MsgRes
�$�$E K
)
�$�$K L
;
�$�$L M
}
�$�$ 	
public
�$�$ 
List
�$�$ 
<
�$�$  
urg_cargue_base_ok
�$�$ &
>
�$�$& ' 
ConsultarUrgencias
�$�$( :
(
�$�$: ;
int
�$�$; >
?
�$�$> ?

idurgencia
�$�$@ J
,
�$�$J K
DateTime
�$�$L T
?
�$�$T U

fechadesde
�$�$V `
,
�$�$` a
DateTime
�$�$b j
?
�$�$j k

fechahasta
�$�$l v
,
�$�$v w
int
�$�$x {
?
�$�${ |
regional�$�$} �
,�$�$� �
int�$�$� �
?�$�$� �
	idusuario�$�$� �
,�$�$� �
ref�$�$� �"
MessageResponseOBJ�$�$� �
MsgRes�$�$� �
)�$�$� �
{
�$�$ 	
return
�$�$ 
DACConsulta
�$�$ 
.
�$�$  
ConsultarUrgencias
�$�$ 1
(
�$�$1 2

idurgencia
�$�$2 <
,
�$�$< =

fechadesde
�$�$> H
,
�$�$H I

fechahasta
�$�$J T
,
�$�$T U
regional
�$�$V ^
,
�$�$^ _
	idusuario
�$�$` i
,
�$�$i j
ref
�$�$k n
MsgRes
�$�$o u
)
�$�$u v
;
�$�$v w
}
�$�$ 	
public
�$�$ 
List
�$�$ 
<
�$�$ 
Ref_tipo_egreso
�$�$ #
>
�$�$# $ 
ConsultaTipoEgreso
�$�$% 7
(
�$�$7 8
ref
�$�$8 ; 
MessageResponseOBJ
�$�$< N
MsgRes
�$�$O U
)
�$�$U V
{
�$�$ 	
return
�$�$ 
DACConsulta
�$�$ 
.
�$�$  
ConsultaTipoEgreso
�$�$ 1
(
�$�$1 2
ref
�$�$2 5
MsgRes
�$�$6 <
)
�$�$< =
;
�$�$= >
}
�$�$ 	
public
�$�$ 
List
�$�$ 
<
�$�$ &
ref_urg_destino_paciente
�$�$ ,
>
�$�$, -%
ConsultaDestinoPaciente
�$�$. E
(
�$�$E F
ref
�$�$F I 
MessageResponseOBJ
�$�$J \
MsgRes
�$�$] c
)
�$�$c d
{
�$�$ 	
return
�$�$ 
DACConsulta
�$�$ 
.
�$�$ %
ConsultaDestinoPaciente
�$�$ 6
(
�$�$6 7
ref
�$�$7 :
MsgRes
�$�$; A
)
�$�$A B
;
�$�$B C
}
�$�$ 	
public
�$�$ 
List
�$�$ 
<
�$�$ %
vw_tablero_urgencias_ok
�$�$ +
>
�$�$+ ,'
ConsultaTablerUrgenciasOk
�$�$- F
(
�$�$F G
)
�$�$G H
{
�$�$ 	
return
�%�% 
DACConsulta
�%�% 
.
�%�% '
ConsultaTablerUrgenciasOk
�%�% 8
(
�%�%8 9
)
�%�%9 :
;
�%�%: ;
}
�%�% 	
public
�%�% 
Int32
�%�% $
InsertarCierreContable
�%�% +
(
�%�%+ ,
cierre_contable
�%�%, ;
obj
�%�%< ?
,
�%�%? @
ref
�%�%A D 
MessageResponseOBJ
�%�%E W
MsgRes
�%�%X ^
)
�%�%^ _
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�% $
InsertarCierreContable
�%�% 4
(
�%�%4 5
obj
�%�%5 8
,
�%�%8 9
ref
�%�%: =
MsgRes
�%�%> D
)
�%�%D E
;
�%�%E F
}
�%�% 	
public
�%�% 
List
�%�% 
<
�%�% 
cierre_contable
�%�% #
>
�%�%# $#
GetListCierreContable
�%�%% :
(
�%�%: ;
ref
�%�%; > 
MessageResponseOBJ
�%�%? Q
MsgRes
�%�%R X
)
�%�%X Y
{
�%�% 	
return
�%�% 
DACConsulta
�%�% 
.
�%�% #
GetListCierreContable
�%�% 4
(
�%�%4 5
ref
�%�%5 8
MsgRes
�%�%9 ?
)
�%�%? @
;
�%�%@ A
}
�%�% 	
public
�%�% 
bool
�%�% )
InsertarFacturasMesInterior
�%�% /
(
�%�%/ 0
List
�%�%0 4
<
�%�%4 5&
cierre_cont_mes_anterior
�%�%5 M
>
�%�%M N
List
�%�%O S
,
�%�%S T
ref
�%�%U X 
MessageResponseOBJ
�%�%Y k
MsgRes
�%�%l r
)
�%�%r s
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�% )
InsertarFacturasMesInterior
�%�% 9
(
�%�%9 :
List
�%�%: >
,
�%�%> ?
ref
�%�%@ C
MsgRes
�%�%D J
)
�%�%J K
;
�%�%K L
}
�%�% 	
public
�%�% 
bool
�%�% &
InsertarFacturasRechazos
�%�% ,
(
�%�%, -
List
�%�%- 1
<
�%�%1 2"
cierre_cont_rechazos
�%�%2 F
>
�%�%F G
List
�%�%H L
,
�%�%L M
ref
�%�%N Q 
MessageResponseOBJ
�%�%R d
MsgRes
�%�%e k
)
�%�%k l
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�% &
InsertarFacturasRechazos
�%�% 6
(
�%�%6 7
List
�%�%7 ;
,
�%�%; <
ref
�%�%= @
MsgRes
�%�%A G
)
�%�%G H
;
�%�%H I
}
�%�% 	
public
�%�% 
bool
�%�% -
InsertarFacturasPendientesProcs
�%�% 3
(
�%�%3 4
List
�%�%4 8
<
�%�%8 9,
cierre_cont_pendiente_procesar
�%�%9 W
>
�%�%W X
List
�%�%Y ]
,
�%�%] ^
ref
�%�%_ b 
MessageResponseOBJ
�%�%c u
MsgRes
�%�%v |
)
�%�%| }
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�% -
InsertarFacturasPendientesProcs
�%�% =
(
�%�%= >
List
�%�%> B
,
�%�%B C
ref
�%�%D G
MsgRes
�%�%H N
)
�%�%N O
;
�%�%O P
}
�%�% 	
public
�%�% 
bool
�%�% *
InsertarFacturasdevoluciones
�%�% 0
(
�%�%0 1
List
�%�%1 5
<
�%�%5 6&
cierre_cont_devoluciones
�%�%6 N
>
�%�%N O
List
�%�%P T
,
�%�%T U
ref
�%�%V Y 
MessageResponseOBJ
�%�%Z l
MsgRes
�%�%m s
)
�%�%s t
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�% *
InsertarFacturasdevoluciones
�%�% :
(
�%�%: ;
List
�%�%; ?
,
�%�%? @
ref
�%�%A D
MsgRes
�%�%E K
)
�%�%K L
;
�%�%L M
}
�%�% 	
public
�%�% 
bool
�%�% &
InsertarFacturascausadas
�%�% ,
(
�%�%, -
List
�%�%- 1
<
�%�%1 2"
cierre_cont_causadas
�%�%2 F
>
�%�%F G
List
�%�%H L
,
�%�%L M
ref
�%�%N Q 
MessageResponseOBJ
�%�%R d
MsgRes
�%�%e k
)
�%�%k l
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�% &
InsertarFacturascausadas
�%�% 6
(
�%�%6 7
List
�%�%7 ;
,
�%�%; <
ref
�%�%= @
MsgRes
�%�%A G
)
�%�%G H
;
�%�%H I
}
�%�% 	
public
�%�% 
bool
�%�% '
InsertarFacturasradicadas
�%�% -
(
�%�%- .
List
�%�%. 2
<
�%�%2 3#
cierre_cont_radicadas
�%�%3 H
>
�%�%H I
List
�%�%J N
,
�%�%N O
ref
�%�%P S 
MessageResponseOBJ
�%�%T f
MsgRes
�%�%g m
)
�%�%m n
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�% '
InsertarFacturasradicadas
�%�% 7
(
�%�%7 8
List
�%�%8 <
,
�%�%< =
ref
�%�%> A
MsgRes
�%�%B H
)
�%�%H I
;
�%�%I J
}
�%�% 	
public
�%�% 
cierre_contable
�%�% 
GetCierreContable
�%�% 0
(
�%�%0 1
int
�%�%1 4
idcierre
�%�%5 =
,
�%�%= >
ref
�%�%? B 
MessageResponseOBJ
�%�%C U
MsgRes
�%�%V \
)
�%�%\ ]
{
�%�% 	
return
�%�% 
DACConsulta
�%�% 
.
�%�% 
GetCierreContable
�%�% 0
(
�%�%0 1
idcierre
�%�%1 9
,
�%�%9 :
ref
�%�%; >
MsgRes
�%�%? E
)
�%�%E F
;
�%�%F G
}
�%�% 	
public
�%�% 
List
�%�% 
<
�%�% (
vw_totales_cierre_contable
�%�% .
>
�%�%. /*
GetListTotalesCierreContable
�%�%0 L
(
�%�%L M
int
�%�%M P
idcierre
�%�%Q Y
,
�%�%Y Z
ref
�%�%[ ^ 
MessageResponseOBJ
�%�%_ q
MsgRes
�%�%r x
)
�%�%x y
{
�%�% 	
return
�%�% 
DACConsulta
�%�% 
.
�%�% *
GetListTotalesCierreContable
�%�% ;
(
�%�%; <
idcierre
�%�%< D
,
�%�%D E
ref
�%�%F I
MsgRes
�%�%J P
)
�%�%P Q
;
�%�%Q R
}
�%�% 	
public
�%�% 
List
�%�% 
<
�%�%  
vw_causas_facturas
�%�% &
>
�%�%& ')
GetListCausasCierreContable
�%�%( C
(
�%�%C D
int
�%�%D G
idcierre
�%�%H P
,
�%�%P Q
ref
�%�%R U 
MessageResponseOBJ
�%�%V h
MsgRes
�%�%i o
)
�%�%o p
{
�%�% 	
return
�%�% 
DACConsulta
�%�% 
.
�%�% )
GetListCausasCierreContable
�%�% :
(
�%�%: ;
idcierre
�%�%; C
,
�%�%C D
ref
�%�%E H
MsgRes
�%�%I O
)
�%�%O P
;
�%�%P Q
}
�%�% 	
public
�%�% )
cierre_contable_cargue_base
�%�% *!
traerCierreContable
�%�%+ >
(
�%�%> ?
int
�%�%? B
?
�%�%B C
mes
�%�%D G
,
�%�%G H
int
�%�%I L
?
�%�%L M
año
�%�%N Q
,
�%�%Q R
int
�%�%S V
?
�%�%V W
regional
�%�%X `
)
�%�%` a
{
�%�% 	
return
�%�% 
DACConsulta
�%�% 
.
�%�% !
traerCierreContable
�%�% 2
(
�%�%2 3
mes
�%�%3 6
,
�%�%6 7
año
�%�%8 ;
,
�%�%; <
regional
�%�%= E
)
�%�%E F
;
�%�%F G
}
�%�% 	
public
�%�% 
List
�%�% 
<
�%�% =
/management_cierre_contable_tableroControlResult
�%�% C
>
�%�%C D&
TraerDatosCierreContable
�%�%E ]
(
�%�%] ^
)
�%�%^ _
{
�%�% 	
return
�%�% 
DACConsulta
�%�% 
.
�%�% &
TraerDatosCierreContable
�%�% 7
(
�%�%7 8
)
�%�%8 9
;
�%�%9 :
}
�%�% 	
public
�%�% 
int
�%�% $
InsertarCierreContable
�%�% )
(
�%�%) *)
cierre_contable_cargue_base
�%�%* E
obj
�%�%F I
,
�%�%I J
ref
�%�%K N 
MessageResponseOBJ
�%�%O a
MsgRes
�%�%b h
)
�%�%h i
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�% $
InsertarCierreContable
�%�% 4
(
�%�%4 5
obj
�%�%5 8
,
�%�%8 9
ref
�%�%: =
MsgRes
�%�%> D
)
�%�%D E
;
�%�%E F
}
�%�% 	
public
�%�% 
void
�%�% +
InsertarCierreContableDetalle
�%�% 1
(
�%�%1 2
List
�%�%2 6
<
�%�%6 7,
cierre_contable_cargue_detalle
�%�%7 U
>
�%�%U V
dtll
�%�%W [
,
�%�%[ \
ref
�%�%] ` 
MessageResponseOBJ
�%�%a s
MsgRes
�%�%t z
)
�%�%z {
{
�%�% 	

DACInserta
�%�% 
.
�%�% +
InsertarCierreContableDetalle
�%�% 4
(
�%�%4 5
dtll
�%�%5 9
,
�%�%9 :
ref
�%�%; >
MsgRes
�%�%? E
)
�%�%E F
;
�%�%F G
}
�%�% 	
public
�%�% 
int
�%�% *
EliminarCargueCierreContable
�%�% /
(
�%�%/ 0
int
�%�%0 3
idCargue
�%�%4 <
)
�%�%< =
{
�%�% 	
return
�%�% 

DACElimina
�%�% 
.
�%�% *
EliminarCargueCierreContable
�%�% :
(
�%�%: ;
idCargue
�%�%; C
)
�%�%C D
;
�%�%D E
}
�%�% 	
public
�%�% 
int
�%�% /
!InsertarLogEliminarCierreContable
�%�% 4
(
�%�%4 54
&log_cierreContable_eliminarConsolidado
�%�%5 [
obj
�%�%\ _
)
�%�%_ `
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�% /
!InsertarLogEliminarCierreContable
�%�% ?
(
�%�%? @
obj
�%�%@ C
)
�%�%C D
;
�%�%D E
}
�%�% 	
public
�%�% 
List
�%�% 
<
�%�% 
ref_cohortes
�%�%  
>
�%�%  !
Get_refCohortes
�%�%" 1
(
�%�%1 2
)
�%�%2 3
{
�%�% 	
return
�%�% 
DACConsulta
�%�% 
.
�%�% 
Get_refCohortes
�%�% .
(
�%�%. /
)
�%�%/ 0
;
�%�%0 1
}
�%�% 	
public
�%�% 
List
�%�% 
<
�%�% 
ref_cohortes
�%�%  
>
�%�%  !"
Get_refCohortesSindh
�%�%" 6
(
�%�%6 7
)
�%�%7 8
{
�%�% 	
return
�%�% 
DACConsulta
�%�% 
.
�%�% "
Get_refCohortesSindh
�%�% 3
(
�%�%3 4
)
�%�%4 5
;
�%�%5 6
}
�%�% 	
public
�%�% 
List
�%�% 
<
�%�% *
ref_adh_modalidad_prestacion
�%�% 0
>
�%�%0 11
#Get_adherencia_modalidad_prestacion
�%�%2 U
(
�%�%U V
)
�%�%V W
{
�%�% 	
return
�%�% 
DACConsulta
�%�% 
.
�%�% 1
#Get_adherencia_modalidad_prestacion
�%�% B
(
�%�%B C
)
�%�%C D
;
�%�%D E
}
�%�% 	
public
�%�% 
int
�%�% !
InsertCohortesDatos
�%�% &
(
�%�%& '"
cohortes_cargue_base
�%�%' ;
obj
�%�%< ?
,
�%�%? @
List
�%�%A E
<
�%�%E F(
cohortes_detalle_cargue_OK
�%�%F `
>
�%�%` a
lista
�%�%b g
,
�%�%g h
ref
�%�%i l 
MessageResponseOBJ
�%�%m 
MsgRes�%�%� �
)�%�%� �
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�% !
InsertCohortesDatos
�%�% 1
(
�%�%1 2
obj
�%�%2 5
,
�%�%5 6
lista
�%�%7 <
,
�%�%< =
ref
�%�%> A
MsgRes
�%�%B H
)
�%�%H I
;
�%�%I J
}
�%�% 	
public
�%�% 
int
�%�%  
InsertCohortesEPOC
�%�% %
(
�%�%% &"
cohortes_cargue_base
�%�%& :
obj
�%�%; >
,
�%�%> ?
List
�%�%@ D
<
�%�%D E(
cohortes_detalle_cargue_OK
�%�%E _
>
�%�%_ `
	listaepoc
�%�%a j
,
�%�%j k
ref
�%�%l o!
MessageResponseOBJ�%�%p �
MsgRes�%�%� �
)�%�%� �
{
�%�% 	
return
�%�% 

DACInserta
�%�% 
.
�%�%  
InsertCohortesEPOC
�%�% 0
(
�%�%0 1
obj
�%�%1 4
,
�%�%4 5
	listaepoc
�%�%6 ?
,
�%�%? @
ref
�%�%A D
MsgRes
�%�%E K
)
�%�%K L
;
�%�%L M
}
�%�% 	
public
�%�% 
void
�%�% 
InsertCohortesPAD
�%�% %
(
�%�%% &"
cohortes_cargue_base
�%�%& :
cargue
�%�%; A
,
�%�%A B
List
�%�%C G
<
�%�%G H(
cohortes_detalle_cargue_OK
�%�%H b
>
�%�%b c
listaPAD
�%�%d l
,
�%�%l m
ref
�%�%n q!
MessageResponseOBJ�%�%r �
MsgRes�%�%� �
)�%�%� �
{
�%�% 	

DACInserta
�%�% 
.
�%�% 
InsertCohortesPAD
�%�% (
(
�%�%( )
cargue
�%�%) /
,
�%�%/ 0
listaPAD
�%�%1 9
,
�%�%9 :
ref
�%�%; >
MsgRes
�%�%? E
)
�%�%E F
;
�%�%F G
}
�%�% 	
public
�%�% 
void
�%�% 
InsertCohortesRCV
�%�% %
(
�%�%% &"
cohortes_cargue_base
�%�%& :
cargue
�%�%; A
,
�%�%A B
List
�%�%C G
<
�%�%G H(
cohortes_detalle_cargue_OK
�%�%H b
>
�%�%b c
listaRCV
�%�%d l
,
�%�%l m
ref
�%�%n q!
MessageResponseOBJ�%�%r �
MsgRes�%�%� �
)�%�%� �
{
�%�% 	

DACInserta
�%�% 
.
�%�% 
InsertCohortesRCV
�%�% (
(
�%�%( )
cargue
�%�%) /
,
�%�%/ 0
listaRCV
�%�%1 9
,
�%�%9 :
ref
�%�%; >
MsgRes
�%�%? E
)
�%�%E F
;
�%�%F G
}
�&�& 	
public
�&�& 
void
�&�& %
InsertCohortesGESTANTES
�&�& +
(
�&�&+ ,"
cohortes_cargue_base
�&�&, @
cargue
�&�&A G
,
�&�&G H
List
�&�&I M
<
�&�&M N(
cohortes_detalle_cargue_OK
�&�&N h
>
�&�&h i
listaGestantes
�&�&j x
,
�&�&x y
ref
�&�&z }!
MessageResponseOBJ�&�&~ �
MsgRes�&�&� �
)�&�&� �
{
�&�& 	

DACInserta
�&�& 
.
�&�& %
InsertCohortesGESTANTES
�&�& .
(
�&�&. /
cargue
�&�&/ 5
,
�&�&5 6
listaGestantes
�&�&7 E
,
�&�&E F
ref
�&�&G J
MsgRes
�&�&K Q
)
�&�&Q R
;
�&�&R S
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& 3
%management_cohortesBeneficiarioResult
�&�& 9
>
�&�&9 :%
GetCohortesBeneficiario
�&�&; R
(
�&�&R S
string
�&�&S Y
idDoc
�&�&Z _
)
�&�&_ `
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& %
GetCohortesBeneficiario
�&�& 6
(
�&�&6 7
idDoc
�&�&7 <
)
�&�&< =
;
�&�&= >
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& ?
1management_HospitalizacionEvitable_cohortesResult
�&�& E
>
�&�&E F0
"HospitalizacionPrevenible_cohortes
�&�&G i
(
�&�&i j
string
�&�&j p
idDoc
�&�&q v
)
�&�&v w
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& 0
"HospitalizacionPrevenible_cohortes
�&�& A
(
�&�&A B
idDoc
�&�&B G
)
�&�&G H
;
�&�&H I
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& @
2management_hospitalizacionPrevenible_TableroResult
�&�& F
>
�&�&F G*
GetHospitalizacionPrevenible
�&�&H d
(
�&�&d e
)
�&�&e f
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& *
GetHospitalizacionPrevenible
�&�& ;
(
�&�&; <
)
�&�&< =
;
�&�&= >
}
�&�& 	
public
�&�& @
2management_hospitalizacionPrevenible_detalleResult
�&�& A1
#GetHospitalizacionPrevenibleDetalle
�&�&B e
(
�&�&e f
int
�&�&f i
idHE
�&�&j n
)
�&�&n o
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& 1
#GetHospitalizacionPrevenibleDetalle
�&�& B
(
�&�&B C
idHE
�&�&C G
)
�&�&G H
;
�&�&H I
}
�&�& 	
public
�&�& 
int
�&�& /
!InsertarHospitalizacionPrevenible
�&�& 4
(
�&�&4 5-
ecop_hospitalizacion_prevenible
�&�&5 T
obj
�&�&U X
)
�&�&X Y
{
�&�& 	
return
�&�& 

DACInserta
�&�& 
.
�&�& /
!InsertarHospitalizacionPrevenible
�&�& ?
(
�&�&? @
obj
�&�&@ C
)
�&�&C D
;
�&�&D E
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& (
ecop_directorioPPE_correos
�&�& .
>
�&�&. /+
GetEcop_DirectorioPPE_Correos
�&�&0 M
(
�&�&M N
string
�&�&N T
regional
�&�&U ]
)
�&�&] ^
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& +
GetEcop_DirectorioPPE_Correos
�&�& <
(
�&�&< =
regional
�&�&= E
)
�&�&E F
;
�&�&F G
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& (
ecop_directorioPPE_correos
�&�& .
>
�&�&. /4
&GetEcop_DirectorioPPE_CorreosDocumento
�&�&0 V
(
�&�&V W
string
�&�&W ]
	documento
�&�&^ g
)
�&�&g h
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& 4
&GetEcop_DirectorioPPE_CorreosDocumento
�&�& E
(
�&�&E F
	documento
�&�&F O
)
�&�&O P
;
�&�&P Q
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& #
ref_adh_tipo_criterio
�&�& )
>
�&�&) *"
get_ref_TipoCriterio
�&�&+ ?
(
�&�&? @
)
�&�&@ A
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& "
get_ref_TipoCriterio
�&�& 3
(
�&�&3 4
)
�&�&4 5
;
�&�&5 6
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& (
ref_adh_grupo_tipocriterio
�&�& .
>
�&�&. /'
get_ref_grupoTipoCriterio
�&�&0 I
(
�&�&I J
)
�&�&J K
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& '
get_ref_grupoTipoCriterio
�&�& 8
(
�&�&8 9
)
�&�&9 :
;
�&�&: ;
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& 
adh_tipocriterio
�&�& $
>
�&�&$ %"
get_adh_tipocriterio
�&�&& :
(
�&�&: ;
int
�&�&; >
idadherencia
�&�&? K
)
�&�&K L
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& "
get_adh_tipocriterio
�&�& 3
(
�&�&3 4
idadherencia
�&�&4 @
)
�&�&@ A
;
�&�&A B
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& (
ref_adh_grupo_tipocriterio
�&�& .
>
�&�&. /+
get_ref_adh_grupotipocriterio
�&�&0 M
(
�&�&M N
)
�&�&N O
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& +
get_ref_adh_grupotipocriterio
�&�& <
(
�&�&< =
)
�&�&= >
;
�&�&> ?
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& #
ref_adh_tipo_criterio
�&�& )
>
�&�&) **
get_ref_TipoCriterio_cohorte
�&�&+ G
(
�&�&G H
int
�&�&H K
idtipocohorte
�&�&L Y
)
�&�&Y Z
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& *
get_ref_TipoCriterio_cohorte
�&�& ;
(
�&�&; <
idtipocohorte
�&�&< I
)
�&�&I J
;
�&�&J K
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& 
adh_criterio
�&�&  
>
�&�&  !'
getcriteriosbytipocohorte
�&�&" ;
(
�&�&; <
int
�&�&< ?
tipocohorte
�&�&@ K
)
�&�&K L
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& '
getcriteriosbytipocohorte
�&�& 8
(
�&�&8 9
tipocohorte
�&�&9 D
)
�&�&D E
;
�&�&E F
}
�&�& 	
public
�&�& 
void
�&�& "
InsertarTipoCriterio
�&�& (
(
�&�&( )#
ref_adh_tipo_criterio
�&�&) >
obj
�&�&? B
,
�&�&B C
ref
�&�&D G 
MessageResponseOBJ
�&�&H Z
MsgRes
�&�&[ a
)
�&�&a b
{
�&�& 	

DACInserta
�&�& 
.
�&�& "
InsertarTipoCriterio
�&�& +
(
�&�&+ ,
obj
�&�&, /
,
�&�&/ 0
ref
�&�&1 4
MsgRes
�&�&5 ;
)
�&�&; <
;
�&�&< =
}
�&�& 	
public
�&�& 
void
�&�& 
InsertarCriterio
�&�& $
(
�&�&$ %
adh_criterio
�&�&% 1
criterio
�&�&2 :
,
�&�&: ;
ref
�&�&< ? 
MessageResponseOBJ
�&�&@ R
MsgRes
�&�&S Y
)
�&�&Y Z
{
�&�& 	

DACInserta
�&�& 
.
�&�& 
InsertarCriterio
�&�& '
(
�&�&' (
criterio
�&�&( 0
,
�&�&0 1
ref
�&�&2 5
MsgRes
�&�&6 <
)
�&�&< =
;
�&�&= >
}
�&�& 	
public
�&�& 
void
�&�& $
ActualizarTipoCriterio
�&�& *
(
�&�&* +#
ref_adh_tipo_criterio
�&�&+ @
obj
�&�&A D
,
�&�&D E
ref
�&�&F I 
MessageResponseOBJ
�&�&J \
MsgRes
�&�&] c
)
�&�&c d
{
�&�& 	
DACActualiza
�&�& 
.
�&�& $
ActualizarTipoCriterio
�&�& /
(
�&�&/ 0
obj
�&�&0 3
,
�&�&3 4
ref
�&�&5 8
MsgRes
�&�&9 ?
)
�&�&? @
;
�&�&@ A
}
�&�& 	
public
�&�& 
void
�&�&  
ActualizarCriterio
�&�& &
(
�&�&& '
adh_criterio
�&�&' 3
criterio
�&�&4 <
,
�&�&< =
ref
�&�&> A 
MessageResponseOBJ
�&�&B T
MsgRes
�&�&U [
)
�&�&[ \
{
�&�& 	
DACActualiza
�&�& 
.
�&�&  
ActualizarCriterio
�&�& +
(
�&�&+ ,
criterio
�&�&, 4
,
�&�&4 5
ref
�&�&6 9
MsgRes
�&�&: @
)
�&�&@ A
;
�&�&A B
}
�&�& 	
public
�&�& 
void
�&�& %
EliminarCriterioCohorte
�&�& +
(
�&�&+ ,
int
�&�&, /

idcriterio
�&�&0 :
,
�&�&: ;
ref
�&�&< ? 
MessageResponseOBJ
�&�&@ R
MsgRes
�&�&S Y
)
�&�&Y Z
{
�&�& 	

DACElimina
�&�& 
.
�&�& %
EliminarCriterioCohorte
�&�& .
(
�&�&. /

idcriterio
�&�&/ 9
,
�&�&9 :
ref
�&�&; >
MsgRes
�&�&? E
)
�&�&E F
;
�&�&F G
}
�&�& 	
public
�&�& 
void
�&�& "
EliminarTipoCriterio
�&�& (
(
�&�&( )
int
�&�&) ,
idtipocriterio
�&�&- ;
,
�&�&; <
ref
�&�&= @ 
MessageResponseOBJ
�&�&A S
MsgRes
�&�&T Z
)
�&�&Z [
{
�&�& 	

DACElimina
�&�& 
.
�&�& "
EliminarTipoCriterio
�&�& +
(
�&�&+ ,
idtipocriterio
�&�&, :
,
�&�&: ;
ref
�&�&< ?
MsgRes
�&�&@ F
)
�&�&F G
;
�&�&G H
}
�&�& 	
public
�&�& 
adh_criterio
�&�& #
ConsultarCriterioById
�&�& 1
(
�&�&1 2
int
�&�&2 5

idcriterio
�&�&6 @
)
�&�&@ A
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& #
ConsultarCriterioById
�&�& 4
(
�&�&4 5

idcriterio
�&�&5 ?
)
�&�&? @
;
�&�&@ A
}
�&�& 	
public
�&�& 
int
�&�&  
InsertarResultados
�&�& %
(
�&�&% &
adh_resultados
�&�&& 4

resultados
�&�&5 ?
,
�&�&? @
List
�&�&A E
<
�&�&E F
string
�&�&F L
>
�&�&L M
resultadoshc1
�&�&N [
,
�&�&[ \
ref
�&�&] ` 
MessageResponseOBJ
�&�&a s
Msg
�&�&t w
)
�&�&w x
{
�&�& 	
return
�&�& 

DACInserta
�&�& 
.
�&�&  
InsertarResultados
�&�& 0
(
�&�&0 1

resultados
�&�&1 ;
,
�&�&; <
resultadoshc1
�&�&= J
,
�&�&J K
ref
�&�&L O
Msg
�&�&P S
)
�&�&S T
;
�&�&T U
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& 
adh_resultados
�&�& "
>
�&�&" #$
GetResultadosPrestador
�&�&$ :
(
�&�&: ;
int
�&�&; >
idprestador
�&�&? J
,
�&�&J K
int
�&�&L O
profesional
�&�&P [
,
�&�&[ \
int
�&�&] `
mes
�&�&a d
,
�&�&d e
int
�&�&f i
año
�&�&j m
)
�&�&m n
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& &
GetResultadosPrestadorv2
�&�& 7
(
�&�&7 8
idprestador
�&�&8 C
,
�&�&C D
profesional
�&�&E P
,
�&�&P Q
mes
�&�&R U
,
�&�&U V
año
�&�&W Z
)
�&�&Z [
;
�&�&[ \
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& (
vw_rptResultadosAdherencia
�&�& .
>
�&�&. /$
GetResultadosPrestador
�&�&0 F
(
�&�&F G
Int32
�&�&G L
?
�&�&L M
idresultados
�&�&N Z
)
�&�&Z [
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& $
GetResultadosPrestador
�&�& 5
(
�&�&5 6
idresultados
�&�&6 B
)
�&�&B C
;
�&�&C D
}
�&�& 	
public
�&�& 
List
�&�& 
<
�&�& 8
*managmentReporteResultadosAdherenciaResult
�&�& >
>
�&�&> ?%
GetResultadosAdherencia
�&�&@ W
(
�&�&W X
Int32
�&�&X ]
idresultados
�&�&^ j
)
�&�&j k
{
�&�& 	
return
�&�& 
DACConsulta
�&�& 
.
�&�& %
GetResultadosAdherencia
�&�& 6
(
�&�&6 7
idresultados
�&�&7 C
)
�&�&C D
;
�&�&D E
}
�&�& 	
public
�'�' 
List
�'�' 
<
�'�' 9
+managmentReporteResultadosAdherencia2Result
�'�' ?
>
�'�'? @&
GetResultadosAdherencia2
�'�'A Y
(
�'�'Y Z
Int32
�'�'Z _
idresultados
�'�'` l
)
�'�'l m
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' &
GetResultadosAdherencia2
�'�' 7
(
�'�'7 8
idresultados
�'�'8 D
)
�'�'D E
;
�'�'E F
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' <
.Management_adh_cantidad_resultados_grupoResult
�'�' B
>
�'�'B C*
GetResultadosGrupoAdherencia
�'�'D `
(
�'�'` a
Int32
�'�'a f
idresultados
�'�'g s
)
�'�'s t
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' *
GetResultadosGrupoAdherencia
�'�' ;
(
�'�'; <
idresultados
�'�'< H
)
�'�'H I
;
�'�'I J
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' 
Ref_ips_cuentas
�'�' #
>
�'�'# $
getprestadores
�'�'% 3
(
�'�'3 4
)
�'�'4 5
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' 
getprestadores
�'�' -
(
�'�'- .
)
�'�'. /
;
�'�'/ 0
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' 5
'management_prestadoresHomologadosResult
�'�' ;
>
�'�'; <'
getprestadoresHomologados
�'�'= V
(
�'�'V W
)
�'�'W X
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' '
getprestadoresHomologados
�'�' 8
(
�'�'8 9
)
�'�'9 :
;
�'�': ;
}
�'�' 	
public
�'�' 
void
�'�' !
InsertarTipoCohorte
�'�' '
(
�'�'' (
ref_cohortes
�'�'( 4
obj
�'�'5 8
)
�'�'8 9
{
�'�' 	

DACInserta
�'�' 
.
�'�' !
InsertarTipoCohorte
�'�' *
(
�'�'* +
obj
�'�'+ .
)
�'�'. /
;
�'�'/ 0
}
�'�' 	
public
�'�' 
void
�'�' #
ActualizarTipoCohorte
�'�' )
(
�'�') *
ref_cohortes
�'�'* 6
obj
�'�'7 :
)
�'�': ;
{
�'�' 	
DACActualiza
�'�' 
.
�'�' #
ActualizarTipoCohorte
�'�' .
(
�'�'. /
obj
�'�'/ 2
)
�'�'2 3
;
�'�'3 4
}
�'�' 	
public
�'�' 
ref_cohortes
�'�'  
getTipoCohorteById
�'�' .
(
�'�'. /
int
�'�'/ 2
idtipocohorte
�'�'3 @
)
�'�'@ A
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�'  
getTipoCohorteById
�'�' 1
(
�'�'1 2
idtipocohorte
�'�'2 ?
)
�'�'? @
;
�'�'@ A
}
�'�' 	
public
�'�' 
void
�'�' $
InsertarAdminCriterios
�'�' *
(
�'�'* +
int
�'�'+ .
tipoadherencia
�'�'/ =
,
�'�'= >
List
�'�'? C
<
�'�'C D
int
�'�'D G
>
�'�'G H
seleccionados
�'�'I V
,
�'�'V W
List
�'�'X \
<
�'�'\ ]
int
�'�'] `
>
�'�'` a
seleccionados2
�'�'b p
,
�'�'p q
ref
�'�'r u!
MessageResponseOBJ�'�'v �
MsgRes�'�'� �
)�'�'� �
{
�'�' 	

DACInserta
�'�' 
.
�'�' $
InsertarAdminCriterios
�'�' -
(
�'�'- .
tipoadherencia
�'�'. <
,
�'�'< =
seleccionados
�'�'> K
,
�'�'K L
seleccionados2
�'�'M [
,
�'�'[ \
ref
�'�'] `
MsgRes
�'�'a g
)
�'�'g h
;
�'�'h i
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' !
ref_adherencia_unis
�'�' '
>
�'�'' (
GetUnisByRegional
�'�') :
(
�'�': ;
int
�'�'; >

idregional
�'�'? I
)
�'�'I J
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' 
GetUnisByRegional
�'�' 0
(
�'�'0 1

idregional
�'�'1 ;
)
�'�'; <
;
�'�'< =
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' #
ref_adherencia_ciudad
�'�' )
>
�'�') *
GetciudadByunis
�'�'+ :
(
�'�': ;
int
�'�'; >
idunis
�'�'? E
)
�'�'E F
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' 
GetciudadByunis
�'�' .
(
�'�'. /
idunis
�'�'/ 5
)
�'�'5 6
;
�'�'6 7
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' -
ref_adherencia_prestador_ciudad
�'�' 3
>
�'�'3 4$
GetPrestadoresByciudad
�'�'5 K
(
�'�'K L
int
�'�'L O
idciudad
�'�'P X
)
�'�'X Y
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' $
GetPrestadoresByciudad
�'�' 5
(
�'�'5 6
idciudad
�'�'6 >
)
�'�'> ?
;
�'�'? @
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' 2
$ref_adherencia_profesional_prestador
�'�' 8
>
�'�'8 9)
GetProfesionalesByprestador
�'�': U
(
�'�'U V
int
�'�'V Y
idprestador
�'�'Z e
)
�'�'e f
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' )
GetProfesionalesByprestador
�'�' :
(
�'�': ;
idprestador
�'�'; F
)
�'�'F G
;
�'�'G H
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' (
Ref_odont_list_check_ortod
�'�' .
>
�'�'. /
getcheckOrtod
�'�'0 =
(
�'�'= >
)
�'�'> ?
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' 
getcheckOrtod
�'�' ,
(
�'�', -
)
�'�'- .
;
�'�'. /
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' )
Ref_odont_check_porcentajes
�'�' /
>
�'�'/ 0 
getcheckPorcentaje
�'�'1 C
(
�'�'C D
)
�'�'D E
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�'  
getcheckPorcentaje
�'�' 1
(
�'�'1 2
)
�'�'2 3
;
�'�'3 4
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' '
Ref_odont_tipo_endodoncia
�'�' -
>
�'�'- .#
getListTipoEndodoncia
�'�'/ D
(
�'�'D E
)
�'�'E F
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' #
getListTipoEndodoncia
�'�' 4
(
�'�'4 5
)
�'�'5 6
;
�'�'6 7
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' ,
Ref_odont_parametros_auditados
�'�' 2
>
�'�'2 3(
getListParametrosAuditados
�'�'4 N
(
�'�'N O
)
�'�'O P
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' (
getListParametrosAuditados
�'�' 9
(
�'�'9 :
)
�'�': ;
;
�'�'; <
}
�'�' 	
public
�'�' 
Int32
�'�' %
InsertarOdontOrtodoncia
�'�' ,
(
�'�', -*
odont_tratamiento_ortodoncia
�'�'- I
OBJ
�'�'J M
,
�'�'M N
ref
�'�'O R 
MessageResponseOBJ
�'�'S e
MsgRes
�'�'f l
)
�'�'l m
{
�'�' 	
return
�'�' 

DACInserta
�'�' 
.
�'�' %
InsertarOdontOrtodoncia
�'�' 5
(
�'�'5 6
OBJ
�'�'6 9
,
�'�'9 :
ref
�'�'; >
MsgRes
�'�'? E
)
�'�'E F
;
�'�'F G
}
�'�' 	
public
�'�' 
Int32
�'�' ,
InsertarOdontOrtodonciaDetalle
�'�' 3
(
�'�'3 42
$odont_tratamiento_ortodoncia_detalle
�'�'4 X
OBJ
�'�'Y \
,
�'�'\ ]
ref
�'�'^ a 
MessageResponseOBJ
�'�'b t
MsgRes
�'�'u {
)
�'�'{ |
{
�'�' 	
return
�'�' 

DACInserta
�'�' 
.
�'�' ,
InsertarOdontOrtodonciaDetalle
�'�' <
(
�'�'< =
OBJ
�'�'= @
,
�'�'@ A
ref
�'�'B E
MsgRes
�'�'F L
)
�'�'L M
;
�'�'M N
}
�'�' 	
public
�'�' 
Int32
�'�' %
InsertarOdontEndodoncia
�'�' ,
(
�'�', -*
odont_tratamiento_endodoncia
�'�'- I
OBJ
�'�'J M
,
�'�'M N
ref
�'�'O R 
MessageResponseOBJ
�'�'S e
MsgRes
�'�'f l
)
�'�'l m
{
�'�' 	
return
�'�' 

DACInserta
�'�' 
.
�'�' %
InsertarOdontEndodoncia
�'�' 5
(
�'�'5 6
OBJ
�'�'6 9
,
�'�'9 :
ref
�'�'; >
MsgRes
�'�'? E
)
�'�'E F
;
�'�'F G
}
�'�' 	
public
�'�' 
Int32
�'�' 
InsertarOdontFija
�'�' &
(
�'�'& '5
'odont_rehabilitacion_oral_protesis_fija
�'�'' N
OBJ
�'�'O R
,
�'�'R S
ref
�'�'T W 
MessageResponseOBJ
�'�'X j
MsgRes
�'�'k q
)
�'�'q r
{
�'�' 	
return
�'�' 

DACInserta
�'�' 
.
�'�' 
InsertarOdontFija
�'�' /
(
�'�'/ 0
OBJ
�'�'0 3
,
�'�'3 4
ref
�'�'5 8
MsgRes
�'�'9 ?
)
�'�'? @
;
�'�'@ A
}
�'�' 	
public
�'�' 
void
�'�' #
InsertarOdontFijaDtll
�'�' )
(
�'�') *
List
�'�'* .
<
�'�'. /:
,odont_rehabilitacion_oral_protesis_fija_dtll
�'�'/ [
>
�'�'[ \
OBJ
�'�'] `
,
�'�'` a
ref
�'�'b e 
MessageResponseOBJ
�'�'f x
MsgRes
�'�'y 
)�'�' �
{
�'�' 	

DACInserta
�'�' 
.
�'�' #
InsertarOdontFijaDtll
�'�' ,
(
�'�', -
OBJ
�'�'- 0
,
�'�'0 1
ref
�'�'2 5
MsgRes
�'�'6 <
)
�'�'< =
;
�'�'= >
}
�'�' 	
public
�'�' 
Int32
�'�' $
InsertarOdontRemovible
�'�' +
(
�'�'+ ,;
-odont_rehabilitacion_oral_protesis_removibles
�'�', Y
OBJ
�'�'Z ]
,
�'�'] ^
ref
�'�'_ b 
MessageResponseOBJ
�'�'c u
MsgRes
�'�'v |
)
�'�'| }
{
�'�' 	
return
�'�' 

DACInserta
�'�' 
.
�'�' $
InsertarOdontRemovible
�'�' 4
(
�'�'4 5
OBJ
�'�'5 8
,
�'�'8 9
ref
�'�': =
MsgRes
�'�'> D
)
�'�'D E
;
�'�'E F
}
�'�' 	
public
�'�' 
List
�'�' 
<
�'�' (
vw_odont_ortodoncia_report
�'�' .
>
�'�'. /)
ConsultaIdReporteOrtodoncia
�'�'0 K
(
�'�'K L
Int32
�'�'L Q'
id_tratamiento_ortodoncia
�'�'R k
,
�'�'k l
ref
�'�'m p!
MessageResponseOBJ�'�'q �
MsgRes�'�'� �
)�'�'� �
{
�'�' 	
return
�'�' 
DACConsulta
�'�' 
.
�'�' )
ConsultaIdReporteOrtodoncia
�'�' :
(
�'�': ;'
id_tratamiento_ortodoncia
�'�'; T
,
�'�'T U
ref
�'�'V Y
MsgRes
�'�'Z `
)
�'�'` a
;
�'�'a b
}
�'�' 	
public
�'�' 
Int32
�'�' (
InsertarOdontRemovibledtll
�'�' /
(
�'�'/ 0@
2odont_rehabilitacion_oral_protesis_removibles_dtll
�'�'0 b
OBJ
�'�'c f
,
�'�'f g
ref
�'�'h k 
MessageResponseOBJ
�'�'l ~
MsgRes�'�' �
)�'�'� �
{
�'�' 	
return
�'�' 

DACInserta
�'�' 
.
�'�' (
InsertarOdontRemovibledtll
�'�' 8
(
�'�'8 9
OBJ
�'�'9 <
,
�'�'< =
ref
�'�'> A
MsgRes
�'�'B H
)
�'�'H I
;
�'�'I J
}
�'�' 	
public
�'�' 
Int32
�'�' )
InsertarOdontEndodonciadtll
�'�' 0
(
�'�'0 1/
!odont_tratamiento_endodoncia_dtll
�'�'1 R
OBJ
�'�'S V
,
�'�'V W
ref
�'�'X [ 
MessageResponseOBJ
�'�'\ n
MsgRes
�'�'o u
)
�'�'u v
{
�'�' 	
return
�'�' 

DACInserta
�'�' 
.
�'�' )
InsertarOdontEndodonciadtll
�'�' 9
(
�'�'9 :
OBJ
�'�': =
,
�'�'= >
ref
�'�'? B
MsgRes
�'�'C I
)
�'�'I J
;
�'�'J K
}
�'�' 	
public
�(�( 
List
�(�( 
<
�(�( '
vw_odont_removible_report
�(�( -
>
�(�(- .(
ConsultaIdReporteRemovible
�(�(/ I
(
�(�(I J
Int32
�(�(J O8
*id_rehabilitacion_oral_protesis_removibles
�(�(P z
,
�(�(z {
ref
�(�(| "
MessageResponseOBJ�(�(� �
MsgRes�(�(� �
)�(�(� �
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( (
ConsultaIdReporteRemovible
�(�( 9
(
�(�(9 :8
*id_rehabilitacion_oral_protesis_removibles
�(�(: d
,
�(�(d e
ref
�(�(f i
MsgRes
�(�(j p
)
�(�(p q
;
�(�(q r
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( (
vw_odont_endodoncia_report
�(�( .
>
�(�(. /)
ConsultaIdReporteEndodoncia
�(�(0 K
(
�(�(K L
Int32
�(�(L Q'
id_tratamiento_endodoncia
�(�(R k
,
�(�(k l
ref
�(�(m p!
MessageResponseOBJ�(�(q �
MsgRes�(�(� �
)�(�(� �
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( )
ConsultaIdReporteEndodoncia
�(�( :
(
�(�(: ;'
id_tratamiento_endodoncia
�(�(; T
,
�(�(T U
ref
�(�(V Y
MsgRes
�(�(Z `
)
�(�(` a
;
�(�(a b
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( "
vw_odont_fija_report
�(�( (
>
�(�(( )+
ConsultaIdReporteProtesisFija
�(�(* G
(
�(�(G H
Int32
�(�(H M*
id_tratamiento_Protesis_Fija
�(�(N j
,
�(�(j k
ref
�(�(l o!
MessageResponseOBJ�(�(p �
MsgRes�(�(� �
)�(�(� �
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( +
ConsultaIdReporteProtesisFija
�(�( <
(
�(�(< =*
id_tratamiento_Protesis_Fija
�(�(= Y
,
�(�(Y Z
ref
�(�([ ^
MsgRes
�(�(_ e
)
�(�(e f
;
�(�(f g
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( :
,odont_rehabilitacion_oral_protesis_fija_dtll
�(�( @
>
�(�(@ A/
!ConsultaIdReporteProtesisFijaDtll
�(�(B c
(
�(�(c d
Int32
�(�(d i+
id_tratamiento_Protesis_Fija�(�(j �
,�(�(� �
ref�(�(� �"
MessageResponseOBJ�(�(� �
MsgRes�(�(� �
)�(�(� �
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( /
!ConsultaIdReporteProtesisFijaDtll
�(�( @
(
�(�(@ A*
id_tratamiento_Protesis_Fija
�(�(A ]
,
�(�(] ^
ref
�(�(_ b
MsgRes
�(�(c i
)
�(�(i j
;
�(�(j k
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( )
vw_odont_porcentaje_d1_fija
�(�( /
>
�(�(/ 0#
Getporcentaje_d1_fija
�(�(1 F
(
�(�(F G
Int32
�(�(G L
id_protesis_fija
�(�(M ]
,
�(�(] ^
ref
�(�(_ b 
MessageResponseOBJ
�(�(c u
MsgRes
�(�(v |
)
�(�(| }
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( #
Getporcentaje_d1_fija
�(�( 4
(
�(�(4 5
id_protesis_fija
�(�(5 E
,
�(�(E F
ref
�(�(G J
MsgRes
�(�(K Q
)
�(�(Q R
;
�(�(R S
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( )
vw_odont_porcentaje_d2_fija
�(�( /
>
�(�(/ 0#
Getporcentaje_d2_fija
�(�(1 F
(
�(�(F G
Int32
�(�(G L
id_protesis_fija
�(�(M ]
,
�(�(] ^
ref
�(�(_ b 
MessageResponseOBJ
�(�(c u
MsgRes
�(�(v |
)
�(�(| }
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( #
Getporcentaje_d2_fija
�(�( 4
(
�(�(4 5
id_protesis_fija
�(�(5 E
,
�(�(E F
ref
�(�(G J
MsgRes
�(�(K Q
)
�(�(Q R
;
�(�(R S
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( -
vw_odont_reporte_removible_dtll
�(�( 3
>
�(�(3 44
&ConsultaIdReporteProtesisRemovibleDtll
�(�(5 [
(
�(�([ \
Int32
�(�(\ a9
*id_rehabilitacion_oral_protesis_removibles�(�(b �
,�(�(� �
ref�(�(� �"
MessageResponseOBJ�(�(� �
MsgRes�(�(� �
)�(�(� �
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( 4
&ConsultaIdReporteProtesisRemovibleDtll
�(�( E
(
�(�(E F8
*id_rehabilitacion_oral_protesis_removibles
�(�(F p
,
�(�(p q
ref
�(�(r u
MsgRes
�(�(v |
)
�(�(| }
;
�(�(} ~
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( *
vw_odont_tableros_ortodoncia
�(�( 0
>
�(�(0 1*
ConsultaListadoTTOsOrodoncia
�(�(2 N
(
�(�(N O
ref
�(�(O R 
MessageResponseOBJ
�(�(S e
MsgRes
�(�(f l
)
�(�(l m
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( *
ConsultaListadoTTOsOrodoncia
�(�( ;
(
�(�(; <
ref
�(�(< ?
MsgRes
�(�(@ F
)
�(�(F G
;
�(�(G H
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( /
!vw_odont_tableros_ortodoncia_prof
�(�( 5
>
�(�(5 6.
 ConsultaListadoTTOsOrodonciaProf
�(�(7 W
(
�(�(W X
ref
�(�(X [ 
MessageResponseOBJ
�(�(\ n
MsgRes
�(�(o u
)
�(�(u v
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( .
 ConsultaListadoTTOsOrodonciaProf
�(�( ?
(
�(�(? @
ref
�(�(@ C
MsgRes
�(�(D J
)
�(�(J K
;
�(�(K L
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( ,
vw_odont_tableros_ProtesisFija
�(�( 2
>
�(�(2 3$
ConsultaListadoTTOsPPF
�(�(4 J
(
�(�(J K
ref
�(�(K N 
MessageResponseOBJ
�(�(O a
MsgRes
�(�(b h
)
�(�(h i
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( $
ConsultaListadoTTOsPPF
�(�( 5
(
�(�(5 6
ref
�(�(6 9
MsgRes
�(�(: @
)
�(�(@ A
;
�(�(A B
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( 1
#vw_odont_tableros_ProtesisFija_prof
�(�( 7
>
�(�(7 8%
ConsultaListadoTTOsProf
�(�(9 P
(
�(�(P Q
ref
�(�(Q T 
MessageResponseOBJ
�(�(U g
MsgRes
�(�(h n
)
�(�(n o
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( %
ConsultaListadoTTOsProf
�(�( 6
(
�(�(6 7
ref
�(�(7 :
MsgRes
�(�(; A
)
�(�(A B
;
�(�(B C
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( -
vw_odont_tableros_ProtesisRemov
�(�( 3
>
�(�(3 4*
ConsultaListadoTTOsRemovible
�(�(5 Q
(
�(�(Q R
ref
�(�(R U 
MessageResponseOBJ
�(�(V h
MsgRes
�(�(i o
)
�(�(o p
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( *
ConsultaListadoTTOsRemovible
�(�( ;
(
�(�(; <
ref
�(�(< ?
MsgRes
�(�(@ F
)
�(�(F G
;
�(�(G H
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( 2
$vw_odont_tableros_ProtesisRemov_prof
�(�( 8
>
�(�(8 9/
!ConsultaListadoTTOsRemoviblesProf
�(�(: [
(
�(�([ \
ref
�(�(\ _ 
MessageResponseOBJ
�(�(` r
MsgRes
�(�(s y
)
�(�(y z
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( /
!ConsultaListadoTTOsRemoviblesProf
�(�( @
(
�(�(@ A
ref
�(�(A D
MsgRes
�(�(E K
)
�(�(K L
;
�(�(L M
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( *
vw_odont_tableros_endodoncia
�(�( 0
>
�(�(0 1+
ConsultaListadoTTOsEndodoncia
�(�(2 O
(
�(�(O P
ref
�(�(P S 
MessageResponseOBJ
�(�(T f
MsgRes
�(�(g m
)
�(�(m n
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( +
ConsultaListadoTTOsEndodoncia
�(�( <
(
�(�(< =
ref
�(�(= @
MsgRes
�(�(A G
)
�(�(G H
;
�(�(H I
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( /
!vw_odont_tableros_endodoncia_prof
�(�( 5
>
�(�(5 6+
ConsultaListadoEndodonciaProf
�(�(7 T
(
�(�(T U
ref
�(�(U X 
MessageResponseOBJ
�(�(Y k
MsgRes
�(�(l r
)
�(�(r s
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( +
ConsultaListadoEndodonciaProf
�(�( <
(
�(�(< =
ref
�(�(= @
MsgRes
�(�(A G
)
�(�(G H
;
�(�(H I
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( '
Ref_odont_acciones_mejora
�(�( -
>
�(�(- .#
GetListAccionesMejora
�(�(/ D
(
�(�(D E
)
�(�(E F
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( #
GetListAccionesMejora
�(�( 4
(
�(�(4 5
)
�(�(5 6
;
�(�(6 7
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( %
Ref_odont_estado_accion
�(�( +
>
�(�(+ ,*
GetListEstadosAccionesMejora
�(�(- I
(
�(�(I J
)
�(�(J K
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( *
GetListEstadosAccionesMejora
�(�( ;
(
�(�(; <
)
�(�(< =
;
�(�(= >
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( &
vw_odont_tbl_prestadores
�(�( ,
>
�(�(, -&
GetprestadoresPlanMejora
�(�(. F
(
�(�(F G
)
�(�(G H
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( &
GetprestadoresPlanMejora
�(�( 7
(
�(�(7 8
)
�(�(8 9
;
�(�(9 :
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( $
vw_odont_planes_mejora
�(�( *
>
�(�(* +
GetPlanesMejora
�(�(, ;
(
�(�(; <
)
�(�(< =
{
�(�( 	
return
�(�( 
DACConsulta
�(�( 
.
�(�( 
GetPlanesMejora
�(�( .
(
�(�(. /
)
�(�(/ 0
;
�(�(0 1
}
�(�( 	
public
�(�( 
Int32
�(�( +
InsertarPlanMejoraTratamiento
�(�( 2
(
�(�(2 3+
odont_plan_mejora_tratamiento
�(�(3 P
OBJ
�(�(Q T
,
�(�(T U
ref
�(�(V Y 
MessageResponseOBJ
�(�(Z l
MsgRes
�(�(m s
)
�(�(s t
{
�(�( 	
return
�(�( 

DACInserta
�(�( 
.
�(�( +
InsertarPlanMejoraTratamiento
�(�( ;
(
�(�(; <
OBJ
�(�(< ?
,
�(�(? @
ref
�(�(A D
MsgRes
�(�(E K
)
�(�(K L
;
�(�(L M
}
�(�( 	
public
�(�( 
Int32
�(�( 2
$InsertarPlanMejoraTratamientodetalle
�(�( 9
(
�(�(9 :0
"odont_plan_mejora_tratamiento_dtll
�(�(: \
OBJ
�(�(] `
,
�(�(` a
ref
�(�(b e 
MessageResponseOBJ
�(�(f x
MsgRes
�(�(y 
)�(�( �
{
�(�( 	
return
�(�( 

DACInserta
�(�( 
.
�(�( 2
$InsertarPlanMejoraTratamientodetalle
�(�( B
(
�(�(B C
OBJ
�(�(C F
,
�(�(F G
ref
�(�(H K
MsgRes
�(�(L R
)
�(�(R S
;
�(�(S T
}
�(�( 	
public
�(�( 
Int32
�(�( ,
InsertarPlanMejoraBeneficiario
�(�( 3
(
�(�(3 4,
odont_plan_mejora_beneficiario
�(�(4 R
OBJ
�(�(S V
,
�(�(V W
ref
�(�(X [ 
MessageResponseOBJ
�(�(\ n
MsgRes
�(�(o u
)
�(�(u v
{
�(�( 	
return
�(�( 

DACInserta
�(�( 
.
�(�( ,
InsertarPlanMejoraBeneficiario
�(�( <
(
�(�(< =
OBJ
�(�(= @
,
�(�(@ A
ref
�(�(B E
MsgRes
�(�(F L
)
�(�(L M
;
�(�(M N
}
�(�( 	
public
�(�( 
Int32
�(�( 3
%InsertarPlanMejoraBeneficiariodetalle
�(�( :
(
�(�(: ;1
#odont_plan_mejora_beneficiario_dtll
�(�(; ^
OBJ
�(�(_ b
,
�(�(b c
ref
�(�(d g 
MessageResponseOBJ
�(�(h z
MsgRes�(�({ �
)�(�(� �
{
�(�( 	
return
�(�( 

DACInserta
�(�( 
.
�(�( 3
%InsertarPlanMejoraBeneficiariodetalle
�(�( C
(
�(�(C D
OBJ
�(�(D G
,
�(�(G H
ref
�(�(I L
MsgRes
�(�(M S
)
�(�(S T
;
�(�(T U
}
�(�( 	
public
�(�( 
void
�(�( '
ActualizarOdontPlanMejora
�(�( -
(
�(�(- .$
odont_plan_mejora_dtll
�(�(. D
obj2
�(�(E I
,
�(�(I J
ref
�(�(K N 
MessageResponseOBJ
�(�(O a
MsgRes
�(�(b h
)
�(�(h i
{
�(�( 	
DACActualiza
�(�( 
.
�(�( '
ActualizarOdontPlanMejora
�(�( 2
(
�(�(2 3
obj2
�(�(3 7
,
�(�(7 8
ref
�(�(9 <
MsgRes
�(�(= C
)
�(�(C D
;
�(�(D E
}
�(�( 	
public
�(�( 
void
�(�( 3
%ActualizarOdontPlanMejoraBeneficiario
�(�( 9
(
�(�(9 :1
#odont_plan_mejora_beneficiario_dtll
�(�(: ]
obj2
�(�(^ b
,
�(�(b c
ref
�(�(d g 
MessageResponseOBJ
�(�(h z
MsgRes�(�({ �
)�(�(� �
{
�(�( 	
DACActualiza
�(�( 
.
�(�( 3
%ActualizarOdontPlanMejoraBeneficiario
�(�( >
(
�(�(> ?
obj2
�(�(? C
,
�(�(C D
ref
�(�(E H
MsgRes
�(�(I O
)
�(�(O P
;
�(�(P Q
}
�(�( 	
public
�(�( 
List
�(�( 
<
�(�( 
odont_plan_mejora
�(�( %
>
�(�(% &
GetPlanMejoraTra
�(�(' 7
(
�(�(7 8
)
�(�(8 9
{
�(�( 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) 
GetPlanMejoraTra
�)�) /
(
�)�)/ 0
)
�)�)0 1
;
�)�)1 2
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) ,
odont_plan_mejora_beneficiario
�)�) 2
>
�)�)2 3
GetPlanMejoraBen
�)�)4 D
(
�)�)D E
)
�)�)E F
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) 
GetPlanMejoraBen
�)�) /
(
�)�)/ 0
)
�)�)0 1
;
�)�)1 2
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) $
vw_odont_planes_mejora
�)�) *
>
�)�)* +"
GetPlanMejoraTradtll
�)�), @
(
�)�)@ A
Int32
�)�)A F"
id_odont_plan_mejora
�)�)G [
,
�)�)[ \
ref
�)�)] ` 
MessageResponseOBJ
�)�)a s
MsgRes
�)�)t z
)
�)�)z {
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) "
GetPlanMejoraTradtll
�)�) 3
(
�)�)3 4"
id_odont_plan_mejora
�)�)4 H
,
�)�)H I
ref
�)�)J M
MsgRes
�)�)N T
)
�)�)T U
;
�)�)U V
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) (
vw_odont_planes_mejora_ben
�)�) .
>
�)�). /"
GetPlanMejoraBendtll
�)�)0 D
(
�)�)D E
Int32
�)�)E J/
!id_odont_plan_mejora_beneficiario
�)�)K l
,
�)�)l m
ref
�)�)n q!
MessageResponseOBJ�)�)r �
MsgRes�)�)� �
)�)�)� �
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) "
GetPlanMejoraBendtll
�)�) 3
(
�)�)3 4/
!id_odont_plan_mejora_beneficiario
�)�)4 U
,
�)�)U V
ref
�)�)W Z
MsgRes
�)�)[ a
)
�)�)a b
;
�)�)b c
}
�)�) 	
public
�)�) 
void
�)�) 5
'ActualizarOdontPlanMejoraObsTratamiento
�)�) ;
(
�)�); <
odont_plan_mejora
�)�)< M
obj2
�)�)N R
,
�)�)R S
ref
�)�)T W 
MessageResponseOBJ
�)�)X j
MsgRes
�)�)k q
)
�)�)q r
{
�)�) 	
DACActualiza
�)�) 
.
�)�) 5
'ActualizarOdontPlanMejoraObsTratamiento
�)�) @
(
�)�)@ A
obj2
�)�)A E
,
�)�)E F
ref
�)�)G J
MsgRes
�)�)K Q
)
�)�)Q R
;
�)�)R S
}
�)�) 	
public
�)�) 
void
�)�) 6
(ActualizarOdontPlanMejoraObsBeneficiario
�)�) <
(
�)�)< =,
odont_plan_mejora_beneficiario
�)�)= [
obj2
�)�)\ `
,
�)�)` a
ref
�)�)b e 
MessageResponseOBJ
�)�)f x
MsgRes
�)�)y 
)�)�) �
{
�)�) 	
DACActualiza
�)�) 
.
�)�) 6
(ActualizarOdontPlanMejoraObsBeneficiario
�)�) A
(
�)�)A B
obj2
�)�)B F
,
�)�)F G
ref
�)�)H K
MsgRes
�)�)L R
)
�)�)R S
;
�)�)S T
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) (
vw_tablero_plan_mejora_ben
�)�) .
>
�)�). /$
ConsultaTableroPlanBen
�)�)0 F
(
�)�)F G
)
�)�)G H
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) $
ConsultaTableroPlanBen
�)�) 5
(
�)�)5 6
)
�)�)6 7
;
�)�)7 8
}
�)�) 	
public
�)�) 
void
�)�) 1
#ActualizarOdontPlanMejoraCierreTrat
�)�) 7
(
�)�)7 8
odont_plan_mejora
�)�)8 I
obj2
�)�)J N
,
�)�)N O
ref
�)�)P S 
MessageResponseOBJ
�)�)T f
MsgRes
�)�)g m
)
�)�)m n
{
�)�) 	
DACActualiza
�)�) 
.
�)�) 1
#ActualizarOdontPlanMejoraCierreTrat
�)�) <
(
�)�)< =
obj2
�)�)= A
,
�)�)A B
ref
�)�)C F
MsgRes
�)�)G M
)
�)�)M N
;
�)�)N O
}
�)�) 	
public
�)�) 
void
�)�) 0
"ActualizarOdontPlanMejoraCierreBen
�)�) 6
(
�)�)6 7,
odont_plan_mejora_beneficiario
�)�)7 U
obj2
�)�)V Z
,
�)�)Z [
ref
�)�)\ _ 
MessageResponseOBJ
�)�)` r
MsgRes
�)�)s y
)
�)�)y z
{
�)�) 	
DACActualiza
�)�) 
.
�)�) 0
"ActualizarOdontPlanMejoraCierreBen
�)�) ;
(
�)�); <
obj2
�)�)< @
,
�)�)@ A
ref
�)�)B E
MsgRes
�)�)F L
)
�)�)L M
;
�)�)M N
}
�)�) 	
public
�)�) 
Int32
�)�) %
InsertarHistoriaClinica
�)�) ,
(
�)�), -$
odont_historia_clinica
�)�)- C
OBJ
�)�)D G
,
�)�)G H
ref
�)�)I L 
MessageResponseOBJ
�)�)M _
MsgRes
�)�)` f
)
�)�)f g
{
�)�) 	
return
�)�) 

DACInserta
�)�) 
.
�)�) %
InsertarHistoriaClinica
�)�) 5
(
�)�)5 6
OBJ
�)�)6 9
,
�)�)9 :
ref
�)�); >
MsgRes
�)�)? E
)
�)�)E F
;
�)�)F G
}
�)�) 	
public
�)�) 
Int32
�)�) -
InsertarHistoriaClinicaPaciente
�)�) 4
(
�)�)4 5-
odont_historia_clinica_paciente
�)�)5 T
OBJ
�)�)U X
,
�)�)X Y
ref
�)�)Z ] 
MessageResponseOBJ
�)�)^ p
MsgRes
�)�)q w
)
�)�)w x
{
�)�) 	
return
�)�) 

DACInserta
�)�) 
.
�)�) -
InsertarHistoriaClinicaPaciente
�)�) =
(
�)�)= >
OBJ
�)�)> A
,
�)�)A B
ref
�)�)C F
MsgRes
�)�)G M
)
�)�)M N
;
�)�)N O
}
�)�) 	
public
�)�) 
Int32
�)�) ,
InsertarHistoriaClinicaDetalle
�)�) 3
(
�)�)3 44
&odont_historia_clinica_detalle_calidad
�)�)4 Z
OBJ
�)�)[ ^
,
�)�)^ _
ref
�)�)` c 
MessageResponseOBJ
�)�)d v
MsgRes
�)�)w }
)
�)�)} ~
{
�)�) 	
return
�)�) 

DACInserta
�)�) 
.
�)�) ,
InsertarHistoriaClinicaDetalle
�)�) <
(
�)�)< =
OBJ
�)�)= @
,
�)�)@ A
ref
�)�)B E
MsgRes
�)�)F L
)
�)�)L M
;
�)�)M N
}
�)�) 	
public
�)�) 
Int32
�)�) 1
#InsertarHistoriaClinicaDetalleConte
�)�) 8
(
�)�)8 96
(odont_historia_clinica_detalle_contenido
�)�)9 a
OBJ
�)�)b e
,
�)�)e f
ref
�)�)g j 
MessageResponseOBJ
�)�)k }
MsgRes�)�)~ �
)�)�)� �
{
�)�) 	
return
�)�) 

DACInserta
�)�) 
.
�)�) 1
#InsertarHistoriaClinicaDetalleConte
�)�) A
(
�)�)A B
OBJ
�)�)B E
,
�)�)E F
ref
�)�)G J
MsgRes
�)�)K Q
)
�)�)Q R
;
�)�)R S
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) $
odont_historia_clinica
�)�) *
>
�)�)* + 
GetHistoriaClinica
�)�), >
(
�)�)> ?
)
�)�)? @
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�)  
GetHistoriaClinica
�)�) 1
(
�)�)1 2
)
�)�)2 3
;
�)�)3 4
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) -
odont_historia_clinica_paciente
�)�) 3
>
�)�)3 4(
GetHistoriaClinicaPaciente
�)�)5 O
(
�)�)O P
Int32
�)�)P U'
id_odont_historia_clinica
�)�)V o
,
�)�)o p
ref
�)�)q t!
MessageResponseOBJ�)�)u �
MsgRes�)�)� �
)�)�)� �
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) (
GetHistoriaClinicaPaciente
�)�) 9
(
�)�)9 :'
id_odont_historia_clinica
�)�): S
,
�)�)S T
ref
�)�)U X
MsgRes
�)�)Y _
)
�)�)_ `
;
�)�)` a
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) /
!vw_odont_historia_clinica_detalle
�)�) 5
>
�)�)5 6)
GetVWHistoriaClinicaDetalle
�)�)7 R
(
�)�)R S
Int32
�)�)S X0
"id_odont_historia_clinica_paciente
�)�)Y {
,
�)�){ |
ref�)�)} �"
MessageResponseOBJ�)�)� �
MsgRes�)�)� �
)�)�)� �
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) )
GetVWHistoriaClinicaDetalle
�)�) :
(
�)�): ;0
"id_odont_historia_clinica_paciente
�)�); ]
,
�)�)] ^
ref
�)�)_ b
MsgRes
�)�)c i
)
�)�)i j
;
�)�)j k
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) 9
+vw_odont_historia_clinica_detalle_contenido
�)�) ?
>
�)�)? @/
!GetVWHistoriaClinicaDetalleConten
�)�)A b
(
�)�)b c
Int32
�)�)c h1
"id_odont_historia_clinica_paciente�)�)i �
,�)�)� �
ref�)�)� �"
MessageResponseOBJ�)�)� �
MsgRes�)�)� �
)�)�)� �
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) /
!GetVWHistoriaClinicaDetalleConten
�)�) @
(
�)�)@ A0
"id_odont_historia_clinica_paciente
�)�)A c
,
�)�)c d
ref
�)�)e h
MsgRes
�)�)i o
)
�)�)o p
;
�)�)p q
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) )
Ref_odont_hc_calidad_formal
�)�) /
>
�)�)/ 00
"GetHistoriaClinicaRefCalidadFormal
�)�)1 S
(
�)�)S T
)
�)�)T U
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) 0
"GetHistoriaClinicaRefCalidadFormal
�)�) A
(
�)�)A B
)
�)�)B C
;
�)�)C D
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) ,
Ref_odont_hc_calidad_contenido
�)�) 2
>
�)�)2 3,
GetHistoriaClinicaRefContenido
�)�)4 R
(
�)�)R S
)
�)�)S T
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) ,
GetHistoriaClinicaRefContenido
�)�) =
(
�)�)= >
)
�)�)> ?
;
�)�)? @
}
�)�) 	
public
�)�) 
void
�)�) $
ActualizarOdontHCdtll1
�)�) *
(
�)�)* +4
&odont_historia_clinica_detalle_calidad
�)�)+ Q
obj2
�)�)R V
,
�)�)V W
ref
�)�)X [ 
MessageResponseOBJ
�)�)\ n
MsgRes
�)�)o u
)
�)�)u v
{
�)�) 	
DACActualiza
�)�) 
.
�)�) $
ActualizarOdontHCdtll1
�)�) /
(
�)�)/ 0
obj2
�)�)0 4
,
�)�)4 5
ref
�)�)6 9
MsgRes
�)�): @
)
�)�)@ A
;
�)�)A B
}
�)�) 	
public
�)�) 
void
�)�) $
ActualizarOdontHCdtll2
�)�) *
(
�)�)* +6
(odont_historia_clinica_detalle_contenido
�)�)+ S
obj2
�)�)T X
,
�)�)X Y
ref
�)�)Z ] 
MessageResponseOBJ
�)�)^ p
MsgRes
�)�)q w
)
�)�)w x
{
�)�) 	
DACActualiza
�)�) 
.
�)�) $
ActualizarOdontHCdtll2
�)�) /
(
�)�)/ 0
obj2
�)�)0 4
,
�)�)4 5
ref
�)�)6 9
MsgRes
�)�): @
)
�)�)@ A
;
�)�)A B
}
�)�) 	
public
�)�) 
void
�)�) (
ActualizarOdontHCdtllFinal
�)�) .
(
�)�). /-
odont_historia_clinica_paciente
�)�)/ N
obj2
�)�)O S
,
�)�)S T
ref
�)�)U X 
MessageResponseOBJ
�)�)Y k
MsgRes
�)�)l r
)
�)�)r s
{
�)�) 	
DACActualiza
�)�) 
.
�)�) (
ActualizarOdontHCdtllFinal
�)�) 3
(
�)�)3 4
obj2
�)�)4 8
,
�)�)8 9
ref
�)�): =
MsgRes
�)�)> D
)
�)�)D E
;
�)�)E F
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) #
Ref_odont_prestadores
�)�) )
>
�)�)) *!
GetPrestadoresOdont
�)�)+ >
(
�)�)> ?
)
�)�)? @
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) !
GetPrestadoresOdont
�)�) 2
(
�)�)2 3
)
�)�)3 4
;
�)�)4 5
}
�)�) 	
public
�)�) 
Int32
�)�)  
InsertarPlanMejora
�)�) '
(
�)�)' (
odont_plan_mejora
�)�)( 9
OBJ
�)�): =
,
�)�)= >
ref
�)�)? B 
MessageResponseOBJ
�)�)C U
MsgRes
�)�)V \
)
�)�)\ ]
{
�)�) 	
return
�)�) 

DACInserta
�)�) 
.
�)�)  
InsertarPlanMejora
�)�) 0
(
�)�)0 1
OBJ
�)�)1 4
,
�)�)4 5
ref
�)�)6 9
MsgRes
�)�): @
)
�)�)@ A
;
�)�)A B
}
�)�) 	
public
�)�) 
Int32
�)�) '
InsertarPlanMejoradetalle
�)�) .
(
�)�). /$
odont_plan_mejora_dtll
�)�)/ E
OBJ
�)�)F I
,
�)�)I J
ref
�)�)K N 
MessageResponseOBJ
�)�)O a
MsgRes
�)�)b h
)
�)�)h i
{
�)�) 	
return
�)�) 

DACInserta
�)�) 
.
�)�) '
InsertarPlanMejoradetalle
�)�) 7
(
�)�)7 8
OBJ
�)�)8 ;
,
�)�); <
ref
�)�)= @
MsgRes
�)�)A G
)
�)�)G H
;
�)�)H I
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) !
vw_odont_totales_hc
�)�) '
>
�)�)' (
GetOdontogTotales
�)�)) :
(
�)�): ;
Int32
�)�); @'
id_odont_historia_clinica
�)�)A Z
,
�)�)Z [
ref
�)�)\ _ 
MessageResponseOBJ
�)�)` r
MsgRes
�)�)s y
)
�)�)y z
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) 
GetOdontogTotales
�)�) 0
(
�)�)0 1'
id_odont_historia_clinica
�)�)1 J
,
�)�)J K
ref
�)�)L O
MsgRes
�)�)P V
)
�)�)V W
;
�)�)W X
}
�)�) 	
public
�)�) 
List
�)�) 
<
�)�) *
vw_odont_detalle_plan_mejora
�)�) 0
>
�)�)0 1)
GetOdontogdetallePlanMejora
�)�)2 M
(
�)�)M N
)
�)�)N O
{
�)�) 	
return
�)�) 
DACConsulta
�)�) 
.
�)�) )
GetOdontogdetallePlanMejora
�)�) :
(
�)�): ;
)
�)�); <
;
�)�)< =
}
�)�) 	
public
�*�* 
List
�*�* 
<
�*�* *
vw_odont_tableros_ortodoncia
�*�* 0
>
�*�*0 1*
GetOdontogTablerosOrtodoncia
�*�*2 N
(
�*�*N O
)
�*�*O P
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* *
GetOdontogTablerosOrtodoncia
�*�* ;
(
�*�*; <
)
�*�*< =
;
�*�*= >
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* ,
vw_odont_tableros_ProtesisFija
�*�* 2
>
�*�*2 3"
GetOdontogTablerosPT
�*�*4 H
(
�*�*H I
)
�*�*I J
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* "
GetOdontogTablerosPT
�*�* 3
(
�*�*3 4
)
�*�*4 5
;
�*�*5 6
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* -
vw_odont_tableros_ProtesisRemov
�*�* 3
>
�*�*3 4"
GetOdontogTablerosPR
�*�*5 I
(
�*�*I J
)
�*�*J K
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* "
GetOdontogTablerosPR
�*�* 3
(
�*�*3 4
)
�*�*4 5
;
�*�*5 6
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* *
vw_odont_tableros_endodoncia
�*�* 0
>
�*�*0 1*
GetOdontogTablerosEndodoncia
�*�*2 N
(
�*�*N O
)
�*�*O P
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* *
GetOdontogTablerosEndodoncia
�*�* ;
(
�*�*; <
)
�*�*< =
;
�*�*= >
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* 0
"Ref_odont_parametros_auditados_rem
�*�* 6
>
�*�*6 7
GetparametrosRem
�*�*8 H
(
�*�*H I
)
�*�*I J
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* 
GetparametrosRem
�*�* /
(
�*�*/ 0
)
�*�*0 1
;
�*�*1 2
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* '
Ref_odont_tratamiento_rem
�*�* -
>
�*�*- . 
GetTratamientosRem
�*�*/ A
(
�*�*A B
)
�*�*B C
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�*  
GetTratamientosRem
�*�* 1
(
�*�*1 2
)
�*�*2 3
;
�*�*3 4
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* +
vw_odont_tableros_plan_mejora
�*�* 1
>
�*�*1 2*
GetOdontogTablerosPlanMejora
�*�*3 O
(
�*�*O P
)
�*�*P Q
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* *
GetOdontogTablerosPlanMejora
�*�* ;
(
�*�*; <
)
�*�*< =
;
�*�*= >
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* %
vw_odont_consolidado_hc
�*�* +
>
�*�*+ ,
GetConsolidadoHc
�*�*- =
(
�*�*= >
ref
�*�*> A 
MessageResponseOBJ
�*�*B T
MsgRes
�*�*U [
)
�*�*[ \
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* 
GetConsolidadoHc
�*�* /
(
�*�*/ 0
ref
�*�*0 3
MsgRes
�*�*4 :
)
�*�*: ;
;
�*�*; <
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* /
!vw_odont_consolidado_hc_prestador
�*�* 5
>
�*�*5 6*
GetConsolidadoHcporprestador
�*�*7 S
(
�*�*S T
ref
�*�*T W 
MessageResponseOBJ
�*�*X j
MsgRes
�*�*k q
)
�*�*q r
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* *
GetConsolidadoHcporprestador
�*�* ;
(
�*�*; <
ref
�*�*< ?
MsgRes
�*�*@ F
)
�*�*F G
;
�*�*G H
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* *
vw_odont_count_planes_mejora
�*�* 0
>
�*�*0 1&
GetListCountPlanesMejora
�*�*2 J
(
�*�*J K
int
�*�*K N

idregional
�*�*O Y
)
�*�*Y Z
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* &
GetListCountPlanesMejora
�*�* 7
(
�*�*7 8

idregional
�*�*8 B
)
�*�*B C
;
�*�*C D
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* ,
vw_odont_count_acciones_mejora
�*�* 2
>
�*�*2 3(
GetListCountAccionesMejora
�*�*4 N
(
�*�*N O
int
�*�*O R

idregional
�*�*S ]
)
�*�*] ^
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* (
GetListCountAccionesMejora
�*�* 9
(
�*�*9 :

idregional
�*�*: D
)
�*�*D E
;
�*�*E F
}
�*�* 	
public
�*�* 
void
�*�* .
 InsertarRemisionesEspecialidades
�*�* 4
(
�*�*4 5-
odont_remisiones_especialidades
�*�*5 T
obj
�*�*U X
,
�*�*X Y
ref
�*�*Z ] 
MessageResponseOBJ
�*�*^ p
MsgRes
�*�*q w
)
�*�*w x
{
�*�* 	

DACInserta
�*�* 
.
�*�* .
 InsertarRemisionesEspecialidades
�*�* 7
(
�*�*7 8
obj
�*�*8 ;
,
�*�*; <
ref
�*�*= @
MsgRes
�*�*A G
)
�*�*G H
;
�*�*H I
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* 0
"vw_odont_remisiones_especialidades
�*�* 6
>
�*�*6 7
GetRemisiones
�*�*8 E
(
�*�*E F
ref
�*�*F I 
MessageResponseOBJ
�*�*J \
MsgRes
�*�*] c
)
�*�*c d
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* 
GetRemisiones
�*�* ,
(
�*�*, -
ref
�*�*- 0
MsgRes
�*�*1 7
)
�*�*7 8
;
�*�*8 9
}
�*�* 	
public
�*�* 
void
�*�* +
InsertarRemisionesVerificadas
�*�* 1
(
�*�*1 2*
odont_remisiones_verificadas
�*�*2 N
obj
�*�*O R
,
�*�*R S
ref
�*�*T W 
MessageResponseOBJ
�*�*X j
MsgRes
�*�*k q
)
�*�*q r
{
�*�* 	

DACInserta
�*�* 
.
�*�* +
InsertarRemisionesVerificadas
�*�* 4
(
�*�*4 5
obj
�*�*5 8
,
�*�*8 9
ref
�*�*: =
MsgRes
�*�*> D
)
�*�*D E
;
�*�*E F
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* -
vw_odont_remisiones_verificadas
�*�* 3
>
�*�*3 4&
GetRemisionesVerificadas
�*�*5 M
(
�*�*M N
ref
�*�*N Q 
MessageResponseOBJ
�*�*R d
MsgRes
�*�*e k
)
�*�*k l
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* &
GetRemisionesVerificadas
�*�* 7
(
�*�*7 8
ref
�*�*8 ;
MsgRes
�*�*< B
)
�*�*B C
;
�*�*C D
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* 
vw_totales_odont
�*�* $
>
�*�*$ %
GetTotalPaciente
�*�*& 6
(
�*�*6 7
Int32
�*�*7 <'
id_odont_historia_clinica
�*�*= V
,
�*�*V W
ref
�*�*X [ 
MessageResponseOBJ
�*�*\ n
MsgRes
�*�*o u
)
�*�*u v
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* 
GetTotalPaciente
�*�* /
(
�*�*/ 0'
id_odont_historia_clinica
�*�*0 I
,
�*�*I J
ref
�*�*K N
MsgRes
�*�*O U
)
�*�*U V
;
�*�*V W
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* /
!vw_reportesTratamientosUnificados
�*�* 5
>
�*�*5 6-
GetReportTratamientosUnificados
�*�*7 V
(
�*�*V W
ref
�*�*W Z 
MessageResponseOBJ
�*�*[ m
MsgRes
�*�*n t
)
�*�*t u
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* -
GetReportTratamientosUnificados
�*�* >
(
�*�*> ?
ref
�*�*? B
MsgRes
�*�*C I
)
�*�*I J
;
�*�*J K
}
�*�* 	
public
�*�* 
void
�*�* *
InsertarprestadorOdontologia
�*�* 0
(
�*�*0 1#
Ref_odont_prestadores
�*�*1 F
obj
�*�*G J
,
�*�*J K
ref
�*�*L O 
MessageResponseOBJ
�*�*P b
MsgRes
�*�*c i
)
�*�*i j
{
�*�* 	

DACInserta
�*�* 
.
�*�* *
InsertarprestadorOdontologia
�*�* 3
(
�*�*3 4
obj
�*�*4 7
,
�*�*7 8
ref
�*�*9 <
MsgRes
�*�*= C
)
�*�*C D
;
�*�*D E
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* 1
#vw_prestadores_odontologiaUnificado
�*�* 7
>
�*�*7 8*
GetPrestadoresOdonUnificados
�*�*9 U
(
�*�*U V
ref
�*�*V Y 
MessageResponseOBJ
�*�*Z l
MsgRes
�*�*m s
)
�*�*s t
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* *
GetPrestadoresOdonUnificados
�*�* ;
(
�*�*; <
ref
�*�*< ?
MsgRes
�*�*@ F
)
�*�*F G
;
�*�*G H
}
�*�* 	
public
�*�* 
Int32
�*�* $
InsertarFFMMRadicacion
�*�* +
(
�*�*+ ,%
ffmm_Cuentas_radicacion
�*�*, C
OBJ
�*�*D G
,
�*�*G H
ref
�*�*I L 
MessageResponseOBJ
�*�*M _
MsgRes
�*�*` f
)
�*�*f g
{
�*�* 	
return
�*�* 

DACInserta
�*�* 
.
�*�* $
InsertarFFMMRadicacion
�*�* 4
(
�*�*4 5
OBJ
�*�*5 8
,
�*�*8 9
ref
�*�*: =
MsgRes
�*�*> D
)
�*�*D E
;
�*�*E F
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* -
vw_ffmm_consulta_radicacion_ips
�*�* 3
>
�*�*3 4%
GetOdontogRadicacionIPS
�*�*5 L
(
�*�*L M
)
�*�*M N
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* %
GetOdontogRadicacionIPS
�*�* 6
(
�*�*6 7
)
�*�*7 8
;
�*�*8 9
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* %
ffmm_Cuentas_radicacion
�*�* +
>
�*�*+ ,&
GetRadicacionIPSFacturas
�*�*- E
(
�*�*E F
Int32
�*�*F K
id_proveedor
�*�*L X
,
�*�*X Y
Int32
�*�*Z _

id_factura
�*�*` j
,
�*�*j k
string
�*�*l r
prefijo
�*�*s z
,
�*�*z {
ref
�*�*| "
MessageResponseOBJ�*�*� �
MsgRes�*�*� �
)�*�*� �
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�* &
GetRadicacionIPSFacturas
�*�* 7
(
�*�*7 8
id_proveedor
�*�*8 D
,
�*�*D E

id_factura
�*�*F P
,
�*�*P Q
prefijo
�*�*R Y
,
�*�*Y Z
ref
�*�*[ ^
MsgRes
�*�*_ e
)
�*�*e f
;
�*�*f g
}
�*�* 	
public
�*�* 
Int32
�*�* #
InsertarFFMMAuditoria
�*�* *
(
�*�** +$
ffmm_Cuentas_auditoria
�*�*+ A
OBJ
�*�*B E
,
�*�*E F
List
�*�*G K
<
�*�*K L'
ffmm_cuentas_medicas_cups
�*�*L e
>
�*�*e f
obj2
�*�*g k
,
�*�*k l
List
�*�*m q
<
�*�*q r*
ffmm_cuentas_medicas_glosas�*�*r �
>�*�*� �
obj3�*�*� �
,�*�*� �
ref�*�*� �"
MessageResponseOBJ�*�*� �
MsgRes�*�*� �
)�*�*� �
{
�*�* 	
return
�*�* 

DACInserta
�*�* 
.
�*�* #
InsertarFFMMAuditoria
�*�* 3
(
�*�*3 4
OBJ
�*�*4 7
,
�*�*7 8
obj2
�*�*9 =
,
�*�*= >
obj3
�*�*? C
,
�*�*C D
ref
�*�*E H
MsgRes
�*�*I O
)
�*�*O P
;
�*�*P Q
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* ,
management_CupsAuditoriaResult
�*�* 2
>
�*�*2 3 
ListaCupsAuditoria
�*�*4 F
(
�*�*F G
)
�*�*G H
{
�*�* 	
return
�*�* 
DACConsulta
�*�* 
.
�*�*  
ListaCupsAuditoria
�*�* 1
(
�*�*1 2
)
�*�*2 3
;
�*�*3 4
}
�*�* 	
public
�*�* 
void
�*�* (
ActualizarEstadoRadicacion
�*�* .
(
�*�*. /%
ffmm_Cuentas_radicacion
�*�*/ F
obj2
�*�*G K
,
�*�*K L
ref
�*�*M P 
MessageResponseOBJ
�*�*Q c
MsgRes
�*�*d j
)
�*�*j k
{
�*�* 	
DACActualiza
�*�* 
.
�*�* (
ActualizarEstadoRadicacion
�*�* 3
(
�*�*3 4
obj2
�*�*4 8
,
�*�*8 9
ref
�*�*: =
MsgRes
�*�*> D
)
�*�*D E
;
�*�*E F
}
�*�* 	
public
�*�* 
List
�*�* 
<
�*�* $
ffmm_Cuentas_auditoria
�*�* *
>
�*�** +
GetIPSTotal
�*�*, 7
(
�*�*7 8
Int32
�*�*8 =,
id_ref_ffmm_radicacion_Cuentas
�*�*> \
,
�*�*\ ]
ref
�*�*^ a 
MessageResponseOBJ
�*�*b t
MsgRes
�*�*u {
)
�*�*{ |
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ 
GetIPSTotal
�+�+ *
(
�+�+* +,
id_ref_ffmm_radicacion_Cuentas
�+�++ I
,
�+�+I J
ref
�+�+K N
MsgRes
�+�+O U
)
�+�+U V
;
�+�+V W
}
�+�+ 	
public
�+�+ 
Int32
�+�+ )
InsertarFFMMAuditoriaGlosas
�+�+ 0
(
�+�+0 1!
ffmm_cuentas_glosas
�+�+1 D
OBJ
�+�+E H
,
�+�+H I
ref
�+�+J M 
MessageResponseOBJ
�+�+N `
MsgRes
�+�+a g
)
�+�+g h
{
�+�+ 	
return
�+�+ 

DACInserta
�+�+ 
.
�+�+ )
InsertarFFMMAuditoriaGlosas
�+�+ 9
(
�+�+9 :
OBJ
�+�+: =
,
�+�+= >
ref
�+�+? B
MsgRes
�+�+C I
)
�+�+I J
;
�+�+J K
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ !
ffmm_cuentas_glosas
�+�+ '
>
�+�+' (
GetIPSTotalGlosas
�+�+) :
(
�+�+: ;
Int32
�+�+; @,
id_ref_ffmm_radicacion_Cuentas
�+�+A _
,
�+�+_ `
ref
�+�+a d 
MessageResponseOBJ
�+�+e w
MsgRes
�+�+x ~
)
�+�+~ 
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ 
GetIPSTotalGlosas
�+�+ 0
(
�+�+0 1,
id_ref_ffmm_radicacion_Cuentas
�+�+1 O
,
�+�+O P
ref
�+�+Q T
MsgRes
�+�+U [
)
�+�+[ \
;
�+�+\ ]
}
�+�+ 	
public
�+�+ 
void
�+�+ #
ActualizarEstadoGlosa
�+�+ )
(
�+�+) *!
ffmm_cuentas_glosas
�+�+* =
obj2
�+�+> B
,
�+�+B C
ref
�+�+D G 
MessageResponseOBJ
�+�+H Z
MsgRes
�+�+[ a
)
�+�+a b
{
�+�+ 	
DACActualiza
�+�+ 
.
�+�+ #
ActualizarEstadoGlosa
�+�+ .
(
�+�+. /
obj2
�+�+/ 3
,
�+�+3 4
ref
�+�+5 8
MsgRes
�+�+9 ?
)
�+�+? @
;
�+�+@ A
}
�+�+ 	
public
�+�+ 
void
�+�+ /
!ActualizarEstadoGlosaSegundaConci
�+�+ 5
(
�+�+5 6!
ffmm_cuentas_glosas
�+�+6 I
obj2
�+�+J N
,
�+�+N O
ref
�+�+P S 
MessageResponseOBJ
�+�+T f
MsgRes
�+�+g m
)
�+�+m n
{
�+�+ 	
DACActualiza
�+�+ 
.
�+�+ /
!ActualizarEstadoGlosaSegundaConci
�+�+ :
(
�+�+: ;
obj2
�+�+; ?
,
�+�+? @
ref
�+�+A D
MsgRes
�+�+E K
)
�+�+K L
;
�+�+L M
}
�+�+ 	
public
�+�+ 
Int32
�+�+ '
InsertarFFMMref_proveedor
�+�+ .
(
�+�+. /,
Ref_ffmm_prestadores_proveedor
�+�+/ M
OBJ
�+�+N Q
,
�+�+Q R
ref
�+�+S V 
MessageResponseOBJ
�+�+W i
MsgRes
�+�+j p
)
�+�+p q
{
�+�+ 	
return
�+�+ 

DACInserta
�+�+ 
.
�+�+ '
InsertarFFMMref_proveedor
�+�+ 7
(
�+�+7 8
OBJ
�+�+8 ;
,
�+�+; <
ref
�+�+= @
MsgRes
�+�+A G
)
�+�+G H
;
�+�+H I
}
�+�+ 	
public
�+�+ 
Int32
�+�+ (
InsertarFFMMref_paliativos
�+�+ /
(
�+�+/ 0&
ffmm_cuidados_paliativos
�+�+0 H
OBJ
�+�+I L
,
�+�+L M
ref
�+�+N Q 
MessageResponseOBJ
�+�+R d
MsgRes
�+�+e k
)
�+�+k l
{
�+�+ 	
return
�+�+ 

DACInserta
�+�+ 
.
�+�+ (
InsertarFFMMref_paliativos
�+�+ 8
(
�+�+8 9
OBJ
�+�+9 <
,
�+�+< =
ref
�+�+> A
MsgRes
�+�+B H
)
�+�+H I
;
�+�+I J
}
�+�+ 	
public
�+�+ 
int
�+�+ #
InsertarContratosFFMM
�+�+ (
(
�+�+( )
ffmm_contratos
�+�+) 7
obj
�+�+8 ;
)
�+�+; <
{
�+�+ 	
return
�+�+ 

DACInserta
�+�+ 
.
�+�+ #
InsertarContratosFFMM
�+�+ 3
(
�+�+3 4
obj
�+�+4 7
)
�+�+7 8
;
�+�+8 9
}
�+�+ 	
public
�+�+ 
int
�+�+ -
InsertarCargueMasivoPagoFactura
�+�+ 2
(
�+�+2 3
List
�+�+3 7
<
�+�+7 8'
ffmm_cargue_facturas_pago
�+�+8 Q
>
�+�+Q R
List
�+�+S W
,
�+�+W X
ref
�+�+Y \ 
MessageResponseOBJ
�+�+] o
MsgRes
�+�+p v
)
�+�+v w
{
�+�+ 	
return
�+�+ 

DACInserta
�+�+ 
.
�+�+ -
InsertarCargueMasivoPagoFactura
�+�+ =
(
�+�+= >
List
�+�+> B
,
�+�+B C
ref
�+�+D G
MsgRes
�+�+H N
)
�+�+N O
;
�+�+O P
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ -
management_facturas_pagosResult
�+�+ 3
>
�+�+3 4
ListFacturasPago
�+�+5 E
(
�+�+E F
)
�+�+F G
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ 
ListFacturasPago
�+�+ /
(
�+�+/ 0
)
�+�+0 1
;
�+�+1 2
}
�+�+ 	
public
�+�+ 
sis_configuracion
�+�+  
GetParametro
�+�+! -
(
�+�+- .
string
�+�+. 4
	parametro
�+�+5 >
)
�+�+> ?
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ 
GetParametro
�+�+ +
(
�+�++ ,
	parametro
�+�+, 5
)
�+�+5 6
;
�+�+6 7
}
�+�+ 	
public
�+�+ 
Int32
�+�+ /
!InsertarSeguimientoDetalleCovid19
�+�+ 6
(
�+�+6 7
List
�+�+7 ;
<
�+�+; <0
"cargue_seguimiento_covid19_detalle
�+�+< ^
>
�+�+^ _
OBJ
�+�+` c
,
�+�+c d
ref
�+�+e h 
MessageResponseOBJ
�+�+i {
MsgRes�+�+| �
)�+�+� �
{
�+�+ 	
return
�+�+ 

DACInserta
�+�+ 
.
�+�+ /
!InsertarSeguimientoDetalleCovid19
�+�+ ?
(
�+�+? @
OBJ
�+�+@ C
,
�+�+C D
ref
�+�+E H
MsgRes
�+�+I O
)
�+�+O P
;
�+�+P Q
}
�+�+ 	
public
�+�+ 
Int32
�+�+ 3
%InsertarConsolidadoSeguimientoCovid19
�+�+ :
(
�+�+: ;
List
�+�+; ?
<
�+�+? @(
cargue_seguimiento_covid19
�+�+@ Z
>
�+�+Z [
OBJ
�+�+\ _
,
�+�+_ `
ref
�+�+a d 
MessageResponseOBJ
�+�+e w
MsgRes
�+�+x ~
)
�+�+~ 
{
�+�+ 	
return
�+�+ 

DACInserta
�+�+ 
.
�+�+ 3
%InsertarConsolidadoSeguimientoCovid19
�+�+ C
(
�+�+C D
OBJ
�+�+D G
,
�+�+G H
ref
�+�+I L
MsgRes
�+�+M S
)
�+�+S T
;
�+�+T U
}
�+�+ 	
public
�+�+ 
Int32
�+�+ /
!InsertarSeguimientoCovid19Detalle
�+�+ 6
(
�+�+6 70
"cargue_seguimiento_covid19_detalle
�+�+7 Y
OBJ
�+�+Z ]
,
�+�+] ^
ref
�+�+_ b 
MessageResponseOBJ
�+�+c u
MsgRes
�+�+v |
)
�+�+| }
{
�+�+ 	
return
�+�+ 

DACInserta
�+�+ 
.
�+�+ /
!InsertarSeguimientoCovid19Detalle
�+�+ ?
(
�+�+? @
OBJ
�+�+@ C
,
�+�+C D
ref
�+�+E H
MsgRes
�+�+I O
)
�+�+O P
;
�+�+P Q
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ (
cargue_seguimiento_covid19
�+�+ .
>
�+�+. /*
ConsultaIdSeguimientoCovid19
�+�+0 L
(
�+�+L M
Int32
�+�+M R
ID
�+�+S U
,
�+�+U V
ref
�+�+W Z 
MessageResponseOBJ
�+�+[ m
MsgRes
�+�+n t
)
�+�+t u
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ *
ConsultaIdSeguimientoCovid19
�+�+ ;
(
�+�+; <
ID
�+�+< >
,
�+�+> ?
ref
�+�+@ C
MsgRes
�+�+D J
)
�+�+J K
;
�+�+K L
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ ,
vw_seguimiento_covid19_detalle
�+�+ 2
>
�+�+2 31
#ConsultaIdSeguimientoCovid19Detalle
�+�+4 W
(
�+�+W X
Int32
�+�+X ]
ID
�+�+^ `
,
�+�+` a
ref
�+�+b e 
MessageResponseOBJ
�+�+f x
MsgRes
�+�+y 
)�+�+ �
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ 1
#ConsultaIdSeguimientoCovid19Detalle
�+�+ B
(
�+�+B C
ID
�+�+C E
,
�+�+E F
ref
�+�+G J
MsgRes
�+�+K Q
)
�+�+Q R
;
�+�+R S
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ 3
%vw_seguimiento_covid19_ultimo_detalle
�+�+ 9
>
�+�+9 :7
)ConsultaIdSeguimientoCovid19DetalleUltimo
�+�+; d
(
�+�+d e
Int32
�+�+e j
ID
�+�+k m
,
�+�+m n
ref
�+�+o r!
MessageResponseOBJ�+�+s �
MsgRes�+�+� �
)�+�+� �
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ 7
)ConsultaIdSeguimientoCovid19DetalleUltimo
�+�+ H
(
�+�+H I
ID
�+�+I K
,
�+�+K L
ref
�+�+M P
MsgRes
�+�+Q W
)
�+�+W X
;
�+�+X Y
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ (
cargue_seguimiento_covid19
�+�+ .
>
�+�+. /.
 ConsultaDocumentoPacienteCovid19
�+�+0 P
(
�+�+P Q
String
�+�+Q W
ID
�+�+X Z
)
�+�+Z [
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ .
 ConsultaDocumentoPacienteCovid19
�+�+ ?
(
�+�+? @
ID
�+�+@ B
)
�+�+B C
;
�+�+C D
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ (
cargue_seguimiento_covid19
�+�+ .
>
�+�+. /#
ConsultaCargueCovid19
�+�+0 E
(
�+�+E F
ref
�+�+F I 
MessageResponseOBJ
�+�+J \
MsgRes
�+�+] c
)
�+�+c d
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ #
ConsultaCargueCovid19
�+�+ 4
(
�+�+4 5
ref
�+�+5 8
MsgRes
�+�+9 ?
)
�+�+? @
;
�+�+@ A
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ 0
"cargue_seguimiento_covid19_detalle
�+�+ 6
>
�+�+6 7/
!ConsultaDetalleSeguimientoCovid19
�+�+8 Y
(
�+�+Y Z
Int32
�+�+Z _
	id_cargue
�+�+` i
,
�+�+i j
ref
�+�+k n!
MessageResponseOBJ�+�+o �
MsgRes�+�+� �
)�+�+� �
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ /
!ConsultaDetalleSeguimientoCovid19
�+�+ @
(
�+�+@ A
	id_cargue
�+�+A J
,
�+�+J K
ref
�+�+L O
MsgRes
�+�+P V
)
�+�+V W
;
�+�+W X
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ +
vw_seguimiento_covid19_diario
�+�+ 1
>
�+�+1 2/
!ConsultaListadoseguimientoCovid19
�+�+3 T
(
�+�+T U
)
�+�+U V
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ /
!ConsultaListadoseguimientoCovid19
�+�+ @
(
�+�+@ A
)
�+�+A B
;
�+�+B C
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ 0
"vw_seguimiento_covid19_interdiario
�+�+ 6
>
�+�+6 7:
,ConsultaListadoseguimientoInterdiarioCovid19
�+�+8 d
(
�+�+d e
)
�+�+e f
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ :
,ConsultaListadoseguimientoInterdiarioCovid19
�+�+ K
(
�+�+K L
)
�+�+L M
;
�+�+M N
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ 3
%vw_seguimiento_covid19_casos_cerrados
�+�+ 9
>
�+�+9 :7
)ConsultaListadoseguimientoCerradosCovid19
�+�+; d
(
�+�+d e
)
�+�+e f
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ 7
)ConsultaListadoseguimientoCerradosCovid19
�+�+ H
(
�+�+H I
)
�+�+I J
;
�+�+J K
}
�+�+ 	
public
�+�+ 
List
�+�+ 
<
�+�+ &
ref_covid19_tipificacion
�+�+ ,
>
�+�+, -.
 ConsultaListadoTipicacionCovid19
�+�+. N
(
�+�+N O
)
�+�+O P
{
�+�+ 	
return
�+�+ 
DACConsulta
�+�+ 
.
�+�+ .
 ConsultaListadoTipicacionCovid19
�+�+ ?
(
�+�+? @
)
�+�+@ A
;
�+�+A B
}
�+�+ 	
public
�,�, 
List
�,�, 
<
�,�, /
!ref_covid19_tipificacion7_detalle
�,�, 5
>
�,�,5 6/
!ConsultaListadoTipicacion7Covid19
�,�,7 X
(
�,�,X Y
)
�,�,Y Z
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, /
!ConsultaListadoTipicacion7Covid19
�,�, @
(
�,�,@ A
)
�,�,A B
;
�,�,B C
}
�,�, 	
public
�,�, 
void
�,�, 0
"ActualizarEstadoSeguimientoCovid19
�,�, 6
(
�,�,6 7(
cargue_seguimiento_covid19
�,�,7 Q
OBJ2
�,�,R V
,
�,�,V W
ref
�,�,X [ 
MessageResponseOBJ
�,�,\ n
MsgRes
�,�,o u
)
�,�,u v
{
�,�, 	
DACActualiza
�,�, 
.
�,�, 0
"ActualizarEstadoSeguimientoCovid19
�,�, ;
(
�,�,; <
OBJ2
�,�,< @
,
�,�,@ A
ref
�,�,B E
MsgRes
�,�,F L
)
�,�,L M
;
�,�,M N
}
�,�, 	
public
�,�, 
void
�,�, (
Actualizacasocarguecovid19
�,�, .
(
�,�,. /(
cargue_seguimiento_covid19
�,�,/ I
OBJ
�,�,J M
,
�,�,M N
ref
�,�,O R 
MessageResponseOBJ
�,�,S e
MsgRes
�,�,f l
)
�,�,l m
{
�,�, 	
DACActualiza
�,�, 
.
�,�, (
Actualizacasocarguecovid19
�,�, 3
(
�,�,3 4
OBJ
�,�,4 7
,
�,�,7 8
ref
�,�,9 <
MsgRes
�,�,= C
)
�,�,C D
;
�,�,D E
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, '
ref_covid19_estado_asalud
�,�, -
>
�,�,- .)
Consultaestadoasaludcovid19
�,�,/ J
(
�,�,J K
)
�,�,K L
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, )
Consultaestadoasaludcovid19
�,�, :
(
�,�,: ;
)
�,�,; <
;
�,�,< =
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, 4
&vw_seguimiento_covid19_descarga_diaria
�,�, :
>
�,�,: ;7
)ConsultaListadoseguimientodescargaCovid19
�,�,< e
(
�,�,e f
)
�,�,f g
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, 7
)ConsultaListadoseguimientodescargaCovid19
�,�, H
(
�,�,H I
)
�,�,I J
;
�,�,J K
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, 9
+vw_seguimiento_covid19_descarga_interdiaria
�,�, ?
>
�,�,? @B
4ConsultaListadoseguimientointerdiariodescargaCovid19
�,�,A u
(
�,�,u v
)
�,�,v w
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, B
4ConsultaListadoseguimientointerdiariodescargaCovid19
�,�, S
(
�,�,S T
)
�,�,T U
;
�,�,U V
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, <
.vw_seguimiento_covid19_descarga_casos_cerrados
�,�, B
>
�,�,B CD
6ConsultaListadoseguimientoCasosCerradosdescargaCovid19
�,�,D z
(
�,�,z {
)
�,�,{ |
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, D
6ConsultaListadoseguimientoCasosCerradosdescargaCovid19
�,�, U
(
�,�,U V
)
�,�,V W
;
�,�,W X
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, 4
&vw_seguimiento_covid19_general_detalle
�,�, :
>
�,�,: ;4
&Consultageneraldetalleseguimientocovid
�,�,< b
(
�,�,b c
)
�,�,c d
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, 4
&Consultageneraldetalleseguimientocovid
�,�, E
(
�,�,E F
)
�,�,F G
;
�,�,G H
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, 1
#vw_md_tablero_interventoria_general
�,�, 7
>
�,�,7 8$
Getinterventoriagneral
�,�,9 O
(
�,�,O P
)
�,�,P Q
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, $
Getinterventoriagneral
�,�, 5
(
�,�,5 6
)
�,�,6 7
;
�,�,7 8
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, :
,vw_md_tablero_intenventoria_general_detalle1
�,�, @
>
�,�,@ A#
Detalleinterventoria1
�,�,B W
(
�,�,W X
Int32
�,�,X ]
ID
�,�,^ `
)
�,�,` a
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, #
Detalleinterventoria1
�,�, 4
(
�,�,4 5
ID
�,�,5 7
)
�,�,7 8
;
�,�,8 9
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, :
,vw_md_tablero_interventoria_general_detalle2
�,�, @
>
�,�,@ A#
Detalleinterventoria2
�,�,B W
(
�,�,W X
Int32
�,�,X ]
ID
�,�,^ `
)
�,�,` a
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, #
Detalleinterventoria2
�,�, 4
(
�,�,4 5
ID
�,�,5 7
)
�,�,7 8
;
�,�,8 9
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, :
,vw_md_tablero_interventoria_general_detalle3
�,�, @
>
�,�,@ A#
Detalleinterventoria3
�,�,B W
(
�,�,W X
Int32
�,�,X ]
ID
�,�,^ `
)
�,�,` a
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, #
Detalleinterventoria3
�,�, 4
(
�,�,4 5
ID
�,�,5 7
)
�,�,7 8
;
�,�,8 9
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, :
,vw_md_tablero_interventoria_general_detalle4
�,�, @
>
�,�,@ A#
Detalleinterventoria4
�,�,B W
(
�,�,W X
Int32
�,�,X ]
ID
�,�,^ `
)
�,�,` a
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, #
Detalleinterventoria4
�,�, 4
(
�,�,4 5
ID
�,�,5 7
)
�,�,7 8
;
�,�,8 9
}
�,�, 	
public
�,�, 
int
�,�, 3
%InsertarHospitalizacionPrevenibleDtll
�,�, 8
(
�,�,8 92
$ecop_hospitalizacion_prevenible_dtll
�,�,9 ]
obj
�,�,^ a
)
�,�,a b
{
�,�, 	
return
�,�, 

DACInserta
�,�, 
.
�,�, 3
%InsertarHospitalizacionPrevenibleDtll
�,�, C
(
�,�,C D
obj
�,�,D G
)
�,�,G H
;
�,�,H I
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, @
2management_vigilancia_epidemiologica_tableroResult
�,�, F
>
�,�,F G)
GetVigilanciaEpidemiologica
�,�,H c
(
�,�,c d
)
�,�,d e
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, )
GetVigilanciaEpidemiologica
�,�, :
(
�,�,: ;
)
�,�,; <
;
�,�,< =
}
�,�, 	
public
�,�, 
int
�,�, 7
)InsertarVigilanciaEpidemiologica_archivos
�,�, <
(
�,�,< =(
ecop_VE_gestion_documental
�,�,= W
obj
�,�,X [
)
�,�,[ \
{
�,�, 	
return
�,�, 

DACInserta
�,�, 
.
�,�, 7
)InsertarVigilanciaEpidemiologica_archivos
�,�, G
(
�,�,G H
obj
�,�,H K
)
�,�,K L
;
�,�,L M
}
�,�, 	
public
�,�, 
int
�,�, .
 InsertarVigilanciaEpidemiologica
�,�, 3
(
�,�,3 4,
ecop_vigilancia_epidemiologica
�,�,4 R
obj
�,�,S V
)
�,�,V W
{
�,�, 	
return
�,�, 

DACInserta
�,�, 
.
�,�, .
 InsertarVigilanciaEpidemiologica
�,�, >
(
�,�,> ?
obj
�,�,? B
)
�,�,B C
;
�,�,C D
}
�,�, 	
public
�,�, 
Int32
�,�, 4
&InsertarArchivoHospitalziacionEvitable
�,�, ;
(
�,�,; <(
ecop_HE_gestion_documental
�,�,< V
obj
�,�,W Z
)
�,�,Z [
{
�,�, 	
return
�,�, 

DACInserta
�,�, 
.
�,�, 4
&InsertarArchivoHospitalziacionEvitable
�,�, D
(
�,�,D E
obj
�,�,E H
)
�,�,H I
;
�,�,I J
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, !
ref_he_analisisCaso
�,�, '
>
�,�,' ( 
ListAnalisisCasoHE
�,�,) ;
(
�,�,; <
)
�,�,< =
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�,  
ListAnalisisCasoHE
�,�, 1
(
�,�,1 2
)
�,�,2 3
;
�,�,3 4
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, $
ref_he_analisisCaso_si
�,�, *
>
�,�,* +"
ListAnalisisCasoHESi
�,�,, @
(
�,�,@ A
)
�,�,A B
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, "
ListAnalisisCasoHESi
�,�, 3
(
�,�,3 4
)
�,�,4 5
;
�,�,5 6
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, $
ref_he_analisisCaso_no
�,�, *
>
�,�,* +"
ListAnalisisCasoHENo
�,�,, @
(
�,�,@ A
)
�,�,A B
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, "
ListAnalisisCasoHENo
�,�, 3
(
�,�,3 4
)
�,�,4 5
;
�,�,5 6
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, H
:management_hospitalizacionPrevenible_detalle_gestionResult
�,�, N
>
�,�,N O9
+GetHospitalizacionPrevenibleDetalle_gestion
�,�,P {
(
�,�,{ |
int
�,�,| 
idHE�,�,� �
)�,�,� �
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, 9
+GetHospitalizacionPrevenibleDetalle_gestion
�,�, J
(
�,�,J K
idHE
�,�,K O
)
�,�,O P
;
�,�,P Q
}
�,�, 	
public
�,�, (
ecop_HE_gestion_documental
�,�, )!
buscarArchivoHEDtll
�,�,* =
(
�,�,= >
int
�,�,> A
HEDtll
�,�,B H
)
�,�,H I
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, !
buscarArchivoHEDtll
�,�, 2
(
�,�,2 3
HEDtll
�,�,3 9
)
�,�,9 :
;
�,�,: ;
}
�,�, 	
public
�,�, (
ecop_VE_gestion_documental
�,�, )
buscarArchivoVE
�,�,* 9
(
�,�,9 :
int
�,�,: =
idVE
�,�,> B
,
�,�,B C
int
�,�,D G
tipo
�,�,H L
)
�,�,L M
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, 
buscarArchivoVE
�,�, .
(
�,�,. /
idVE
�,�,/ 3
,
�,�,3 4
tipo
�,�,5 9
)
�,�,9 :
;
�,�,: ;
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, @
2management_hospitalizacionPrevenible_reporteResult
�,�, F
>
�,�,F G2
$GetHospitalizacionPrevenible_Reporte
�,�,H l
(
�,�,l m
)
�,�,m n
{
�,�, 	
return
�,�, 
DACConsulta
�,�, 
.
�,�, 2
$GetHospitalizacionPrevenible_Reporte
�,�, C
(
�,�,C D
)
�,�,D E
;
�,�,E F
}
�,�, 	
public
�,�, 
List
�,�, 
<
�,�, $
vw_md_datos_comunicado
�,�, *
>
�,�,* + 
GetDatosComunicado
�,�,, >
(
�,�,> ?
)
�,�,? @
{
�,�, 	
return
�-�- 
DACConsulta
�-�- 
.
�-�-  
GetDatosComunicado
�-�- 1
(
�-�-1 2
)
�-�-2 3
;
�-�-3 4
}
�-�- 	
public
�-�- 
List
�-�- 
<
�-�- 
md_comunicaciones
�-�- %
>
�-�-% &'
TraerdocumentoComunicados
�-�-' @
(
�-�-@ A
Int32
�-�-A F
ID
�-�-G I
)
�-�-I J
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- '
TraerdocumentoComunicados
�-�- 8
(
�-�-8 9
ID
�-�-9 ;
)
�-�-; <
;
�-�-< =
}
�-�- 	
public
�-�- .
 GestionDocumentalMedicamentosCad
�-�- /$
Traerimagenindicacioes
�-�-0 F
(
�-�-F G
Int32
�-�-G L
ID
�-�-M O
)
�-�-O P
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- $
Traerimagenindicacioes
�-�- 5
(
�-�-5 6
ID
�-�-6 8
)
�-�-8 9
;
�-�-9 :
}
�-�- 	
public
�-�- 
List
�-�- 
<
�-�- +
vw_md_consolidado_sin_auditor
�-�- 1
>
�-�-1 2!
Getfactursinauditor
�-�-3 F
(
�-�-F G
)
�-�-G H
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- !
Getfactursinauditor
�-�- 2
(
�-�-2 3
)
�-�-3 4
;
�-�-4 5
}
�-�- 	
public
�-�- 0
"ManagmentIngresarIncapacidadResult
�-�- 1*
GetAsignarAuditorConsolidado
�-�-2 N
(
�-�-N O
String
�-�-O U
factura
�-�-V ]
,
�-�-] ^
ref
�-�-_ b 
MessageResponseOBJ
�-�-c u
MsgRes
�-�-v |
)
�-�-| }
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- *
GetAsignarAuditorConsolidado
�-�- ;
(
�-�-; <
factura
�-�-< C
,
�-�-C D
ref
�-�-E H
MsgRes
�-�-I O
)
�-�-O P
;
�-�-P Q
}
�-�- 	
public
�-�- 
List
�-�- 
<
�-�- :
,managmentprestadoresFacturasRechazadasResult
�-�- @
>
�-�-@ A#
GetFacturasRechazadas
�-�-B W
(
�-�-W X
string
�-�-X ^
factura
�-�-_ f
,
�-�-f g
ref
�-�-h k 
MessageResponseOBJ
�-�-l ~
MsgRes�-�- �
)�-�-� �
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- #
GetFacturasRechazadas
�-�- 4
(
�-�-4 5
factura
�-�-5 <
,
�-�-< =
ref
�-�-> A
MsgRes
�-�-B H
)
�-�-H I
;
�-�-I J
}
�-�- 	
public
�-�- 
void
�-�- (
InsertarCargueContratacion
�-�- .
(
�-�-. /&
contratacion_cargue_base
�-�-/ G
obj
�-�-H K
,
�-�-K L
List
�-�-M Q
<
�-�-Q R&
contratacion_cargue_dtll
�-�-R j
>
�-�-j k
ListaContratacion
�-�-l }
,
�-�-} ~
ref�-�- �"
MessageResponseOBJ�-�-� �
MsgRes�-�-� �
)�-�-� �
{
�-�- 	

DACInserta
�-�- 
.
�-�- (
InsertarCargueContratacion
�-�- 1
(
�-�-1 2
obj
�-�-2 5
,
�-�-5 6
ListaContratacion
�-�-7 H
,
�-�-H I
ref
�-�-J M
MsgRes
�-�-N T
)
�-�-T U
;
�-�-U V
}
�-�- 	
public
�-�- &
contratacion_cargue_base
�-�- '#
getcarguecontratacion
�-�-( =
(
�-�-= >
int
�-�-> A
mes
�-�-B E
,
�-�-E F
int
�-�-G J
año
�-�-K N
)
�-�-N O
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- #
getcarguecontratacion
�-�- 4
(
�-�-4 5
mes
�-�-5 8
,
�-�-8 9
año
�-�-: =
)
�-�-= >
;
�-�-> ?
}
�-�- 	
public
�-�- -
ecop_gestion_facturas_aprobadas
�-�- ."
GetFacturasAprobadas
�-�-/ C
(
�-�-C D
int
�-�-D G
id_cargue_dtll
�-�-H V
)
�-�-V W
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- "
GetFacturasAprobadas
�-�- 3
(
�-�-3 4
id_cargue_dtll
�-�-4 B
)
�-�-B C
;
�-�-C D
}
�-�- 	
public
�-�- 
List
�-�- 
<
�-�- 5
'managementFacturasanalistas_lotesResult
�-�- ;
>
�-�-; <!
GetFacturaAnalistas
�-�-= P
(
�-�-P Q
String
�-�-Q W
usuario
�-�-X _
,
�-�-_ `
ref
�-�-a d 
MessageResponseOBJ
�-�-e w
MsgRes
�-�-x ~
)
�-�-~ 
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- !
GetFacturaAnalistas
�-�- 2
(
�-�-2 3
usuario
�-�-3 :
,
�-�-: ;
ref
�-�-< ?
MsgRes
�-�-@ F
)
�-�-F G
;
�-�-G H
}
�-�- 	
public
�-�- 
List
�-�- 
<
�-�- 8
*managementFacturasanalistas_lotes_okResult
�-�- >
>
�-�-> ?#
GetFacturaAnalistasok
�-�-@ U
(
�-�-U V
ref
�-�-V Y 
MessageResponseOBJ
�-�-Z l
MsgRes
�-�-m s
)
�-�-s t
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- #
GetFacturaAnalistasok
�-�- 4
(
�-�-4 5
ref
�-�-5 8
MsgRes
�-�-9 ?
)
�-�-? @
;
�-�-@ A
}
�-�- 	
public
�-�- 
List
�-�- 
<
�-�- 9
+Management_Lotes_totales_con_analistaResult
�-�- ?
>
�-�-? @#
GetLotesAnalistaTotal
�-�-A V
(
�-�-V W
DateTime
�-�-W _
fecha_inicio
�-�-` l
,
�-�-l m
DateTime
�-�-n v
	fecha_fin�-�-w �
,�-�-� �
ref�-�-� �"
MessageResponseOBJ�-�-� �
MsgRes�-�-� �
)�-�-� �
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- #
GetLotesAnalistaTotal
�-�- 4
(
�-�-4 5
fecha_inicio
�-�-5 A
,
�-�-A B
	fecha_fin
�-�-C L
,
�-�-L M
ref
�-�-N Q
MsgRes
�-�-R X
)
�-�-X Y
;
�-�-Y Z
}
�-�- 	
public
�-�- 
List
�-�- 
<
�-�- =
/Management_Lotes_totales_con_analistaDtllResult
�-�- C
>
�-�-C D'
GetLotesAnalistaTotalDtll
�-�-E ^
(
�-�-^ _
Int32
�-�-_ d
Id
�-�-e g
,
�-�-g h
ref
�-�-i l 
MessageResponseOBJ
�-�-m 
MsgRes�-�-� �
)�-�-� �
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- '
GetLotesAnalistaTotalDtll
�-�- 8
(
�-�-8 9
Id
�-�-9 ;
,
�-�-; <
ref
�-�-= @
MsgRes
�-�-A G
)
�-�-G H
;
�-�-H I
}
�-�- 	
public
�-�- 
Int32
�-�- "
InsertarFirmadigital
�-�- )
(
�-�-) *+
ecop_firma_digital_cod_barras
�-�-* G
obj
�-�-H K
,
�-�-K L
ref
�-�-M P 
MessageResponseOBJ
�-�-Q c
MsgRes
�-�-d j
)
�-�-j k
{
�-�- 	
return
�-�- 

DACInserta
�-�- 
.
�-�- "
InsertarFirmadigital
�-�- 2
(
�-�-2 3
obj
�-�-3 6
,
�-�-6 7
ref
�-�-8 ;
MsgRes
�-�-< B
)
�-�-B C
;
�-�-C D
}
�-�- 	
public
�-�- +
ecop_firma_digital_cod_barras
�-�- ,
GetDtll_codBarras
�-�-- >
(
�-�-> ?
Int32
�-�-? D
?
�-�-D E
	idDetalle
�-�-F O
)
�-�-O P
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- 
GetDtll_codBarras
�-�- 0
(
�-�-0 1
	idDetalle
�-�-1 :
)
�-�-: ;
;
�-�-; <
}
�-�- 	
public
�-�- *
Management_consulta_QRResult
�-�- +
GetConsultaQr
�-�-, 9
(
�-�-9 :
Int32
�-�-: ?
?
�-�-? @
	idDetalle
�-�-A J
)
�-�-J K
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- 
GetConsultaQr
�-�- ,
(
�-�-, -
	idDetalle
�-�-- 6
)
�-�-6 7
;
�-�-7 8
}
�-�- 	
public
�-�- 
Int32
�-�- &
InsertarFirmadigitalsami
�-�- -
(
�-�-- .%
ecop_firma_digital_sami
�-�-. E
obj
�-�-F I
,
�-�-I J
ref
�-�-K N 
MessageResponseOBJ
�-�-O a
MsgRes
�-�-b h
)
�-�-h i
{
�-�- 	
return
�-�- 

DACInserta
�-�- 
.
�-�- &
InsertarFirmadigitalsami
�-�- 6
(
�-�-6 7
obj
�-�-7 :
,
�-�-: ;
ref
�-�-< ?
MsgRes
�-�-@ F
)
�-�-F G
;
�-�-G H
}
�-�- 	
public
�-�- 
List
�-�- 
<
�-�- 7
)vw_odontologia_detallado_historia_clinica
�-�- =
>
�-�-= >'
getdetallehistoriaclinica
�-�-? X
(
�-�-X Y
)
�-�-Y Z
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- '
getdetallehistoriaclinica
�-�- 8
(
�-�-8 9
)
�-�-9 :
;
�-�-: ;
}
�-�- 	
public
�-�- 
Int32
�-�- +
InsertarGestionFacturadigital
�-�- 2
(
�-�-2 3*
ecop_gestion_factura_digital
�-�-3 O
obj
�-�-P S
,
�-�-S T
ref
�-�-U X 
MessageResponseOBJ
�-�-Y k
MsgRes
�-�-l r
)
�-�-r s
{
�-�- 	
return
�-�- 

DACInserta
�-�- 
.
�-�- +
InsertarGestionFacturadigital
�-�- ;
(
�-�-; <
obj
�-�-< ?
,
�-�-? @
ref
�-�-A D
MsgRes
�-�-E K
)
�-�-K L
;
�-�-L M
}
�-�- 	
public
�-�- 
Int32
�-�- 0
"InsertarGestionFacturadigitalGasto
�-�- 7
(
�-�-7 80
"ecop_gestion_factura_digital_gasto
�-�-8 Z
obj
�-�-[ ^
,
�-�-^ _
ref
�-�-` c 
MessageResponseOBJ
�-�-d v
MsgRes
�-�-w }
)
�-�-} ~
{
�-�- 	
return
�-�- 

DACInserta
�-�- 
.
�-�- 0
"InsertarGestionFacturadigitalGasto
�-�- @
(
�-�-@ A
obj
�-�-A D
,
�-�-D E
ref
�-�-F I
MsgRes
�-�-J P
)
�-�-P Q
;
�-�-Q R
}
�-�- 	
public
�-�- 
void
�-�- 7
)insertarListadoGestionFacturadigitalGasto
�-�- =
(
�-�-= >
List
�-�-> B
<
�-�-B C0
"ecop_gestion_factura_digital_gasto
�-�-C e
>
�-�-e f
obj
�-�-g j
,
�-�-j k
ref
�-�-l o!
MessageResponseOBJ�-�-p �
MsgRes�-�-� �
)�-�-� �
{
�-�- 	

DACInserta
�-�- 
.
�-�- 7
)insertarListadoGestionFacturadigitalGasto
�-�- @
(
�-�-@ A
obj
�-�-A D
,
�-�-D E
ref
�-�-F I
MsgRes
�-�-J P
)
�-�-P Q
;
�-�-Q R
}
�-�- 	
public
�-�- 
void
�-�- 2
$ActualizarGestionFacturadigitalGasto
�-�- 8
(
�-�-8 9,
vw_factura_digital_gasto_total
�-�-9 W
obj
�-�-X [
,
�-�-[ \
ref
�-�-] ` 
MessageResponseOBJ
�-�-a s
MsgRes
�-�-t z
)
�-�-z {
{
�-�- 	
DACActualiza
�-�- 
.
�-�- 2
$ActualizarGestionFacturadigitalGasto
�-�- =
(
�-�-= >
obj
�-�-> A
,
�-�-A B
ref
�-�-C F
MsgRes
�-�-G M
)
�-�-M N
;
�-�-N O
}
�-�- 	
public
�-�- 
List
�-�- 
<
�-�- %
ref_tipo_gasto_facturas
�-�- +
>
�-�-+ ,(
Getref_tipo_gasto_facturas
�-�-- G
(
�-�-G H
ref
�-�-H K 
MessageResponseOBJ
�-�-L ^
MsgRes
�-�-_ e
)
�-�-e f
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- (
Getref_tipo_gasto_facturas
�-�- 9
(
�-�-9 :
ref
�-�-: =
MsgRes
�-�-> D
)
�-�-D E
;
�-�-E F
}
�-�- 	
public
�-�- %
ecop_firma_digital_sami
�-�- &
	GetFirmas
�-�-' 0
(
�-�-0 1
Int32
�-�-1 6
?
�-�-6 7
	idusuario
�-�-8 A
)
�-�-A B
{
�-�- 	
return
�-�- 
DACComonClass
�-�-  
.
�-�-  !
	GetFirmas
�-�-! *
(
�-�-* +
	idusuario
�-�-+ 4
)
�-�-4 5
;
�-�-5 6
}
�-�- 	
public
�-�- 6
(management_ecop_firma_digital_samiResult
�-�- 7
GetFirmasId
�-�-8 C
(
�-�-C D
int
�-�-D G
?
�-�-G H
	idUsuario
�-�-I R
)
�-�-R S
{
�-�- 	
return
�-�- 
DACComonClass
�-�-  
.
�-�-  !
GetFirmasId
�-�-! ,
(
�-�-, -
	idUsuario
�-�-- 6
)
�-�-6 7
;
�-�-7 8
}
�-�- 	
public
�-�- %
ecop_firma_digital_sami
�-�- &
traerDatosFirma
�-�-' 6
(
�-�-6 7
int
�-�-7 :
?
�-�-: ;
	idUsuario
�-�-< E
)
�-�-E F
{
�-�- 	
return
�-�- 
DACConsulta
�-�- 
.
�-�- 
traerDatosFirma
�-�- .
(
�-�-. /
	idUsuario
�-�-/ 8
)
�-�-8 9
;
�-�-9 :
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. ;
-management_ecop_firma_digital_sami_todoResult
�.�. A
>
�.�.A B"
ListadoFirmasSinRuta
�.�.C W
(
�.�.W X
)
�.�.X Y
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. "
ListadoFirmasSinRuta
�.�. 3
(
�.�.3 4
)
�.�.4 5
;
�.�.5 6
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. %
ecop_firma_digital_sami
�.�. +
>
�.�.+ ,!
listaFirmasUsuarios
�.�.- @
(
�.�.@ A
)
�.�.A B
{
�.�. 	
return
�.�. 
DACComonClass
�.�.  
.
�.�.  !!
listaFirmasUsuarios
�.�.! 4
(
�.�.4 5
)
�.�.5 6
;
�.�.6 7
}
�.�. 	
public
�.�. 
void
�.�. )
ActualizarRutaFirmasDigital
�.�. /
(
�.�./ 0
string
�.�.0 6
ruta
�.�.7 ;
,
�.�.; <
int
�.�.= @
?
�.�.@ A
idFirma
�.�.B I
)
�.�.I J
{
�.�. 	
DACActualiza
�.�. 
.
�.�. )
ActualizarRutaFirmasDigital
�.�. 4
(
�.�.4 5
ruta
�.�.5 9
,
�.�.9 :
idFirma
�.�.; B
)
�.�.B C
;
�.�.C D
}
�.�. 	
public
�.�. 
int
�.�. 
cantidaddias
�.�. 
(
�.�.  
int
�.�.  #
idconcurrencia
�.�.$ 2
)
�.�.2 3
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. 
cantidaddias
�.�. +
(
�.�.+ ,
idconcurrencia
�.�., :
)
�.�.: ;
;
�.�.; <
}
�.�. 	
public
�.�. 
void
�.�. +
ActualizarAuditorConcurrencia
�.�. 1
(
�.�.1 2
ecop_concurrencia
�.�.2 C
OBJ
�.�.D G
,
�.�.G H
ref
�.�.I L 
MessageResponseOBJ
�.�.M _
MsgRes
�.�.` f
)
�.�.f g
{
�.�. 	
DACActualiza
�.�. 
.
�.�. +
ActualizarAuditorConcurrencia
�.�. 6
(
�.�.6 7
OBJ
�.�.7 :
,
�.�.: ;
ref
�.�.< ?
MsgRes
�.�.@ F
)
�.�.F G
;
�.�.G H
}
�.�. 	
public
�.�. 
void
�.�. $
ActualizarAuditorCenso
�.�. *
(
�.�.* +

ecop_censo
�.�.+ 5
OBJ
�.�.6 9
,
�.�.9 :
ref
�.�.; > 
MessageResponseOBJ
�.�.? Q
MsgRes
�.�.R X
)
�.�.X Y
{
�.�. 	
DACActualiza
�.�. 
.
�.�. $
ActualizarAuditorCenso
�.�. /
(
�.�./ 0
OBJ
�.�.0 3
,
�.�.3 4
ref
�.�.5 8
MsgRes
�.�.9 ?
)
�.�.? @
;
�.�.@ A
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. 5
'ManagmentDetalleFacturasDevueltasResult
�.�. ;
>
�.�.; <(
GetDetalleFacturadevuletas
�.�.= W
(
�.�.W X
int
�.�.X [

id_detalle
�.�.\ f
)
�.�.f g
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. (
GetDetalleFacturadevuletas
�.�. 9
(
�.�.9 :

id_detalle
�.�.: D
)
�.�.D E
;
�.�.E F
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. &
view_ref_estado_facturas
�.�. ,
>
�.�., -
GetEstadoFacturas
�.�.. ?
(
�.�.? @
)
�.�.@ A
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. 
GetEstadoFacturas
�.�. 0
(
�.�.0 1
)
�.�.1 2
;
�.�.2 3
}
�.�. 	
public
�.�. 
Int32
�.�. 2
$InsertarLogCambiosGetionHospitalaria
�.�. 9
(
�.�.9 :.
 log_cambios_gestion_hospitalaria
�.�.: Z
obj
�.�.[ ^
,
�.�.^ _
ref
�.�.` c 
MessageResponseOBJ
�.�.d v
MsgRes
�.�.w }
)
�.�.} ~
{
�.�. 	
return
�.�. 

DACInserta
�.�. 
.
�.�. 2
$InsertarLogCambiosGetionHospitalaria
�.�. B
(
�.�.B C
obj
�.�.C F
,
�.�.F G
ref
�.�.H K
MsgRes
�.�.L R
)
�.�.R S
;
�.�.S T
}
�.�. 	
public
�.�. 
void
�.�. ,
ActualizarCambiosPacienteCenso
�.�. 2
(
�.�.2 3

ecop_censo
�.�.3 =
OBJ
�.�.> A
,
�.�.A B
String
�.�.C I

tipocambio
�.�.J T
,
�.�.T U
ref
�.�.V Y 
MessageResponseOBJ
�.�.Z l
MsgRes
�.�.m s
)
�.�.s t
{
�.�. 	
DACActualiza
�.�. 
.
�.�. ,
ActualizarCambiosPacienteCenso
�.�. 7
(
�.�.7 8
OBJ
�.�.8 ;
,
�.�.; <

tipocambio
�.�.= G
,
�.�.G H
ref
�.�.I L
MsgRes
�.�.M S
)
�.�.S T
;
�.�.T U
}
�.�. 	
public
�.�. 
void
�.�. ,
ActualizarCambiosPacienteConcu
�.�. 2
(
�.�.2 3
ecop_concurrencia
�.�.3 D
OBJ
�.�.E H
,
�.�.H I
String
�.�.J P

tipocambio
�.�.Q [
,
�.�.[ \
ref
�.�.] ` 
MessageResponseOBJ
�.�.a s
MsgRes
�.�.t z
)
�.�.z {
{
�.�. 	
DACActualiza
�.�. 
.
�.�. ,
ActualizarCambiosPacienteConcu
�.�. 7
(
�.�.7 8
OBJ
�.�.8 ;
,
�.�.; <

tipocambio
�.�.= G
,
�.�.G H
ref
�.�.I L
MsgRes
�.�.M S
)
�.�.S T
;
�.�.T U
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. 5
'management_egresos_categorizacionResult
�.�. ;
>
�.�.; <*
listadoEgresosCategorizacion
�.�.= Y
(
�.�.Y Z
int
�.�.Z ]
idConcurrencia
�.�.^ l
)
�.�.l m
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. *
listadoEgresosCategorizacion
�.�. ;
(
�.�.; <
idConcurrencia
�.�.< J
)
�.�.J K
;
�.�.K L
}
�.�. 	
public
�.�. 
Int32
�.�. &
InsertarFacturaAprobadas
�.�. -
(
�.�.- .-
ecop_gestion_facturas_aprobadas
�.�.. M
obj
�.�.N Q
,
�.�.Q R
ref
�.�.S V 
MessageResponseOBJ
�.�.W i
MsgRes
�.�.j p
)
�.�.p q
{
�.�. 	
return
�.�. 

DACInserta
�.�. 
.
�.�. &
InsertarFacturaAprobadas
�.�. 6
(
�.�.6 7
obj
�.�.7 :
,
�.�.: ;
ref
�.�.< ?
MsgRes
�.�.@ F
)
�.�.F G
;
�.�.G H
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. $
vw_analistas_recepcion
�.�. *
>
�.�.* +
GetListAnalistas
�.�., <
(
�.�.< =
)
�.�.= >
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. 
GetListAnalistas
�.�. /
(
�.�./ 0
)
�.�.0 1
;
�.�.1 2
}
�.�. 	
public
�.�. 
void
�.�. "
Insertaranalistalote
�.�. (
(
�.�.( )
ref_analista_lote
�.�.) :
obj
�.�.; >
,
�.�.> ?
ref
�.�.@ C 
MessageResponseOBJ
�.�.D V
MsgRes
�.�.W ]
)
�.�.] ^
{
�.�. 	

DACInserta
�.�. 
.
�.�. "
Insertaranalistalote
�.�. +
(
�.�.+ ,
obj
�.�., /
,
�.�./ 0
ref
�.�.1 4
MsgRes
�.�.5 ;
)
�.�.; <
;
�.�.< =
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. 3
%managmentprestadoresFacturasOBSResult
�.�. 9
>
�.�.9 :#
GetConsultaObsFactura
�.�.; P
(
�.�.P Q
Int32
�.�.Q V
?
�.�.V W
id_af
�.�.X ]
)
�.�.] ^
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. #
GetConsultaObsFactura
�.�. 4
(
�.�.4 5
id_af
�.�.5 :
)
�.�.: ;
;
�.�.; <
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. 8
*managmentprestadoresfacturasDEV_RECHResult
�.�. >
>
�.�.> ?'
GetConsultaRechDevFactura
�.�.@ Y
(
�.�.Y Z
)
�.�.Z [
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. '
GetConsultaRechDevFactura
�.�. 8
(
�.�.8 9
)
�.�.9 :
;
�.�.: ;
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. :
,managmentprestadoresfacturasDEV_RECHV2Result
�.�. @
>
�.�.@ A)
GetConsultaRechDevFacturaV2
�.�.B ]
(
�.�.] ^
int
�.�.^ a
?
�.�.a b
	idfactura
�.�.c l
)
�.�.l m
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. )
GetConsultaRechDevFacturaV2
�.�. :
(
�.�.: ;
	idfactura
�.�.; D
)
�.�.D E
;
�.�.E F
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. 6
(getfacturabynumfactura_idprestadorResult
�.�. <
>
�.�.< =&
ValidarEvistenciaFactura
�.�.> V
(
�.�.V W
int
�.�.W Z
	idfactura
�.�.[ d
,
�.�.d e
string
�.�.f l
numnuevofactura
�.�.m |
,
�.�.| }
string�.�.~ �
numidprestador�.�.� �
)�.�.� �
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. &
ValidarEvistenciaFactura
�.�. 7
(
�.�.7 8
	idfactura
�.�.8 A
,
�.�.A B
numnuevofactura
�.�.C R
,
�.�.R S
numidprestador
�.�.T b
)
�.�.b c
;
�.�.c d
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. *
ecop_gestion_factura_digital
�.�. 0
>
�.�.0 1'
GetConsultaGestionFactura
�.�.2 K
(
�.�.K L
Int32
�.�.L Q
?
�.�.Q R
	idDetalle
�.�.S \
)
�.�.\ ]
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. '
GetConsultaGestionFactura
�.�. 8
(
�.�.8 9
	idDetalle
�.�.9 B
)
�.�.B C
;
�.�.C D
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�.  
factura_devolucion
�.�. &
>
�.�.& '*
GetConsultaGestionDevolucion
�.�.( D
(
�.�.D E
Int32
�.�.E J
?
�.�.J K
	idDetalle
�.�.L U
)
�.�.U V
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. *
GetConsultaGestionDevolucion
�.�. ;
(
�.�.; <
	idDetalle
�.�.< E
)
�.�.E F
;
�.�.F G
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. 9
+managmentprestadoresfacturasACEP_ASIGResult
�.�. ?
>
�.�.? @)
GetConsultaAcep_AsigFactura
�.�.A \
(
�.�.\ ]
)
�.�.] ^
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. )
GetConsultaAcep_AsigFactura
�.�. :
(
�.�.: ;
)
�.�.; <
;
�.�.< =
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�. 5
'managmentprestadoresNumeroFacturaResult
�.�. ;
>
�.�.; <&
GetConsultaNumeroFactura
�.�.= U
(
�.�.U V
String
�.�.V \
	numeroFac
�.�.] f
)
�.�.f g
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. &
GetConsultaNumeroFactura
�.�. 7
(
�.�.7 8
	numeroFac
�.�.8 A
)
�.�.A B
;
�.�.B C
}
�.�. 	
public
�.�. 
List
�.�. 
<
�.�.  
factura_devolucion
�.�. &
>
�.�.& '-
GetfacturadevolucionByIdFactura
�.�.( G
(
�.�.G H
int
�.�.H K
	idfactura
�.�.L U
)
�.�.U V
{
�.�. 	
return
�.�. 
DACConsulta
�.�. 
.
�.�. -
GetfacturadevolucionByIdFactura
�.�. >
(
�.�.> ?
	idfactura
�.�.? H
)
�.�.H I
;
�.�.I J
}
�.�. 	
public
�.�. 
Int32
�.�. .
 InsertarFacturacionContabilizado
�.�. 5
(
�.�.5 6
List
�.�.6 :
<
�.�.: ;9
+ecop_gestion_factura_digital_contabilizados
�.�.; f
>
�.�.f g

OBJDetalle
�.�.h r
,
�.�.r s
ref
�.�.t w!
MessageResponseOBJ�.�.x �
MsgRes�.�.� �
)�.�.� �
{
�.�. 	
return
�.�. 

DACInserta
�.�. 
.
�.�. .
 InsertarFacturacionContabilizado
�.�. >
(
�.�.> ?

OBJDetalle
�.�.? I
,
�.�.I J
ref
�.�.K N
MsgRes
�.�.O U
)
�.�.U V
;
�.�.V W
}
�.�. 	
public
�.�. 
Int32
�.�. $
InsertarControlCambios
�.�. +
(
�.�.+ ,:
,ecop_gestion_factura_digital_control_cambios
�.�., X
OBJ
�.�.Y \
,
�.�.\ ]
ref
�.�.^ a 
MessageResponseOBJ
�.�.b t
MsgRes
�.�.u {
)
�.�.{ |
{
�.�. 	
return
�.�. 

DACInserta
�.�. 
.
�.�. $
InsertarControlCambios
�.�. 4
(
�.�.4 5
OBJ
�.�.5 8
,
�.�.8 9
ref
�.�.: =
MsgRes
�.�.> D
)
�.�.D E
;
�.�.E F
}
�.�. 	
public
�.�. 
int
�.�. '
ActualizarEstado_Facturas
�.�. ,
(
�.�., -
int
�.�.- 0
idFac
�.�.1 6
,
�.�.6 7
int
�.�.8 ;
estadoAntiguo
�.�.< I
,
�.�.I J
int
�.�.K N
estadoNuevo
�.�.O Z
)
�.�.Z [
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  '
ActualizarEstado_Facturas
�/�/  9
(
�/�/9 :
idFac
�/�/: ?
,
�/�/? @
estadoAntiguo
�/�/A N
,
�/�/N O
estadoNuevo
�/�/P [
)
�/�/[ \
;
�/�/\ ]
}
�/�/ 	
public
�/�/ 
List
�/�/ 
<
�/�/ $
md_prefacturas_detalle
�/�/ *
>
�/�/* +$
GetPrefacturasByIdLote
�/�/, B
(
�/�/B C
int
�/�/C F
lote
�/�/G K
)
�/�/K L
{
�/�/ 	
return
�/�/ 
DACConsulta
�/�/ 
.
�/�/ $
GetPrefacturasByIdLote
�/�/ 5
(
�/�/5 6
lote
�/�/6 :
)
�/�/: ;
;
�/�/; <
}
�/�/ 	
public
�/�/ =
/management_prefacturas_existeBeneficiarioResult
�/�/ >+
PrefacturasExisteBeneficiario
�/�/? \
(
�/�/\ ]
string
�/�/] c 
numeroBeneficiario
�/�/d v
,
�/�/v w
DateTime�/�/x �$
fechaDespachoFormula�/�/� �
)�/�/� �
{
�/�/ 	
return
�/�/ 
DACConsulta
�/�/ 
.
�/�/ +
PrefacturasExisteBeneficiario
�/�/ <
(
�/�/< = 
numeroBeneficiario
�/�/= O
,
�/�/O P"
fechaDespachoFormula
�/�/Q e
)
�/�/e f
;
�/�/f g
}
�/�/ 	
public
�/�/ 
string
�/�/ &
PrefacturasExisteFactura
�/�/ .
(
�/�/. /
string
�/�// 5 
numeroBeneficiario
�/�/6 H
,
�/�/H I
int
�/�/J M
numeroUnidades
�/�/N \
,
�/�/\ ]
DateTime
�/�/^ f"
fechaDespachoFormula
�/�/g {
,
�/�/{ |
decimal�/�/} �
vlrUnidades�/�/� �
,
�/�/ 
string
�/�/ 
cum
�/�/ 
,
�/�/ 
string
�/�/  
nombreComercial
�/�/! 0
)
�/�/0 1
{
�/�/ 	
return
�/�/ 
DACConsulta
�/�/ 
.
�/�/ &
PrefacturasExisteFactura
�/�/ 7
(
�/�/7 8 
numeroBeneficiario
�/�/8 J
,
�/�/J K
numeroUnidades
�/�/L Z
,
�/�/Z ["
fechaDespachoFormula
�/�/\ p
,
�/�/p q
vlrUnidades
�/�/r }
,
�/�/} ~
cum�/�/ �
,�/�/� �
nombreComercial�/�/� �
)�/�/� �
;�/�/� �
}
�/�/ 	
public
�/�/ ?
1management_prefacturas_regionalBeneficiarioResult
�/�/ @-
PrefacturasRegionalBeneficiario
�/�/A `
(
�/�/` a
string
�/�/a g 
numeroBeneficiario
�/�/h z
,
�/�/z {
DateTime�/�/| �$
fechaDespachoFormula�/�/� �
,�/�/� �
string�/�/� �
nombreEspecial�/�/� �
)�/�/� �
{
�/�/ 	
return
�/�/ 
DACConsulta
�/�/ 
.
�/�/ -
PrefacturasRegionalBeneficiario
�/�/ >
(
�/�/> ? 
numeroBeneficiario
�/�/? Q
,
�/�/Q R"
fechaDespachoFormula
�/�/S g
,
�/�/g h
nombreEspecial
�/�/i w
)
�/�/w x
;
�/�/x y
}
�/�/ 	
public
�/�/ 
void
�/�/ (
ActualizarPrefacturaCargue
�/�/ .
(
�/�/. /
int
�/�// 2
?
�/�/2 3

cargueBase
�/�/4 >
,
�/�/> ?
int
�/�/@ C
?
�/�/C D
fase
�/�/E I
,
�/�/I J
string
�/�/K Q
usuario
�/�/R Y
,
�/�/Y Z
int
�/�/[ ^
?
�/�/^ _
	terminado
�/�/` i
)
�/�/i j
{
�/�/ 	
DACActualiza
�/�/ 
.
�/�/ (
ActualizarPrefacturaCargue
�/�/ 3
(
�/�/3 4

cargueBase
�/�/4 >
,
�/�/> ?
fase
�/�/@ D
,
�/�/D E
usuario
�/�/F M
,
�/�/M N
	terminado
�/�/O X
)
�/�/X Y
;
�/�/Y Z
}
�/�/ 	
public
�/�/ 
void
�/�/ ,
ActualizarPrefacturaCargueFase
�/�/ 2
(
�/�/2 3
int
�/�/3 6
?
�/�/6 7

cargueBase
�/�/8 B
,
�/�/B C
int
�/�/D G
?
�/�/G H
fase
�/�/I M
,
�/�/M N
string
�/�/O U
usuario
�/�/V ]
)
�/�/] ^
{
�/�/ 	
DACActualiza
�/�/ 
.
�/�/ ,
ActualizarPrefacturaCargueFase
�/�/ 7
(
�/�/7 8

cargueBase
�/�/8 B
,
�/�/B C
fase
�/�/D H
,
�/�/H I
usuario
�/�/J Q
)
�/�/Q R
;
�/�/R S
}
�/�/ 	
public
�/�/ 
int
�/�/ 4
&ActualizarPrefacturaCargueFaseDevolver
�/�/ 9
(
�/�/9 :
int
�/�/: =
?
�/�/= >

cargueBase
�/�/? I
)
�/�/I J
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  4
&ActualizarPrefacturaCargueFaseDevolver
�/�/  F
(
�/�/F G

cargueBase
�/�/G Q
)
�/�/Q R
;
�/�/R S
}
�/�/ 	
public
�/�/ 
int
�/�/ -
ActualizarConteo_Facturas_fase1
�/�/ 2
(
�/�/2 3
int
�/�/3 6
idCargue
�/�/7 ?
,
�/�/? @
string
�/�/A G
usuarioDigita
�/�/H U
,
�/�/U V
ref
�/�/W Z 
MessageResponseOBJ
�/�/[ m
MsgRes
�/�/n t
)
�/�/t u
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  -
ActualizarConteo_Facturas_fase1
�/�/  ?
(
�/�/? @
idCargue
�/�/@ H
,
�/�/H I
usuarioDigita
�/�/J W
,
�/�/W X
ref
�/�/Y \
MsgRes
�/�/] c
)
�/�/c d
;
�/�/d e
}
�/�/ 	
public
�/�/ 
int
�/�/ -
ActualizarConteo_Facturas_fase2
�/�/ 2
(
�/�/2 3
int
�/�/3 6
idCargue
�/�/7 ?
,
�/�/? @
string
�/�/A G
usuarioDigita
�/�/H U
,
�/�/U V
ref
�/�/W Z 
MessageResponseOBJ
�/�/[ m
MsgRes
�/�/n t
)
�/�/t u
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  -
ActualizarConteo_Facturas_fase2
�/�/  ?
(
�/�/? @
idCargue
�/�/@ H
,
�/�/H I
usuarioDigita
�/�/J W
,
�/�/W X
ref
�/�/Y \
MsgRes
�/�/] c
)
�/�/c d
;
�/�/d e
}
�/�/ 	
public
�/�/ 
int
�/�/ /
!ActualizarConteo_Facturas_fase2_2
�/�/ 4
(
�/�/4 5
int
�/�/5 8
idCargue
�/�/9 A
,
�/�/A B
string
�/�/C I
usuarioDigita
�/�/J W
,
�/�/W X
ref
�/�/Y \ 
MessageResponseOBJ
�/�/] o
MsgRes
�/�/p v
)
�/�/v w
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  /
!ActualizarConteo_Facturas_fase2_2
�/�/  A
(
�/�/A B
idCargue
�/�/B J
,
�/�/J K
usuarioDigita
�/�/L Y
,
�/�/Y Z
ref
�/�/[ ^
MsgRes
�/�/_ e
)
�/�/e f
;
�/�/f g
}
�/�/ 	
public
�/�/ 
int
�/�/ -
ActualizarConteo_Facturas_fase3
�/�/ 2
(
�/�/2 3
int
�/�/3 6
idCargue
�/�/7 ?
,
�/�/? @
string
�/�/A G
usuarioDigita
�/�/H U
,
�/�/U V
ref
�/�/W Z 
MessageResponseOBJ
�/�/[ m
MsgRes
�/�/n t
)
�/�/t u
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  -
ActualizarConteo_Facturas_fase3
�/�/  ?
(
�/�/? @
idCargue
�/�/@ H
,
�/�/H I
usuarioDigita
�/�/J W
,
�/�/W X
ref
�/�/Y \
MsgRes
�/�/] c
)
�/�/c d
;
�/�/d e
}
�/�/ 	
public
�/�/ 
int
�/�/ .
 ActualizarConteo_FacturasInicial
�/�/ 3
(
�/�/3 4
int
�/�/4 7
idCargue
�/�/8 @
,
�/�/@ A
ref
�/�/B E 
MessageResponseOBJ
�/�/F X
MsgRes
�/�/Y _
)
�/�/_ `
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  .
 ActualizarConteo_FacturasInicial
�/�/  @
(
�/�/@ A
idCargue
�/�/A I
,
�/�/I J
ref
�/�/K N
MsgRes
�/�/O U
)
�/�/U V
;
�/�/V W
}
�/�/ 	
public
�/�/ 
int
�/�/ *
ActualizarConteo_FacturasUno
�/�/ /
(
�/�// 0
int
�/�/0 3
idCargue
�/�/4 <
,
�/�/< =
string
�/�/> D
usuarioDigita
�/�/E R
,
�/�/R S
ref
�/�/T W 
MessageResponseOBJ
�/�/X j
MsgRes
�/�/k q
)
�/�/q r
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  *
ActualizarConteo_FacturasUno
�/�/  <
(
�/�/< =
idCargue
�/�/= E
,
�/�/E F
usuarioDigita
�/�/G T
,
�/�/T U
ref
�/�/V Y
MsgRes
�/�/Z `
)
�/�/` a
;
�/�/a b
}
�/�/ 	
public
�/�/ 
int
�/�/ '
ActualizarConteo_Facturas
�/�/ ,
(
�/�/, -
int
�/�/- 0
idCargue
�/�/1 9
,
�/�/9 :
string
�/�/; A
usuarioDigita
�/�/B O
,
�/�/O P
int
�/�/Q T
?
�/�/T U
tipo
�/�/V Z
,
�/�/Z [
ref
�/�/\ _ 
MessageResponseOBJ
�/�/` r
MsgRes
�/�/s y
)
�/�/y z
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  '
ActualizarConteo_Facturas
�/�/  9
(
�/�/9 :
idCargue
�/�/: B
,
�/�/B C
usuarioDigita
�/�/D Q
,
�/�/Q R
tipo
�/�/S W
,
�/�/W X
ref
�/�/Y \
MsgRes
�/�/] c
)
�/�/c d
;
�/�/d e
}
�/�/ 	
public
�/�/ 
List
�/�/ 
<
�/�/ 8
*management_prefacturas_reporteCierreResult
�/�/ >
>
�/�/> ?(
ReportePrefacturasCerradas
�/�/@ Z
(
�/�/Z [
int
�/�/[ ^
?
�/�/^ _
idCargue
�/�/` h
)
�/�/h i
{
�/�/ 	
return
�/�/ 
DACConsulta
�/�/ 
.
�/�/ (
ReportePrefacturasCerradas
�/�/ 9
(
�/�/9 :
idCargue
�/�/: B
)
�/�/B C
;
�/�/C D
}
�/�/ 	
public
�/�/ 
int
�/�/ (
ActualizarConteo_Facturas2
�/�/ -
(
�/�/- .
int
�/�/. 1
idCargue
�/�/2 :
,
�/�/: ;
string
�/�/< B
usuario
�/�/C J
,
�/�/J K
ref
�/�/L O 
MessageResponseOBJ
�/�/P b
MsgRes
�/�/c i
)
�/�/i j
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  (
ActualizarConteo_Facturas2
�/�/  :
(
�/�/: ;
idCargue
�/�/; C
,
�/�/C D
usuario
�/�/E L
,
�/�/L M
ref
�/�/N Q
MsgRes
�/�/R X
)
�/�/X Y
;
�/�/Y Z
}
�/�/ 	
public
�/�/ 
int
�/�/ (
ActualizarConteo_Facturas3
�/�/ -
(
�/�/- .
int
�/�/. 1
idCargue
�/�/2 :
,
�/�/: ;
string
�/�/< B
usuarioValida
�/�/C P
,
�/�/P Q
ref
�/�/R U 
MessageResponseOBJ
�/�/V h
MsgRes
�/�/i o
)
�/�/o p
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  (
ActualizarConteo_Facturas3
�/�/  :
(
�/�/: ;
idCargue
�/�/; C
,
�/�/C D
usuarioValida
�/�/E R
,
�/�/R S
ref
�/�/T W
MsgRes
�/�/X ^
)
�/�/^ _
;
�/�/_ `
}
�/�/ 	
public
�/�/ 
int
�/�/ (
ActualizarConteo_Facturas4
�/�/ -
(
�/�/- .
int
�/�/. 1
idCargue
�/�/2 :
,
�/�/: ;
ref
�/�/< ? 
MessageResponseOBJ
�/�/@ R
MsgRes
�/�/S Y
)
�/�/Y Z
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  (
ActualizarConteo_Facturas4
�/�/  :
(
�/�/: ;
idCargue
�/�/; C
,
�/�/C D
ref
�/�/E H
MsgRes
�/�/I O
)
�/�/O P
;
�/�/P Q
}
�/�/ 	
public
�/�/ 
int
�/�/ (
ActualizarConteo_Facturas5
�/�/ -
(
�/�/- .
int
�/�/. 1
idCargue
�/�/2 :
,
�/�/: ;
ref
�/�/< ? 
MessageResponseOBJ
�/�/@ R
MsgRes
�/�/S Y
)
�/�/Y Z
{
�/�/ 	
return
�/�/ 
DACActualiza
�/�/ 
.
�/�/  (
ActualizarConteo_Facturas5
�/�/  :
(
�/�/: ;
idCargue
�/�/; C
,
�/�/C D
ref
�/�/E H
MsgRes
�/�/I O
)
�/�/O P
;
�/�/P Q
}
�/�/ 	
public
�/�/ G
9management_prefacturas_buscarEnHistoricoPrefacturasResult
�/�/ H(
BuscarHistoricoPrefacturas
�/�/I c
(
�/�/c d
string
�/�/d j)
num_documento_beneficiario�/�/k �
,�/�/� �
string�/�/� �
cum�/�/� �
,�/�/� �
string
�/�/ +
nombre_comercial_medicacmento
�/�/ ,
,
�/�/, -
string
�/�/. 4%
num_unidades_entregadas
�/�/5 L
,
�/�/L M
DateTime
�/�/N V$
fecha_despacho_formula
�/�/W m
,
�/�/m n
string
�/�/o u)
vlr_unitario_und_entregada�/�/v �
)�/�/� �
{
�/�/ 	
return
�/�/ 
DACConsulta
�/�/ 
.
�/�/ (
BuscarHistoricoPrefacturas
�/�/ 9
(
�/�/9 :(
num_documento_beneficiario
�/�/: T
,
�/�/T U
cum
�/�/V Y
,
�/�/Y Z+
nombre_comercial_medicacmento
�/�/[ x
,
�/�/x y&
num_unidades_entregadas�/�/z �
,�/�/� �&
fecha_despacho_formula�/�/� �
,�/�/� �*
vlr_unitario_und_entregada�/�/� �
)�/�/� �
;�/�/� �
}
�/�/ 	
public
�/�/ $
md_prefactura_contador
�/�/ %+
TraerDatosContadorPrefacturas
�/�/& C
(
�/�/C D
int
�/�/D G!
idDetallePrefactura
�/�/H [
)
�/�/[ \
{
�/�/ 	
return
�/�/ 
DACConsulta
�/�/ 
.
�/�/ +
TraerDatosContadorPrefacturas
�/�/ <
(
�/�/< =!
idDetallePrefactura
�/�/= P
)
�/�/P Q
;
�/�/Q R
}
�/�/ 	
public
�/�/ 
List
�/�/ 
<
�/�/ 5
'management_Validador_datosCorreosResult
�/�/ ;
>
�/�/; <(
ListadoCorreosValidadorOPL
�/�/= W
(
�/�/W X
int
�/�/X [
?
�/�/[ \

idRegional
�/�/] g
)
�/�/g h
{
�/�/ 	
return
�/�/ 
DACConsulta
�/�/ 
.
�/�/ (
ListadoCorreosValidadorOPL
�/�/ 9
(
�/�/9 :

idRegional
�/�/: D
)
�/�/D E
;
�/�/E F
}
�/�/ 	
public
�/�/ 
List
�/�/ 
<
�/�/ 3
%management_prestadores_regionalResult
�/�/ 9
>
�/�/9 :$
GetPrestadoresRegional
�/�/; Q
(
�/�/Q R
int
�/�/R U

idRegional
�/�/V `
)
�/�/` a
{
�/�/ 	
return
�/�/ 
DACConsulta
�/�/ 
.
�/�/ $
GetPrestadoresRegional
�/�/ 5
(
�/�/5 6

idRegional
�/�/6 @
)
�/�/@ A
;
�/�/A B
}
�/�/ 	
public
�/�/ 
List
�/�/ 
<
�/�/ ,
vw_factura_digital_gasto_total
�/�/ 2
>
�/�/2 3
GetGatosFactura
�/�/4 C
(
�/�/C D
int
�/�/D G
id
�/�/H J
)
�/�/J K
{
�/�/ 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 
GetGatosFactura
�0�0 .
(
�0�0. /
id
�0�0/ 1
)
�0�01 2
;
�0�02 3
}
�0�0 	
public
�0�0 
List
�0�0 
<
�0�0 9
+managementprestadores_alertas_activasResult
�0�0 ?
>
�0�0? @'
GetConsultaAlertasactivas
�0�0A Z
(
�0�0Z [
)
�0�0[ \
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 '
GetConsultaAlertasactivas
�0�0 8
(
�0�08 9
)
�0�09 :
;
�0�0: ;
}
�0�0 	
public
�0�0 
List
�0�0 
<
�0�0 ?
1managmentprestadoresfacturasgestionadasdtllResult
�0�0 E
>
�0�0E F)
GetListFacturasByNumFactura
�0�0G b
(
�0�0b c
string
�0�0c i

numfactura
�0�0j t
)
�0�0t u
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 )
GetListFacturasByNumFactura
�0�0 :
(
�0�0: ;

numfactura
�0�0; E
)
�0�0E F
;
�0�0F G
}
�0�0 	
public
�0�0 
List
�0�0 
<
�0�0 G
9managmentprestadoresfacturasgestionadasdtllCompletaResult
�0�0 M
>
�0�0M N1
#GetListFacturasByNumFacturaCompleta
�0�0O r
(
�0�0r s
String
�0�0s y
numFac�0�0z �
,�0�0� �
String�0�0� �
nit�0�0� �
,�0�0� �
String�0�0� �
	prestador�0�0� �
,�0�0� �
String�0�0� �
sap�0�0� �
)�0�0� �
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 1
#GetListFacturasByNumFacturaCompleta
�0�0 B
(
�0�0B C
numFac
�0�0C I
,
�0�0I J
nit
�0�0K N
,
�0�0N O
	prestador
�0�0P Y
,
�0�0Y Z
sap
�0�0[ ^
)
�0�0^ _
;
�0�0_ `
}
�0�0 	
public
�0�0 9
+ManagementPrestadoresFacturasByIdDtllResult
�0�0 : 
GetInfoFacturaById
�0�0; M
(
�0�0M N
int
�0�0N Q
idcarguedtll
�0�0R ^
)
�0�0^ _
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0  
GetInfoFacturaById
�0�0 1
(
�0�01 2
idcarguedtll
�0�02 >
)
�0�0> ?
;
�0�0? @
}
�0�0 	
public
�0�0 
List
�0�0 
<
�0�0 :
,managmentprestadoresFacturas_analistasResult
�0�0 @
>
�0�0@ A+
prestadoresFacturas_analistas
�0�0B _
(
�0�0_ `
)
�0�0` a
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 +
prestadoresFacturas_analistas
�0�0 <
(
�0�0< =
)
�0�0= >
;
�0�0> ?
}
�0�0 	
public
�0�0 
List
�0�0 
<
�0�0 :
,managmentprestadoresFacturas_auditoresResult
�0�0 @
>
�0�0@ A+
prestadoresFacturas_auditores
�0�0B _
(
�0�0_ `
)
�0�0` a
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 +
prestadoresFacturas_auditores
�0�0 <
(
�0�0< =
)
�0�0= >
;
�0�0> ?
}
�0�0 	
public
�0�0 
Int32
�0�0 %
InsertarGestionAnalista
�0�0 ,
(
�0�0, -*
ref_cuentas_medicas_analista
�0�0- I
OBJ
�0�0J M
,
�0�0M N
ref
�0�0O R 
MessageResponseOBJ
�0�0S e
MsgRes
�0�0f l
)
�0�0l m
{
�0�0 	
return
�0�0 

DACInserta
�0�0 
.
�0�0 %
InsertarGestionAnalista
�0�0 5
(
�0�05 6
OBJ
�0�06 9
,
�0�09 :
ref
�0�0; >
MsgRes
�0�0? E
)
�0�0E F
;
�0�0F G
}
�0�0 	
public
�0�0 
List
�0�0 
<
�0�0 +
vw_recep_facturas_cargue_base
�0�0 1
>
�0�01 2(
GetHistoricoRadicacionById
�0�03 M
(
�0�0M N
int
�0�0N Q
idcargue
�0�0R Z
)
�0�0Z [
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 (
GetHistoricoRadicacionById
�0�0 9
(
�0�09 :
idcargue
�0�0: B
)
�0�0B C
;
�0�0C D
}
�0�0 	
public
�0�0 
List
�0�0 
<
�0�0 '
ManagmentFacturasV2Result
�0�0 -
>
�0�0- .&
GetFacturasByRecepcionV2
�0�0/ G
(
�0�0G H
int
�0�0H K
idrecepcion
�0�0L W
)
�0�0W X
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 &
GetFacturasByRecepcionV2
�0�0 7
(
�0�07 8
idrecepcion
�0�08 C
)
�0�0C D
;
�0�0D E
}
�0�0 	
public
�0�0 
Int32
�0�0 $
InsertarGestionAuditor
�0�0 +
(
�0�0+ ,+
ref_cuentas_medicas_auditores
�0�0, I
OBJ
�0�0J M
,
�0�0M N
ref
�0�0O R 
MessageResponseOBJ
�0�0S e
MsgRes
�0�0f l
)
�0�0l m
{
�0�0 	
return
�0�0 

DACInserta
�0�0 
.
�0�0 $
InsertarGestionAuditor
�0�0 4
(
�0�04 5
OBJ
�0�05 8
,
�0�08 9
ref
�0�0: =
MsgRes
�0�0> D
)
�0�0D E
;
�0�0E F
}
�0�0 	
public
�0�0 
void
�0�0 '
ActualizaAnalistaAsignado
�0�0 -
(
�0�0- .*
ref_cuentas_medicas_analista
�0�0. J
ObjAudi
�0�0K R
,
�0�0R S
ref
�0�0T W 
MessageResponseOBJ
�0�0X j
MsgRes
�0�0k q
)
�0�0q r
{
�0�0 	
DACActualiza
�0�0 
.
�0�0 '
ActualizaAnalistaAsignado
�0�0 2
(
�0�02 3
ObjAudi
�0�03 :
,
�0�0: ;
ref
�0�0< ?
MsgRes
�0�0@ F
)
�0�0F G
;
�0�0G H
}
�0�0 	
public
�0�0 
void
�0�0 &
ActualizaAuditorAsignado
�0�0 ,
(
�0�0, -+
ref_cuentas_medicas_auditores
�0�0- J
ObjAudi
�0�0K R
,
�0�0R S
ref
�0�0T W 
MessageResponseOBJ
�0�0X j
MsgRes
�0�0k q
)
�0�0q r
{
�0�0 	
DACActualiza
�0�0 
.
�0�0 &
ActualizaAuditorAsignado
�0�0 1
(
�0�01 2
ObjAudi
�0�02 9
,
�0�09 :
ref
�0�0; >
MsgRes
�0�0? E
)
�0�0E F
;
�0�0F G
}
�0�0 	
public
�0�0 
void
�0�0 )
InsertarLogBusquedaTableros
�0�0 /
(
�0�0/ 0-
log_busquedas_tableros_facturas
�0�00 O
obj
�0�0P S
,
�0�0S T
ref
�0�0U X 
MessageResponseOBJ
�0�0Y k
MsgRes
�0�0l r
)
�0�0r s
{
�0�0 	

DACInserta
�0�0 
.
�0�0 )
InsertarLogBusquedaTableros
�0�0 2
(
�0�02 3
obj
�0�03 6
,
�0�06 7
ref
�0�08 ;
MsgRes
�0�0< B
)
�0�0B C
;
�0�0C D
}
�0�0 	
public
�0�0 
List
�0�0 
<
�0�0 !
ref_gestion_interna
�0�0 '
>
�0�0' (#
GetGestionInternaList
�0�0) >
(
�0�0> ?
)
�0�0? @
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 #
GetGestionInternaList
�0�0 4
(
�0�04 5
)
�0�05 6
;
�0�06 7
}
�0�0 	
public
�0�0 !
ref_gestion_interna
�0�0 "#
GetGestionInternaById
�0�0# 8
(
�0�08 9
int
�0�09 <
	idgestion
�0�0= F
)
�0�0F G
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 #
GetGestionInternaById
�0�0 4
(
�0�04 5
	idgestion
�0�05 >
)
�0�0> ?
;
�0�0? @
}
�0�0 	
public
�0�0 
List
�0�0 
<
�0�0 '
vw_odont_historia_clinica
�0�0 -
>
�0�0- .!
ListHistoriaClinica
�0�0/ B
(
�0�0B C
int
�0�0C F
id_historia
�0�0G R
)
�0�0R S
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 !
ListHistoriaClinica
�0�0 2
(
�0�02 3
id_historia
�0�03 >
)
�0�0> ?
;
�0�0? @
}
�0�0 	
public
�0�0 
List
�0�0 
<
�0�0 '
vw_odont_historia_clinica
�0�0 -
>
�0�0- ./
!GetListHistoriaClinicaXOdontologo
�0�0/ P
(
�0�0P Q
string
�0�0Q W
nomodontologo
�0�0X e
)
�0�0e f
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 /
!GetListHistoriaClinicaXOdontologo
�0�0 @
(
�0�0@ A
nomodontologo
�0�0A N
)
�0�0N O
;
�0�0O P
}
�0�0 	
public
�0�0 
void
�0�0 %
EliminarHistoriaclinica
�0�0 +
(
�0�0+ ,
int
�0�0, /
id_hc_paciente
�0�00 >
,
�0�0> ?<
.log_eliminacion_historias_clinicas_odontologia
�0�0@ n
obj
�0�0o r
,
�0�0r s
ref
�0�0t w!
MessageResponseOBJ�0�0x �
MsgRes�0�0� �
)�0�0� �
{
�0�0 	

DACElimina
�0�0 
.
�0�0 %
EliminarHistoriaclinica
�0�0 .
(
�0�0. /
id_hc_paciente
�0�0/ =
,
�0�0= >
obj
�0�0? B
,
�0�0B C
ref
�0�0D G
MsgRes
�0�0H N
)
�0�0N O
;
�0�0O P
}
�0�0 	
public
�0�0 
void
�0�0 1
#InsertarLogActualizacionFechaEgreso
�0�0 7
(
�0�07 8%
log_update_fecha_egreso
�0�08 O
log
�0�0P S
,
�0�0S T
ref
�0�0U X 
MessageResponseOBJ
�0�0Y k
MsgRes
�0�0l r
)
�0�0r s
{
�0�0 	

DACInserta
�0�0 
.
�0�0 1
#InsertarLogActualizacionFechaEgreso
�0�0 :
(
�0�0: ;
log
�0�0; >
,
�0�0> ?
ref
�0�0@ C
MsgRes
�0�0D J
)
�0�0J K
;
�0�0K L
}
�0�0 	
public
�0�0 
int
�0�0 '
InsertarGastosPorServicio
�0�0 ,
(
�0�0, -,
gasto_por_servicio_cargue_base
�0�0- K
obj
�0�0L O
,
�0�0O P
ref
�0�0Q T 
MessageResponseOBJ
�0�0U g
MsgRes
�0�0h n
)
�0�0n o
{
�0�0 	
return
�0�0 

DACInserta
�0�0 
.
�0�0 '
InsertarGastosPorServicio
�0�0 7
(
�0�07 8
obj
�0�08 ;
,
�0�0; <
ref
�0�0= @
MsgRes
�0�0A G
)
�0�0G H
;
�0�0H I
}
�0�0 	
public
�0�0 
void
�0�0 +
InsertarGastosPorServicioDtll
�0�0 1
(
�0�01 2
List
�0�02 6
<
�0�06 7,
gasto_por_Servicio_cargue_dtll
�0�07 U
>
�0�0U V
dtll
�0�0W [
,
�0�0[ \
ref
�0�0] ` 
MessageResponseOBJ
�0�0a s
MsgRes
�0�0t z
)
�0�0z {
{
�0�0 	

DACInserta
�0�0 
.
�0�0 +
InsertarGastosPorServicioDtll
�0�0 4
(
�0�04 5
dtll
�0�05 9
,
�0�09 :
ref
�0�0; >
MsgRes
�0�0? E
)
�0�0E F
;
�0�0F G
}
�0�0 	
public
�0�0 ,
gasto_por_servicio_cargue_base
�0�0 -
getcarguebase
�0�0. ;
(
�0�0; <
int
�0�0< ?
mes
�0�0@ C
,
�0�0C D
int
�0�0E H
año
�0�0I L
,
�0�0L M
string
�0�0N T
regional
�0�0U ]
)
�0�0] ^
{
�0�0 	
return
�0�0 
DACConsulta
�0�0 
.
�0�0 
getcarguebase
�0�0 ,
(
�0�0, -
mes
�0�0- 0
,
�0�00 1
año
�0�02 5
,
�0�05 6
regional
�0�07 ?
)
�0�0? @
;
�0�0@ A
}
�1�1 	
public
�1�1 
List
�1�1 
<
�1�1 ,
vw_consulta_gasto_por_servicio
�1�1 2
>
�1�12 33
%ObtenerListadoCarguesGastoPorServicio
�1�14 Y
(
�1�1Y Z
)
�1�1Z [
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 3
%ObtenerListadoCarguesGastoPorServicio
�1�1 D
(
�1�1D E
)
�1�1E F
;
�1�1F G
}
�1�1 	
public
�1�1 
List
�1�1 
<
�1�1 ;
-Management_gasto_x_servicio_consolidadoResult
�1�1 A
>
�1�1A B?
1ObtenerConsolidadoGastoPorServicioPorIdCargueBase
�1�1C t
(
�1�1t u
int
�1�1u x
idCargueBase�1�1y �
)�1�1� �
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 ?
1ObtenerConsolidadoGastoPorServicioPorIdCargueBase
�1�1 P
(
�1�1P Q
idCargueBase
�1�1Q ]
)
�1�1] ^
;
�1�1^ _
}
�1�1 	
public
�1�1 
List
�1�1 
<
�1�1 6
(management_gastoxservicio_consultaResult
�1�1 <
>
�1�1< =-
ObtenerGastoPorsercicioConsulta
�1�1> ]
(
�1�1] ^
DateTime
�1�1^ f
?
�1�1f g
fechaInicio
�1�1h s
,
�1�1s t
DateTime
�1�1u }
?
�1�1} ~
fechaFin�1�1 �
,�1�1� �
string�1�1� �
factura�1�1� �
,�1�1� �
int�1�1� �
cedula�1�1� �
,�1�1� �
string�1�1� �
servicio�1�1� �
,�1�1� �
string�1�1� �
tiga�1�1� �
,�1�1� �
DateTime�1�1� �
?�1�1� �
fechaInicioPre�1�1� �
,�1�1� �
DateTime�1�1� �
?�1�1� �
fechaFinPre�1�1� �
)�1�1� �
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 -
ObtenerGastoPorsercicioConsulta
�1�1 >
(
�1�1> ?
fechaInicio
�1�1? J
,
�1�1J K
fechaFin
�1�1L T
,
�1�1T U
factura
�1�1V ]
,
�1�1] ^
cedula
�1�1_ e
,
�1�1e f
servicio
�1�1g o
,
�1�1o p
tiga
�1�1q u
,
�1�1u v
fechaInicioPre�1�1w �
,�1�1� �
fechaFinPre�1�1� �
)�1�1� �
;�1�1� �
}
�1�1 	
public
�1�1 
int
�1�1 ,
EliminarCargueGastoPorServicio
�1�1 1
(
�1�11 2
int
�1�12 5
idCargue
�1�16 >
)
�1�1> ?
{
�1�1 	
return
�1�1 

DACElimina
�1�1 
.
�1�1 ,
EliminarCargueGastoPorServicio
�1�1 <
(
�1�1< =
idCargue
�1�1= E
)
�1�1E F
;
�1�1F G
}
�1�1 	
public
�1�1 
int
�1�1 /
!InsertarLogEliminarGastoxServicio
�1�1 4
(
�1�14 54
&log_gastoxServicio_eliminarConsolidado
�1�15 [
obj
�1�1\ _
)
�1�1_ `
{
�1�1 	
return
�1�1 

DACInserta
�1�1 
.
�1�1 /
!InsertarLogEliminarGastoxServicio
�1�1 ?
(
�1�1? @
obj
�1�1@ C
)
�1�1C D
;
�1�1D E
}
�1�1 	
public
�1�1 
List
�1�1 
<
�1�1 -
seguimiento_entregables_periodo
�1�1 3
>
�1�13 4'
GetListEntregablesPeriodo
�1�15 N
(
�1�1N O
int
�1�1O R
id_seg_entregable
�1�1S d
)
�1�1d e
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 '
GetListEntregablesPeriodo
�1�1 8
(
�1�18 9
id_seg_entregable
�1�19 J
)
�1�1J K
;
�1�1K L
}
�1�1 	
public
�1�1 -
seguimiento_entregables_periodo
�1�1 .&
GetEntregablePeriodoById
�1�1/ G
(
�1�1G H
int
�1�1H K
id_ent_periodo
�1�1L Z
)
�1�1Z [
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 &
GetEntregablePeriodoById
�1�1 7
(
�1�17 8
id_ent_periodo
�1�18 F
)
�1�1F G
;
�1�1G H
}
�1�1 	
public
�1�1 
List
�1�1 
<
�1�1 *
ref_periodicidad_entregables
�1�1 0
>
�1�10 1,
GetListPeriodicidadEntregables
�1�12 P
(
�1�1P Q
)
�1�1Q R
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 ,
GetListPeriodicidadEntregables
�1�1 =
(
�1�1= >
)
�1�1> ?
;
�1�1? @
}
�1�1 	
public
�1�1 
void
�1�1 6
(InsertarOActualizarSeguimientoEntregable
�1�1 <
(
�1�1< =%
seguimiento_entregables
�1�1= T
obj
�1�1U X
,
�1�1X Y
ref
�1�1Z ] 
MessageResponseOBJ
�1�1^ p
MsgRes
�1�1q w
)
�1�1w x
{
�1�1 	

DACInserta
�1�1 
.
�1�1 6
(InsertarOActualizarSeguimientoEntregable
�1�1 ?
(
�1�1? @
obj
�1�1@ C
,
�1�1C D
ref
�1�1E H
MsgRes
�1�1I O
)
�1�1O P
;
�1�1P Q
}
�1�1 	
public
�1�1 
void
�1�1 /
!InsertarSeguimientoEntregableDTLL
�1�1 5
(
�1�15 6
int
�1�16 9
id_seg_entregable
�1�1: K
,
�1�1K L&
seguimiento_dtll_entrega
�1�1M e
obj
�1�1f i
,
�1�1i j
List
�1�1k o
<
�1�1o p1
"seguimiento_entregables_documentos�1�1p �
>�1�1� �
Objdocumentos�1�1� �
,�1�1� �
ref�1�1� �"
MessageResponseOBJ�1�1� �
MsgRes�1�1� �
)�1�1� �
{
�1�1 	

DACInserta
�1�1 
.
�1�1 /
!InsertarSeguimientoEntregableDTLL
�1�1 8
(
�1�18 9
id_seg_entregable
�1�19 J
,
�1�1J K
obj
�1�1L O
,
�1�1O P
Objdocumentos
�1�1Q ^
,
�1�1^ _
ref
�1�1` c
MsgRes
�1�1d j
)
�1�1j k
;
�1�1k l
}
�1�1 	
public
�1�1 
Int32
�1�1 0
"InsertarSeguimientoEntregableDTLL1
�1�1 7
(
�1�17 8
int
�1�18 ;
id_seg_entregable
�1�1< M
,
�1�1M N&
seguimiento_dtll_entrega
�1�1O g
obj
�1�1h k
,
�1�1k l
ref
�1�1m p!
MessageResponseOBJ�1�1q �
MsgRes�1�1� �
)�1�1� �
{
�1�1 	
return
�1�1 

DACInserta
�1�1 
.
�1�1 0
"InsertarSeguimientoEntregableDTLL1
�1�1 @
(
�1�1@ A
id_seg_entregable
�1�1A R
,
�1�1R S
obj
�1�1T W
,
�1�1W X
ref
�1�1Y \
MsgRes
�1�1] c
)
�1�1c d
;
�1�1d e
}
�1�1 	
public
�1�1 
void
�1�1 0
"InsertarSeguimientoEntregableDTLL2
�1�1 6
(
�1�16 7
List
�1�17 ;
<
�1�1; <0
"seguimiento_entregables_documentos
�1�1< ^
>
�1�1^ _
lista
�1�1` e
,
�1�1e f
ref
�1�1g j 
MessageResponseOBJ
�1�1k }
MsgRes�1�1~ �
)�1�1� �
{
�1�1 	

DACInserta
�1�1 
.
�1�1 0
"InsertarSeguimientoEntregableDTLL2
�1�1 9
(
�1�19 :
lista
�1�1: ?
,
�1�1? @
ref
�1�1A D
MsgRes
�1�1E K
)
�1�1K L
;
�1�1L M
}
�1�1 	
public
�1�1 
List
�1�1 
<
�1�1 (
vw_seguimiento_entregables
�1�1 .
>
�1�1. / 
GetListEntregables
�1�10 B
(
�1�1B C
int
�1�1C F
?
�1�1F G
periodicidad
�1�1H T
)
�1�1T U
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1  
GetListEntregables
�1�1 1
(
�1�11 2
periodicidad
�1�12 >
)
�1�1> ?
;
�1�1? @
}
�1�1 	
public
�1�1 &
seguimiento_dtll_entrega
�1�1 ''
GetseguimientoDtllEntrega
�1�1( A
(
�1�1A B
int
�1�1B E
id_dtll
�1�1F M
)
�1�1M N
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 '
GetseguimientoDtllEntrega
�1�1 8
(
�1�18 9
id_dtll
�1�19 @
)
�1�1@ A
;
�1�1A B
}
�1�1 	
public
�1�1 &
seguimiento_dtll_entrega
�1�1 '1
#GetseguimientoDtllEntregaPresentado
�1�1( K
(
�1�1K L
int
�1�1L O
?
�1�1O P
id_dtll
�1�1Q X
)
�1�1X Y
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 1
#GetseguimientoDtllEntregaPresentado
�1�1 B
(
�1�1B C
id_dtll
�1�1C J
)
�1�1J K
;
�1�1K L
}
�1�1 	
public
�1�1 
List
�1�1 
<
�1�1 &
seguimiento_dtll_entrega
�1�1 ,
>
�1�1, -+
GetListseguimientoDtllEntrega
�1�1. K
(
�1�1K L
int
�1�1L O
id_seg_periodo
�1�1P ^
)
�1�1^ _
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 +
GetListseguimientoDtllEntrega
�1�1 <
(
�1�1< =
id_seg_periodo
�1�1= K
)
�1�1K L
;
�1�1L M
}
�1�1 	
public
�1�1 
List
�1�1 
<
�1�1 0
"seguimiento_entregables_documentos
�1�1 6
>
�1�16 7*
GetSeguimientoEntregableDocs
�1�18 T
(
�1�1T U
int
�1�1U X
id
�1�1Y [
)
�1�1[ \
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 *
GetSeguimientoEntregableDocs
�1�1 ;
(
�1�1; <
id
�1�1< >
)
�1�1> ?
;
�1�1? @
}
�1�1 	
public
�1�1 0
"seguimiento_entregables_documentos
�1�1 1(
traerDocumentoEntregableId
�1�12 L
(
�1�1L M
int
�1�1M P
idDocumento
�1�1Q \
)
�1�1\ ]
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 (
traerDocumentoEntregableId
�1�1 9
(
�1�19 :
idDocumento
�1�1: E
)
�1�1E F
;
�1�1F G
}
�1�1 	
public
�1�1 
List
�1�1 
<
�1�1 ?
1managmentSeguimiento_entregables_documentosResult
�1�1 E
>
�1�1E F+
GetSeguimientoEntregableDocs2
�1�1G d
(
�1�1d e
ref
�1�1e h 
MessageResponseOBJ
�1�1i {
MsgRes�1�1| �
)�1�1� �
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 +
GetSeguimientoEntregableDocs2
�1�1 <
(
�1�1< =
ref
�1�1= @
MsgRes
�1�1A G
)
�1�1G H
;
�1�1H I
}
�1�1 	
public
�1�1 %
seguimiento_entregables
�1�1 &&
GetSeguimientoEntregable
�1�1' ?
(
�1�1? @
int
�1�1@ C
id
�1�1D F
)
�1�1F G
{
�1�1 	
return
�1�1 
DACConsulta
�1�1 
.
�1�1 &
GetSeguimientoEntregable
�1�1 7
(
�1�17 8
id
�1�18 :
)
�1�1: ;
;
�1�1; <
}
�1�1 	
public
�2�2 
void
�2�2 2
$InsertarSeguimientoEntregablePeriodo
�2�2 8
(
�2�28 9-
seguimiento_entregables_periodo
�2�29 X
obj
�2�2Y \
,
�2�2\ ]
ref
�2�2^ a 
MessageResponseOBJ
�2�2b t
MsgRes
�2�2u {
)
�2�2{ |
{
�2�2 	

DACInserta
�2�2 
.
�2�2 2
$InsertarSeguimientoEntregablePeriodo
�2�2 ;
(
�2�2; <
obj
�2�2< ?
,
�2�2? @
ref
�2�2A D
MsgRes
�2�2E K
)
�2�2K L
;
�2�2L M
}
�2�2 	
public
�2�2 
void
�2�2 '
InsertarGestionEntregable
�2�2 -
(
�2�2- .
int
�2�2. 1'
id_seg_entregable_periodo
�2�22 K
,
�2�2K L&
seguimiento_dtll_entrega
�2�2M e
obj
�2�2f i
,
�2�2i j
ref
�2�2k n!
MessageResponseOBJ�2�2o �
MsgRes�2�2� �
)�2�2� �
{
�2�2 	

DACInserta
�2�2 
.
�2�2 '
InsertarGestionEntregable
�2�2 0
(
�2�20 1'
id_seg_entregable_periodo
�2�21 J
,
�2�2J K
obj
�2�2L O
,
�2�2O P
ref
�2�2Q T
MsgRes
�2�2U [
)
�2�2[ \
;
�2�2\ ]
}
�2�2 	
public
�2�2 
List
�2�2 
<
�2�2 2
$ref_cobertura_seguimiento_entregable
�2�2 8
>
�2�28 9'
GetCoberturaSegEntregable
�2�2: S
(
�2�2S T
)
�2�2T U
{
�2�2 	
return
�2�2 
DACConsulta
�2�2 
.
�2�2 '
GetCoberturaSegEntregable
�2�2 8
(
�2�28 9
)
�2�29 :
;
�2�2: ;
}
�2�2 	
public
�2�2 
void
�2�2 "
ActualizarEntregable
�2�2 (
(
�2�2( )
int
�2�2) ,'
id_seg_entregable_periodo
�2�2- F
,
�2�2F G&
seguimiento_dtll_entrega
�2�2H `
obj
�2�2a d
,
�2�2d e
ref
�2�2f i 
MessageResponseOBJ
�2�2j |
MsgRes�2�2} �
)�2�2� �
{
�2�2 	
DACActualiza
�2�2 
.
�2�2 "
ActualizarEntregable
�2�2 -
(
�2�2- .'
id_seg_entregable_periodo
�2�2. G
,
�2�2G H
obj
�2�2I L
,
�2�2L M
ref
�2�2N Q
MsgRes
�2�2R X
)
�2�2X Y
;
�2�2Y Z
}
�2�2 	
public
�2�2 
void
�2�2 +
GuardarRespuestaObservaciones
�2�2 1
(
�2�21 2&
seguimiento_dtll_entrega
�2�22 J
obj
�2�2K N
,
�2�2N O
ref
�2�2P S 
MessageResponseOBJ
�2�2T f
MsgRes
�2�2g m
)
�2�2m n
{
�2�2 	
DACActualiza
�2�2 
.
�2�2 +
GuardarRespuestaObservaciones
�2�2 6
(
�2�26 7
obj
�2�27 :
,
�2�2: ;
ref
�2�2< ?
MsgRes
�2�2@ F
)
�2�2F G
;
�2�2G H
}
�2�2 	
public
�2�2 
List
�2�2 
<
�2�2 8
*ref_seguimiento_entregable_usuario_gestion
�2�2 >
>
�2�2> ?#
GetUsuariosSegGestion
�2�2@ U
(
�2�2U V
)
�2�2V W
{
�2�2 	
return
�2�2 
DACConsulta
�2�2 
.
�2�2 #
GetUsuariosSegGestion
�2�2 4
(
�2�24 5
)
�2�25 6
;
�2�26 7
}
�2�2 	
public
�2�2 
int
�2�2 '
InsertarPeriodoEntregable
�2�2 ,
(
�2�2, --
seguimiento_entregables_periodo
�2�2- L
obj
�2�2M P
,
�2�2P Q
ref
�2�2R U 
MessageResponseOBJ
�2�2V h
MsgRes
�2�2i o
)
�2�2o p
{
�2�2 	
return
�2�2 

DACInserta
�2�2 
.
�2�2 '
InsertarPeriodoEntregable
�2�2 7
(
�2�27 8
obj
�2�28 ;
,
�2�2; <
ref
�2�2= @
MsgRes
�2�2A G
)
�2�2G H
;
�2�2H I
}
�2�2 	
public
�2�2 
int
�2�2 )
ActualizarEntregablePeriodo
�2�2 .
(
�2�2. /-
seguimiento_entregables_periodo
�2�2/ N
obj
�2�2O R
,
�2�2R S
ref
�2�2T W 
MessageResponseOBJ
�2�2X j
MsgRes
�2�2k q
)
�2�2q r
{
�2�2 	
return
�2�2 
DACActualiza
�2�2 
.
�2�2  )
ActualizarEntregablePeriodo
�2�2  ;
(
�2�2; <
obj
�2�2< ?
,
�2�2? @
ref
�2�2A D
MsgRes
�2�2E K
)
�2�2K L
;
�2�2L M
}
�2�2 	
public
�2�2 
List
�2�2 
<
�2�2 5
'vw_seguimiento_entregables_competencias
�2�2 ;
>
�2�2; <3
%GetSeguimientoEntregablesCompetencias
�2�2= b
(
�2�2b c
)
�2�2c d
{
�2�2 	
return
�2�2 
DACConsulta
�2�2 
.
�2�2 3
%GetSeguimientoEntregablesCompetencias
�2�2 D
(
�2�2D E
)
�2�2E F
;
�2�2F G
}
�2�2 	
public
�2�2 
List
�2�2 
<
�2�2 $
ref_proceso_entregable
�2�2 *
>
�2�2* +"
Getprocesoentregable
�2�2, @
(
�2�2@ A
)
�2�2A B
{
�2�2 	
return
�2�2 
DACConsulta
�2�2 
.
�2�2 "
Getprocesoentregable
�2�2 3
(
�2�23 4
)
�2�24 5
;
�2�25 6
}
�2�2 	
public
�2�2 
List
�2�2 
<
�2�2 9
+ref_seguimiento_entregables_tipo_entregable
�2�2 ?
>
�2�2? @#
GetListTipoEntregable
�2�2A V
(
�2�2V W
)
�2�2W X
{
�2�2 	
return
�2�2 
DACConsulta
�2�2 
.
�2�2 #
GetListTipoEntregable
�2�2 4
(
�2�24 5
)
�2�25 6
;
�2�26 7
}
�2�2 	
public
�2�2 
List
�2�2 
<
�2�2 #
ref_estado_entregable
�2�2 )
>
�2�2) *%
GetListEstadoEntregable
�2�2+ B
(
�2�2B C
)
�2�2C D
{
�2�2 	
return
�2�2 
DACConsulta
�2�2 
.
�2�2 %
GetListEstadoEntregable
�2�2 6
(
�2�26 7
)
�2�27 8
;
�2�28 9
}
�2�2 	
public
�2�2 
List
�2�2 
<
�2�2 3
%seguimiento_entregables_alerta_diaria
�2�2 9
>
�2�29 :A
3GetListNotificacionesEnviadasSeguimientoEntregables
�2�2; n
(
�2�2n o
DateTime
�2�2o w
?
�2�2w x
fecha
�2�2y ~
)
�2�2~ 
{
�2�2 	
return
�2�2 
DACConsulta
�2�2 
.
�2�2 A
3GetListNotificacionesEnviadasSeguimientoEntregables
�2�2 R
(
�2�2R S
fecha
�2�2S X
)
�2�2X Y
;
�2�2Y Z
}
�2�2 	
public
�2�2 
List
�2�2 
<
�2�2 >
0Management_seguimiento_entregables_gestionResult
�2�2 D
>
�2�2D E1
#GetListSeguimientoEntregableGestion
�2�2F i
(
�2�2i j
int
�2�2j m
?
�2�2m n
periodicidad
�2�2o {
,
�2�2{ |
int�2�2} �
?�2�2� �
tipoEntregable�2�2� �
)�2�2� �
{
�2�2 	
return
�2�2 
DACConsulta
�2�2 
.
�2�2 1
#GetListSeguimientoEntregableGestion
�2�2 B
(
�2�2B C
periodicidad
�2�2C O
,
�2�2O P
tipoEntregable
�2�2Q _
)
�2�2_ `
;
�2�2` a
}
�2�2 	
public
�2�2 
List
�2�2 
<
�2�2 (
vw_seguimiento_entregables
�2�2 .
>
�2�2. //
!GetListEntregablesPorIdEntregable
�2�20 Q
(
�2�2Q R
int
�2�2R U
?
�2�2U V%
idSeguimientoEntregable
�2�2W n
)
�2�2n o
{
�2�2 	
return
�2�2 
DACConsulta
�2�2 
.
�2�2 /
!GetListEntregablesPorIdEntregable
�2�2 @
(
�2�2@ A%
idSeguimientoEntregable
�2�2A X
)
�2�2X Y
;
�2�2Y Z
}
�2�2 	
public
�2�2 
void
�2�2 2
$GuardarDatosEvalCalidadSegEntregable
�2�2 8
(
�2�28 9:
,seguimiento_entregables_periodo_eval_calidad
�2�29 e
obj
�2�2f i
,
�2�2i j
ref
�2�2k n!
MessageResponseOBJ�2�2o �
MsgRes�2�2� �
)�2�2� �
{
�2�2 	

DACInserta
�2�2 
.
�2�2 2
$GuardarDatosEvalCalidadSegEntregable
�2�2 ;
(
�2�2; <
obj
�2�2< ?
,
�2�2? @
ref
�2�2A D
MsgRes
�2�2E K
)
�2�2K L
;
�2�2L M
}
�2�2 	
public
�3�3 :
,seguimiento_entregables_periodo_eval_calidad
�3�3 ;:
,ConsultarEvaluacionPorIdPeriodoSegEntregable
�3�3< h
(
�3�3h i
int
�3�3i l
id
�3�3m o
)
�3�3o p
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 :
,ConsultarEvaluacionPorIdPeriodoSegEntregable
�3�3 K
(
�3�3K L
id
�3�3L N
)
�3�3N O
;
�3�3O P
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 B
4Management_seguimiento_entregables_indicadoresResult
�3�3 H
>
�3�3H I9
+GetListadoOportunidadSeguimientoEntregables
�3�3J u
(
�3�3u v
string
�3�3v |!
personaResponsable�3�3} �
,�3�3� �
int�3�3� �
?�3�3� �
tipoEntregable�3�3� �
,�3�3� �
int�3�3� �
?�3�3� �
periodicidad�3�3� �
,�3�3� �
int�3�3� �
?�3�3� �
año�3�3� �
)�3�3� �
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 9
+GetListadoOportunidadSeguimientoEntregables
�3�3 J
(
�3�3J K 
personaResponsable
�3�3K ]
,
�3�3] ^
tipoEntregable
�3�3_ m
,
�3�3m n
periodicidad
�3�3o {
,
�3�3{ |
año�3�3} �
)�3�3� �
;�3�3� �
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 G
9Management_SeguimientoEntregables_IndicadorXPersonaResult
�3�3 M
>
�3�3M NB
3GetListadoIndicadoresXPersonaSeguimientoEntregables�3�3O �
(�3�3� �
int�3�3� �

mesInicial�3�3� �
,�3�3� �
int�3�3� �
mesFinal�3�3� �
,�3�3� �
int�3�3� �
año�3�3� �
,�3�3� �
string�3�3� �
responsable�3�3� �
)�3�3� �
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 A
3GetListadoIndicadoresXPersonaSeguimientoEntregables
�3�3 R
(
�3�3R S

mesInicial
�3�3S ]
,
�3�3] ^
mesFinal
�3�3_ g
,
�3�3g h
año
�3�3i l
,
�3�3l m
responsable
�3�3n y
)
�3�3y z
;
�3�3z {
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 J
<Management_SeguimientoEntregables_IndicadorXComponenteResult
�3�3 P
>
�3�3P QE
6GetListadoIndicadoresXComponenteSeguimientoEntregables�3�3R �
(�3�3� �
int�3�3� �

mesInicial�3�3� �
,�3�3� �
int�3�3� �
mesFinal�3�3� �
,�3�3� �
int�3�3� �
año�3�3� �
,�3�3� �
int�3�3� �
?�3�3� �
	idProceso�3�3� �
)�3�3� �
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 D
6GetListadoIndicadoresXComponenteSeguimientoEntregables
�3�3 U
(
�3�3U V

mesInicial
�3�3V `
,
�3�3` a
mesFinal
�3�3b j
,
�3�3j k
año
�3�3l o
,
�3�3o p
	idProceso
�3�3q z
)
�3�3z {
;
�3�3{ |
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 P
BManagement_SeguimientoEntregables_IndicadorXCompyPeridicidadResult
�3�3 V
>
�3�3V WL
=GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregables�3�3X �
(�3�3� �
int�3�3� �

mesInicial�3�3� �
,�3�3� �
int�3�3� �
mesFinal�3�3� �
,�3�3� �
int�3�3� �
año�3�3� �
,�3�3� �
int�3�3� �
?�3�3� �
	idProceso�3�3� �
,�3�3� �
int�3�3� �
?�3�3� �
idPeriodicidad�3�3� �
)�3�3� �
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 K
=GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregables
�3�3 \
(
�3�3\ ]

mesInicial
�3�3] g
,
�3�3g h
mesFinal
�3�3i q
,
�3�3q r
año
�3�3s v
,
�3�3v w
	idProceso�3�3x �
,�3�3� �
idPeriodicidad�3�3� �
)�3�3� �
;�3�3� �
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 J
<Management_SeguimientoEntregables_IndicadorVencimientoResult
�3�3 P
>
�3�3P Q6
(GetIndicadorDiasVencimientSegEntregables
�3�3R z
(
�3�3z {
string�3�3{ �
responsable�3�3� �
,�3�3� �
int�3�3� �
?�3�3� �
	idProceso�3�3� �
,�3�3� �
int�3�3� �
?�3�3� �
año�3�3� �
)�3�3� �
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 6
(GetIndicadorDiasVencimientSegEntregables
�3�3 G
(
�3�3G H
responsable
�3�3H S
,
�3�3S T
	idProceso
�3�3U ^
,
�3�3^ _
año
�3�3` c
)
�3�3c d
;
�3�3d e
}
�3�3 	
public
�3�3 
int
�3�3 ,
eliminarEvaluacioEntregablesID
�3�3 1
(
�3�31 2
int
�3�32 5
?
�3�35 6
	idPeriodo
�3�37 @
)
�3�3@ A
{
�3�3 	
return
�3�3 

DACElimina
�3�3 
.
�3�3 ,
eliminarEvaluacioEntregablesID
�3�3 <
(
�3�3< =
	idPeriodo
�3�3= F
)
�3�3F G
;
�3�3G H
}
�3�3 	
public
�3�3 
int
�3�3 1
#eliminarFelicitacionesEntregablesID
�3�3 6
(
�3�36 7
int
�3�37 :
?
�3�3: ;
	idPeriodo
�3�3< E
)
�3�3E F
{
�3�3 	
return
�3�3 

DACElimina
�3�3 
.
�3�3 1
#eliminarFelicitacionesEntregablesID
�3�3 A
(
�3�3A B
	idPeriodo
�3�3B K
)
�3�3K L
;
�3�3L M
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 0
"ref_contact_clasificacion_contacto
�3�3 6
>
�3�36 7*
GetListClasificacionContacto
�3�38 T
(
�3�3T U
)
�3�3U V
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 *
GetListClasificacionContacto
�3�3 ;
(
�3�3; <
)
�3�3< =
;
�3�3= >
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 &
ref_contact_tipificacion
�3�3 ,
>
�3�3, -!
GetListTipificacion
�3�3. A
(
�3�3A B
)
�3�3B C
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 !
GetListTipificacion
�3�3 2
(
�3�32 3
)
�3�33 4
;
�3�34 5
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 '
ref_contact_tipo_servicio
�3�3 -
>
�3�3- .!
GetListTipoServicio
�3�3/ B
(
�3�3B C
)
�3�3C D
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 !
GetListTipoServicio
�3�3 2
(
�3�32 3
)
�3�33 4
;
�3�34 5
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 (
ref_contact_tipo_solicitud
�3�3 .
>
�3�3. /"
GetListTipoSolicitud
�3�30 D
(
�3�3D E
)
�3�3E F
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 "
GetListTipoSolicitud
�3�3 3
(
�3�33 4
)
�3�34 5
;
�3�35 6
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 /
!ref_contact_tipoSolicitudBitacora
�3�3 5
>
�3�35 6*
GetListTipoSolicitudBitacora
�3�37 S
(
�3�3S T
)
�3�3T U
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 *
GetListTipoSolicitudBitacora
�3�3 ;
(
�3�3; <
)
�3�3< =
;
�3�3= >
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 
	Ref_cie10
�3�3 
>
�3�3 
GetCie10Bycodigo
�3�3 /
(
�3�3/ 0
string
�3�30 6
term
�3�37 ;
)
�3�3; <
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 
GetCie10Bycodigo
�3�3 /
(
�3�3/ 0
term
�3�30 4
)
�3�34 5
;
�3�35 6
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 
ref_cie10_mortNat
�3�3 %
>
�3�3% &$
GetCie10MorNatBycodigo
�3�3' =
(
�3�3= >
string
�3�3> D
term
�3�3E I
)
�3�3I J
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 $
GetCie10MorNatBycodigo
�3�3 5
(
�3�35 6
term
�3�36 :
)
�3�3: ;
;
�3�3; <
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 *
ref_contact_estado_solicitud
�3�3 0
>
�3�30 1$
GetListEstadoSolicitud
�3�32 H
(
�3�3H I
)
�3�3I J
{
�3�3 	
return
�3�3 
DACConsulta
�3�3 
.
�3�3 $
GetListEstadoSolicitud
�3�3 5
(
�3�35 6
)
�3�36 7
;
�3�37 8
}
�3�3 	
public
�3�3 
List
�3�3 
<
�3�3 ,
ref_contact_medio_notificacion
�3�3 2
>
�3�32 3'
GetListMediosNotificacion
�3�34 M
(
�3�3M N
)
�3�3N O
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 '
GetListMediosNotificacion
�4�4 8
(
�4�48 9
)
�4�49 :
;
�4�4: ;
}
�4�4 	
public
�4�4 
int
�4�4 *
InsertarIngresoContactCenter
�4�4 /
(
�4�4/ 0
contact_center
�4�40 >
obj
�4�4? B
,
�4�4B C
ref
�4�4D G 
MessageResponseOBJ
�4�4H Z
MsgRes
�4�4[ a
)
�4�4a b
{
�4�4 	
return
�4�4 

DACInserta
�4�4 
.
�4�4 *
InsertarIngresoContactCenter
�4�4 :
(
�4�4: ;
obj
�4�4; >
,
�4�4> ?
ref
�4�4@ C
MsgRes
�4�4D J
)
�4�4J K
;
�4�4K L
}
�4�4 	
public
�4�4 
void
�4�4 (
InsertarBitacoraCallCenter
�4�4 .
(
�4�4. /
List
�4�4/ 3
<
�4�43 4!
contact_center_dtll
�4�44 G
>
�4�4G H
List
�4�4I M
,
�4�4M N
int
�4�4O R
id_contact_center
�4�4S d
,
�4�4d e
string
�4�4f l
usuario
�4�4m t
)
�4�4t u
{
�4�4 	

DACInserta
�4�4 
.
�4�4 (
InsertarBitacoraCallCenter
�4�4 1
(
�4�41 2
List
�4�42 6
,
�4�46 7
id_contact_center
�4�48 I
,
�4�4I J
usuario
�4�4K R
)
�4�4R S
;
�4�4S T
}
�4�4 	
public
�4�4 
int
�4�4 +
InsertarBitacoraContactCenter
�4�4 0
(
�4�40 1!
contact_center_dtll
�4�41 D
obj
�4�4E H
)
�4�4H I
{
�4�4 	
return
�4�4 

DACInserta
�4�4 
.
�4�4 +
InsertarBitacoraContactCenter
�4�4 ;
(
�4�4; <
obj
�4�4< ?
)
�4�4? @
;
�4�4@ A
}
�4�4 	
public
�4�4 
contact_center
�4�4 "
GetContactCenterById
�4�4 2
(
�4�42 3
int
�4�43 6
id
�4�47 9
)
�4�49 :
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 "
GetContactCenterById
�4�4 3
(
�4�43 4
id
�4�44 6
)
�4�46 7
;
�4�47 8
}
�4�4 	
public
�4�4 
List
�4�4 
<
�4�4 !
contact_center_dtll
�4�4 '
>
�4�4' (&
GetListBitacoraByIngreso
�4�4) A
(
�4�4A B
int
�4�4B E
id_contact_center
�4�4F W
,
�4�4W X
int
�4�4Y \
?
�4�4\ ]
censo
�4�4^ c
,
�4�4c d
int
�4�4e h
?
�4�4h i
idConcurrencia
�4�4j x
)
�4�4x y
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 &
GetListBitacoraByIngreso
�4�4 7
(
�4�47 8
id_contact_center
�4�48 I
,
�4�4I J
censo
�4�4K P
,
�4�4P Q
idConcurrencia
�4�4R `
)
�4�4` a
;
�4�4a b
}
�4�4 	
public
�4�4 
int
�4�4 .
 ActualizarContactCenterPrincipal
�4�4 3
(
�4�43 4
int
�4�44 7
?
�4�47 8
	idContact
�4�49 B
)
�4�4B C
{
�4�4 	
return
�4�4 
DACActualiza
�4�4 
.
�4�4  .
 ActualizarContactCenterPrincipal
�4�4  @
(
�4�4@ A
	idContact
�4�4A J
)
�4�4J K
;
�4�4K L
}
�4�4 	
public
�4�4 
List
�4�4 
<
�4�4 
vw_contact_center
�4�4 %
>
�4�4% &"
GetListContactCenter
�4�4' ;
(
�4�4; <
int
�4�4< ?
?
�4�4? @
estado
�4�4A G
)
�4�4G H
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 "
GetListContactCenter
�4�4 3
(
�4�43 4
estado
�4�44 :
)
�4�4: ;
;
�4�4; <
}
�4�4 	
public
�4�4 
List
�4�4 
<
�4�4 -
management_contact_centerResult
�4�4 3
>
�4�43 4'
ListaTableroContactCenter
�4�45 N
(
�4�4N O
DateTime
�4�4O W
?
�4�4W X
fechaIni
�4�4Y a
,
�4�4a b
DateTime
�4�4c k
?
�4�4k l
fechaFin
�4�4m u
)
�4�4u v
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 '
ListaTableroContactCenter
�4�4 8
(
�4�48 9
fechaIni
�4�49 A
,
�4�4A B
fechaFin
�4�4C K
)
�4�4K L
;
�4�4L M
}
�4�4 	
public
�4�4 -
management_contact_centerResult
�4�4 .,
GetContactCenterCensoIdContact
�4�4/ M
(
�4�4M N
int
�4�4N Q
id
�4�4R T
)
�4�4T U
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 ,
GetContactCenterCensoIdContact
�4�4 =
(
�4�4= >
id
�4�4> @
)
�4�4@ A
;
�4�4A B
}
�4�4 	
public
�4�4 -
management_contact_centerResult
�4�4 .*
GetContactCenterCensoIdCenso
�4�4/ K
(
�4�4K L
int
�4�4L O
id
�4�4P R
)
�4�4R S
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 *
GetContactCenterCensoIdCenso
�4�4 ;
(
�4�4; <
id
�4�4< >
)
�4�4> ?
;
�4�4? @
}
�4�4 	
public
�4�4 -
management_contact_centerResult
�4�4 .1
#GetContactCenterCensoIdConcurrencia
�4�4/ R
(
�4�4R S
int
�4�4S V
id
�4�4W Y
)
�4�4Y Z
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 1
#GetContactCenterCensoIdConcurrencia
�4�4 B
(
�4�4B C
id
�4�4C E
)
�4�4E F
;
�4�4F G
}
�4�4 	
public
�4�4 
int
�4�4 3
%ActualizarEnContactCenterConcurrencia
�4�4 8
(
�4�48 9
int
�4�49 <
?
�4�4< =
idConcurrencia
�4�4> L
,
�4�4L M
ref
�4�4N Q 
MessageResponseOBJ
�4�4R d
MsgRes
�4�4e k
)
�4�4k l
{
�4�4 	
return
�4�4 
DACActualiza
�4�4 
.
�4�4  3
%ActualizarEnContactCenterConcurrencia
�4�4  E
(
�4�4E F
idConcurrencia
�4�4F T
,
�4�4T U
ref
�4�4V Y
MsgRes
�4�4Z `
)
�4�4` a
;
�4�4a b
}
�4�4 	
public
�4�4 
int
�4�4 ,
ActualizarEnContactCenterCenso
�4�4 1
(
�4�41 2
int
�4�42 5
?
�4�45 6
idCenso
�4�47 >
,
�4�4> ?
ref
�4�4@ C 
MessageResponseOBJ
�4�4D V
MsgRes
�4�4W ]
)
�4�4] ^
{
�4�4 	
return
�4�4 
DACActualiza
�4�4 
.
�4�4  ,
ActualizarEnContactCenterCenso
�4�4  >
(
�4�4> ?
idCenso
�4�4? F
,
�4�4F G
ref
�4�4H K
MsgRes
�4�4L R
)
�4�4R S
;
�4�4S T
}
�4�4 	
public
�4�4 
void
�4�4 6
(InsertarLogConcurrenciaEnviadaCallCenter
�4�4 <
(
�4�4< =
List
�4�4= A
<
�4�4A B3
%log_concurrenciaEnviada_contactCenter
�4�4B g
>
�4�4g h
log
�4�4i l
,
�4�4l m
ref
�4�4n q!
MessageResponseOBJ�4�4r �
MsgRes�4�4� �
)�4�4� �
{
�4�4 	

DACInserta
�4�4 
.
�4�4 6
(InsertarLogConcurrenciaEnviadaCallCenter
�4�4 ?
(
�4�4? @
log
�4�4@ C
,
�4�4C D
ref
�4�4E H
MsgRes
�4�4I O
)
�4�4O P
;
�4�4P Q
}
�4�4 	
public
�4�4 
void
�4�4 @
2InsertarLogindividualConcurrenciaEnviadaCallCenter
�4�4 F
(
�4�4F G3
%log_concurrenciaEnviada_contactCenter
�4�4G l
log
�4�4m p
,
�4�4p q
ref
�4�4r u!
MessageResponseOBJ�4�4v �
MsgRes�4�4� �
)�4�4� �
{
�4�4 	

DACInserta
�4�4 
.
�4�4 @
2InsertarLogindividualConcurrenciaEnviadaCallCenter
�4�4 I
(
�4�4I J
log
�4�4J M
,
�4�4M N
ref
�4�4O R
MsgRes
�4�4S Y
)
�4�4Y Z
;
�4�4Z [
}
�4�4 	
public
�4�4 
void
�4�4 "
ActualizarImagenCaso
�4�4 (
(
�4�4( )
string
�4�4) /

rutaImagen
�4�40 :
,
�4�4: ;
string
�4�4< B
tipo
�4�4C G
,
�4�4G H
int
�4�4I L
contactcenter
�4�4M Z
)
�4�4Z [
{
�4�4 	
DACActualiza
�4�4 
.
�4�4 "
ActualizarImagenCaso
�4�4 -
(
�4�4- .

rutaImagen
�4�4. 8
,
�4�48 9
tipo
�4�4: >
,
�4�4> ?
contactcenter
�4�4@ M
)
�4�4M N
;
�4�4N O
}
�4�4 	
public
�4�4 
List
�4�4 
<
�4�4 %
ref_contact_solicitante
�4�4 +
>
�4�4+ ,'
GetlistSolicitantesbytipo
�4�4- F
(
�4�4F G
string
�4�4G M
term
�4�4N R
,
�4�4R S
int
�4�4T W
tipo
�4�4X \
)
�4�4\ ]
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 '
GetlistSolicitantesbytipo
�4�4 8
(
�4�48 9
term
�4�49 =
,
�4�4= >
tipo
�4�4? C
)
�4�4C D
;
�4�4D E
}
�4�4 	
public
�4�4 
List
�4�4 
<
�4�4 @
2management_contact_center_camposObligatoriosResult
�4�4 F
>
�4�4F G'
ListaCamposObligatoriosCC
�4�4H a
(
�4�4a b
int
�4�4b e
?
�4�4e f
	idContact
�4�4g p
,
�4�4p q
int
�4�4r u
?
�4�4u v
idConcurrencia�4�4w �
,�4�4� �
int�4�4� �
?�4�4� �
idCenso�4�4� �
)�4�4� �
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 '
ListaCamposObligatoriosCC
�4�4 8
(
�4�48 9
	idContact
�4�49 B
,
�4�4B C
idConcurrencia
�4�4D R
,
�4�4R S
idCenso
�4�4T [
)
�4�4[ \
;
�4�4\ ]
}
�4�4 	
public
�4�4 
List
�4�4 
<
�4�4 9
+management_contact_center_seguimientoResult
�4�4 ?
>
�4�4? @2
$ListaTableroContactCenterSeguimiento
�4�4A e
(
�4�4e f
DateTime
�4�4f n
?
�4�4n o
fechaIni
�4�4p x
,
�4�4x y
DateTime�4�4z �
?�4�4� �
fechaFin�4�4� �
)�4�4� �
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 2
$ListaTableroContactCenterSeguimiento
�4�4 C
(
�4�4C D
fechaIni
�4�4D L
,
�4�4L M
fechaFin
�4�4N V
)
�4�4V W
;
�4�4W X
}
�4�4 	
public
�4�4 
int
�4�4 +
ActualizarContactObligatorios
�4�4 0
(
�4�40 1
contact_center
�4�41 ?
obj
�4�4@ C
)
�4�4C D
{
�4�4 	
return
�4�4 
DACActualiza
�4�4 
.
�4�4  +
ActualizarContactObligatorios
�4�4  =
(
�4�4= >
obj
�4�4> A
)
�4�4A B
;
�4�4B C
}
�4�4 	
public
�4�4 
bool
�4�4 ,
ValidarExistenciaQuejasValidas
�4�4 2
(
�4�42 3
int
�4�43 6
mes
�4�47 :
,
�4�4: ;
int
�4�4< ?
año
�4�4@ C
)
�4�4C D
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 ,
ValidarExistenciaQuejasValidas
�4�4 =
(
�4�4= >
mes
�4�4> A
,
�4�4A B
año
�4�4C F
)
�4�4F G
;
�4�4G H
}
�4�4 	
public
�4�4 
void
�4�4 '
InsertarQuejasValidasDtll
�4�4 -
(
�4�4- .
List
�4�4. 2
<
�4�42 3)
calidad_quejas_validas_dtll
�4�43 N
>
�4�4N O
List
�4�4P T
,
�4�4T U$
calidad_quejas_validas
�4�4V l
objbase
�4�4m t
,
�4�4t u
ref
�4�4v y!
MessageResponseOBJ�4�4z �
MsgRes�4�4� �
)�4�4� �
{
�4�4 	

DACInserta
�4�4 
.
�4�4 '
InsertarQuejasValidasDtll
�4�4 0
(
�4�40 1
List
�4�41 5
,
�4�45 6
objbase
�4�47 >
,
�4�4> ?
ref
�4�4@ C
MsgRes
�4�4D J
)
�4�4J K
;
�4�4K L
}
�4�4 	
public
�4�4 
List
�4�4 
<
�4�4 '
vw_calidad_quejas_validas
�4�4 -
>
�4�4- .)
GetListCalidadQuejasValidas
�4�4/ J
(
�4�4J K
)
�4�4K L
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 )
GetListCalidadQuejasValidas
�4�4 :
(
�4�4: ;
)
�4�4; <
;
�4�4< =
}
�4�4 	
public
�4�4 
List
�4�4 
<
�4�4 -
calidad_quejas_validas_base_zip
�4�4 3
>
�4�43 4/
!GetListBasesCargadasQuejasValidas
�4�45 V
(
�4�4V W
)
�4�4W X
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 /
!GetListBasesCargadasQuejasValidas
�4�4 @
(
�4�4@ A
)
�4�4A B
;
�4�4B C
}
�4�4 	
public
�4�4 -
calidad_quejas_validas_base_zip
�4�4 .
GetArchivoById
�4�4/ =
(
�4�4= >
int
�4�4> A
id
�4�4B D
)
�4�4D E
{
�4�4 	
return
�4�4 
DACConsulta
�4�4 
.
�4�4 
GetArchivoById
�4�4 -
(
�4�4- .
id
�4�4. 0
)
�4�40 1
;
�4�41 2
}
�4�4 	
public
�5�5 
void
�5�5 -
EliminarArchivoZipQuejasValidas
�5�5 3
(
�5�53 4-
calidad_quejas_validas_base_zip
�5�54 S
obj
�5�5T W
)
�5�5W X
{
�5�5 	

DACElimina
�5�5 
.
�5�5 -
EliminarArchivoZipQuejasValidas
�5�5 6
(
�5�56 7
obj
�5�57 :
)
�5�5: ;
;
�5�5; <
}
�5�5 	
public
�5�5 
void
�5�5 *
InsertarArchivoQuejasValidas
�5�5 0
(
�5�50 1-
calidad_quejas_validas_base_zip
�5�51 P
obj
�5�5Q T
,
�5�5T U
ref
�5�5V Y 
MessageResponseOBJ
�5�5Z l
MsgRes
�5�5m s
)
�5�5s t
{
�5�5 	

DACInserta
�5�5 
.
�5�5 *
InsertarArchivoQuejasValidas
�5�5 3
(
�5�53 4
obj
�5�54 7
,
�5�57 8
ref
�5�59 <
MsgRes
�5�5= C
)
�5�5C D
;
�5�5D E
}
�5�5 	
public
�5�5 
bool
�5�5 .
 ValidarExistenciaOportunidadRIPS
�5�5 4
(
�5�54 5
int
�5�55 8
mes
�5�59 <
,
�5�5< =
int
�5�5> A
año
�5�5B E
)
�5�5E F
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 .
 ValidarExistenciaOportunidadRIPS
�5�5 ?
(
�5�5? @
mes
�5�5@ C
,
�5�5C D
año
�5�5E H
)
�5�5H I
;
�5�5I J
}
�5�5 	
public
�5�5 
void
�5�5 %
InsertarOportunidadRips
�5�5 +
(
�5�5+ ,
List
�5�5, 0
<
�5�50 1+
calidad_oportunidad_rips_dtll
�5�51 N
>
�5�5N O
List
�5�5P T
,
�5�5T U&
calidad_oportunidad_rips
�5�5V n
objbase
�5�5o v
,
�5�5v w
ref
�5�5x {!
MessageResponseOBJ�5�5| �
MsgRes�5�5� �
)�5�5� �
{
�5�5 	

DACInserta
�5�5 
.
�5�5 %
InsertarOportunidadRips
�5�5 .
(
�5�5. /
List
�5�5/ 3
,
�5�53 4
objbase
�5�55 <
,
�5�5< =
ref
�5�5> A
MsgRes
�5�5B H
)
�5�5H I
;
�5�5I J
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 3
%vw_calidad_oportunidad_rips_indicador
�5�5 9
>
�5�59 :+
GetListCalidadOportunidadRips
�5�5; X
(
�5�5X Y
)
�5�5Y Z
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 +
GetListCalidadOportunidadRips
�5�5 <
(
�5�5< =
)
�5�5= >
;
�5�5> ?
}
�5�5 	
public
�5�5 
void
�5�5 !
InsertarCalidadRips
�5�5 '
(
�5�5' (
List
�5�5( ,
<
�5�5, -"
calidad_de_rips_dtll
�5�5- A
>
�5�5A B
List
�5�5C G
,
�5�5G H
calidad_de_rips
�5�5I X
objbase
�5�5Y `
,
�5�5` a
ref
�5�5b e 
MessageResponseOBJ
�5�5f x
MsgRes
�5�5y 
)�5�5 �
{
�5�5 	

DACInserta
�5�5 
.
�5�5 !
InsertarCalidadRips
�5�5 *
(
�5�5* +
List
�5�5+ /
,
�5�5/ 0
objbase
�5�51 8
,
�5�58 9
ref
�5�5: =
MsgRes
�5�5> D
)
�5�5D E
;
�5�5E F
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 *
vw_calidad_de_rips_indicador
�5�5 0
>
�5�50 1'
GetListCalidadCalidadRips
�5�52 K
(
�5�5K L
)
�5�5L M
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 '
GetListCalidadCalidadRips
�5�5 8
(
�5�58 9
)
�5�59 :
;
�5�5: ;
}
�5�5 	
public
�5�5 
void
�5�5 -
InsertarOportunidadCitasMedicas
�5�5 3
(
�5�53 4
List
�5�54 8
<
�5�58 9;
-calidad_oportunidad_citas_medicina_gnral_dtll
�5�59 f
>
�5�5f g
List
�5�5h l
,
�5�5l m7
(calidad_oportunidad_citas_medicina_gnral�5�5n �
objbase�5�5� �
,�5�5� �
ref�5�5� �"
MessageResponseOBJ�5�5� �
MsgRes�5�5� �
)�5�5� �
{
�5�5 	

DACInserta
�5�5 
.
�5�5 -
InsertarOportunidadCitasMedicas
�5�5 6
(
�5�56 7
List
�5�57 ;
,
�5�5; <
objbase
�5�5= D
,
�5�5D E
ref
�5�5F I
MsgRes
�5�5J P
)
�5�5P Q
;
�5�5Q R
}
�5�5 	
public
�5�5 
void
�5�5 +
InsertarCalidadCitasCumplidas
�5�5 1
(
�5�51 2
List
�5�52 6
<
�5�56 7*
calidad_citas_cumplidas_dtll
�5�57 S
>
�5�5S T
List
�5�5U Y
,
�5�5Y Z%
calidad_citas_cumplidas
�5�5[ r
objbase
�5�5s z
,
�5�5z {
ref
�5�5| "
MessageResponseOBJ�5�5� �
MsgRes�5�5� �
)�5�5� �
{
�5�5 	

DACInserta
�5�5 
.
�5�5 +
InsertarCalidadCitasCumplidas
�5�5 4
(
�5�54 5
List
�5�55 9
,
�5�59 :
objbase
�5�5; B
,
�5�5B C
ref
�5�5D G
MsgRes
�5�5H N
)
�5�5N O
;
�5�5O P
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 C
5vw_calidad_oportunidad_citas_medicina_gnral_indicador
�5�5 I
>
�5�5I J,
GetListCalidadOporCitasMedicas
�5�5K i
(
�5�5i j
)
�5�5j k
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 ,
GetListCalidadOporCitasMedicas
�5�5 =
(
�5�5= >
)
�5�5> ?
;
�5�5? @
}
�5�5 	
public
�5�5 
void
�5�5 ,
InsertarOportunidadOdontologia
�5�5 2
(
�5�52 3
List
�5�53 7
<
�5�57 88
*calidad_oportunidad_odontologia_gnral_dtll
�5�58 b
>
�5�5b c
List
�5�5d h
,
�5�5h i4
%calidad_oportunidad_odontologia_gnral�5�5j �
objbase�5�5� �
,�5�5� �
ref�5�5� �"
MessageResponseOBJ�5�5� �
MsgRes�5�5� �
)�5�5� �
{
�5�5 	

DACInserta
�5�5 
.
�5�5 ,
InsertarOportunidadOdontologia
�5�5 5
(
�5�55 6
List
�5�56 :
,
�5�5: ;
objbase
�5�5< C
,
�5�5C D
ref
�5�5E H
MsgRes
�5�5I O
)
�5�5O P
;
�5�5P Q
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 @
2vw_calidad_oportunidad_odontologia_gnral_indicador
�5�5 F
>
�5�5F G+
GetListCalidadOporOdontologia
�5�5H e
(
�5�5e f
)
�5�5f g
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 +
GetListCalidadOporOdontologia
�5�5 <
(
�5�5< =
)
�5�5= >
;
�5�5> ?
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 2
$vw_calidad_citas_cumplidas_indicador
�5�5 8
>
�5�58 9*
GetListCalidadCitasCumplidas
�5�5: V
(
�5�5V W
)
�5�5W X
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 *
GetListCalidadCitasCumplidas
�5�5 ;
(
�5�5; <
)
�5�5< =
;
�5�5= >
}
�5�5 	
public
�5�5 
void
�5�5 %
InsertarEventosAdversos
�5�5 +
(
�5�5+ ,
List
�5�5, 0
<
�5�50 1$
calidad_evento_adverso
�5�51 G
>
�5�5G H
List
�5�5I M
,
�5�5M N
ref
�5�5O R 
MessageResponseOBJ
�5�5S e
MsgRes
�5�5f l
)
�5�5l m
{
�5�5 	

DACInserta
�5�5 
.
�5�5 %
InsertarEventosAdversos
�5�5 .
(
�5�5. /
List
�5�5/ 3
,
�5�53 4
ref
�5�55 8
MsgRes
�5�59 ?
)
�5�5? @
;
�5�5@ A
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 $
calidad_evento_adverso
�5�5 *
>
�5�5* +)
GetListCalidadEventoAdverso
�5�5, G
(
�5�5G H
)
�5�5H I
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 )
GetListCalidadEventoAdverso
�5�5 :
(
�5�5: ;
)
�5�5; <
;
�5�5< =
}
�5�5 	
public
�5�5 
void
�5�5 %
InsertarDocumentoInsumo
�5�5 +
(
�5�5+ ,/
!calidad_gestor_documental_insumos
�5�5, M
obj
�5�5N Q
,
�5�5Q R
ref
�5�5S V 
MessageResponseOBJ
�5�5W i
MsgRes
�5�5j p
)
�5�5p q
{
�5�5 	

DACInserta
�5�5 
.
�5�5 %
InsertarDocumentoInsumo
�5�5 .
(
�5�5. /
obj
�5�5/ 2
,
�5�52 3
ref
�5�54 7
MsgRes
�5�58 >
)
�5�5> ?
;
�5�5? @
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 /
!calidad_gestor_documental_insumos
�5�5 5
>
�5�55 6,
GetListGestorDocumentalInsumos
�5�57 U
(
�5�5U V
)
�5�5V W
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 ,
GetListGestorDocumentalInsumos
�5�5 =
(
�5�5= >
)
�5�5> ?
;
�5�5? @
}
�5�5 	
public
�5�5 /
!calidad_gestor_documental_insumos
�5�5 0
GetDocumentoById
�5�51 A
(
�5�5A B
int
�5�5B E
id
�5�5F H
)
�5�5H I
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 
GetDocumentoById
�5�5 /
(
�5�5/ 0
id
�5�50 2
)
�5�52 3
;
�5�53 4
}
�5�5 	
public
�5�5 2
$vw_calidad_gestor_documental_insumos
�5�5 3 
VwGetDocumentoById
�5�54 F
(
�5�5F G
int
�5�5G J
id
�5�5K M
)
�5�5M N
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5  
VwGetDocumentoById
�5�5 1
(
�5�51 2
id
�5�52 4
)
�5�54 5
;
�5�55 6
}
�5�5 	
public
�5�5 2
$vw_calidad_gestor_documental_insumos
�5�5 3#
TarerArchivoInsumosId
�5�54 I
(
�5�5I J
int
�5�5J M
id
�5�5N P
)
�5�5P Q
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 #
TarerArchivoInsumosId
�5�5 4
(
�5�54 5
id
�5�55 7
)
�5�57 8
;
�5�58 9
}
�5�5 	
public
�5�5 
void
�5�5 
EliminarDocumento
�5�5 %
(
�5�5% &/
!calidad_gestor_documental_insumos
�5�5& G
obj
�5�5H K
)
�5�5K L
{
�5�5 	

DACElimina
�5�5 
.
�5�5 
EliminarDocumento
�5�5 (
(
�5�5( )
obj
�5�5) ,
)
�5�5, -
;
�5�5- .
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 1
#ref_calidad_insumos_tipo_documental
�5�5 7
>
�5�57 8)
GetListInsumoTipoDocumental
�5�59 T
(
�5�5T U
)
�5�5U V
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 )
GetListInsumoTipoDocumental
�5�5 :
(
�5�5: ;
)
�5�5; <
;
�5�5< =
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 3
%vw_calidad_quejas_validas_prestadores
�5�5 9
>
�5�59 :)
GetPrestadoresQuejasValidas
�5�5; V
(
�5�5V W
string
�5�5W ]
term
�5�5^ b
,
�5�5b c
ref
�5�5d g 
MessageResponseOBJ
�5�5h z
MsgRes�5�5{ �
)�5�5� �
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 )
GetPrestadoresQuejasValidas
�5�5 :
(
�5�5: ;
term
�5�5; ?
,
�5�5? @
ref
�5�5A D
MsgRes
�5�5E K
)
�5�5K L
;
�5�5L M
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 G
9vw_calidad_oportunidad_calidad_rips_indicador_prestadores
�5�5 M
>
�5�5M N+
GetPrestadoresOportunidadRips
�5�5O l
(
�5�5l m
string
�5�5m s
term
�5�5t x
,
�5�5x y
ref
�5�5z }!
MessageResponseOBJ�5�5~ �
MsgRes�5�5� �
)�5�5� �
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 +
GetPrestadoresOportunidadRips
�5�5 <
(
�5�5< =
term
�5�5= A
,
�5�5A B
ref
�5�5C F
MsgRes
�5�5G M
)
�5�5M N
;
�5�5N O
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 G
9vw_calidad_oportunidad_calidad_rips_indicador_prestadores
�5�5 M
>
�5�5M N.
 GetCodPrestadoresOportunidadRips
�5�5O o
(
�5�5o p
string
�5�5p v
term
�5�5w {
,
�5�5{ |
ref�5�5} �"
MessageResponseOBJ�5�5� �
MsgRes�5�5� �
)�5�5� �
{
�5�5 	
return
�5�5 
DACConsulta
�5�5 
.
�5�5 .
 GetCodPrestadoresOportunidadRips
�5�5 ?
(
�5�5? @
term
�5�5@ D
,
�5�5D E
ref
�5�5F I
MsgRes
�5�5J P
)
�5�5P Q
;
�5�5Q R
}
�5�5 	
public
�5�5 
List
�5�5 
<
�5�5 7
)vw_calidad_opor_citas_y_odont_prestadores
�5�5 =
>
�5�5= >4
&GetPrestadoresCitasmedicasyodontologia
�5�5? e
(
�5�5e f
string
�5�5f l
term
�5�5m q
,
�5�5q r
ref
�5�5s v!
MessageResponseOBJ�5�5w �
MsgRes�5�5� �
)�5�5� �
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 4
&GetPrestadoresCitasmedicasyodontologia
�6�6 E
(
�6�6E F
term
�6�6F J
,
�6�6J K
ref
�6�6L O
MsgRes
�6�6P V
)
�6�6V W
;
�6�6W X
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 8
*vw_calidad_opor_citas_y_odon_profesionales
�6�6 >
>
�6�6> ?6
(GetProfesionalesCitasmedicasyodontologia
�6�6@ h
(
�6�6h i
string
�6�6i o
term
�6�6p t
,
�6�6t u
ref
�6�6v y!
MessageResponseOBJ�6�6z �
MsgRes�6�6� �
)�6�6� �
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 6
(GetProfesionalesCitasmedicasyodontologia
�6�6 G
(
�6�6G H
term
�6�6H L
,
�6�6L M
ref
�6�6N Q
MsgRes
�6�6R X
)
�6�6X Y
;
�6�6Y Z
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 5
'vw_calidad_eventos_adversos_prestadores
�6�6 ;
>
�6�6; <+
GetprestadoresEventosAdversos
�6�6= Z
(
�6�6Z [
string
�6�6[ a
term
�6�6b f
,
�6�6f g
ref
�6�6h k 
MessageResponseOBJ
�6�6l ~
MsgRes�6�6 �
)�6�6� �
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 +
GetprestadoresEventosAdversos
�6�6 <
(
�6�6< =
term
�6�6= A
,
�6�6A B
ref
�6�6C F
MsgRes
�6�6G M
)
�6�6M N
;
�6�6N O
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 6
(vw_calidad_citas_cumplidas_profesionales
�6�6 <
>
�6�6< =,
GetProfesionalesCitasCumplidas
�6�6> \
(
�6�6\ ]
string
�6�6] c
term
�6�6d h
,
�6�6h i
ref
�6�6j m!
MessageResponseOBJ�6�6n �
MsgRes�6�6� �
)�6�6� �
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 ,
GetProfesionalesCitasCumplidas
�6�6 =
(
�6�6= >
term
�6�6> B
,
�6�6B C
ref
�6�6D G
MsgRes
�6�6H N
)
�6�6N O
;
�6�6O P
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 A
3management_insumos_capacidad_resolutiva_listaResult
�6�6 G
>
�6�6G H-
ListaInsumosCapacidadResolutiva
�6�6I h
(
�6�6h i
)
�6�6i j
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 -
ListaInsumosCapacidadResolutiva
�6�6 >
(
�6�6> ?
)
�6�6? @
;
�6�6@ A
}
�6�6 	
public
�6�6 
bool
�6�6 /
!ValidarExistenciaIndicadorCalidad
�6�6 5
(
�6�65 6
int
�6�66 9
mes
�6�6: =
,
�6�6= >
int
�6�6? B
año
�6�6C F
)
�6�6F G
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 /
!ValidarExistenciaIndicadorCalidad
�6�6 @
(
�6�6@ A
mes
�6�6A D
,
�6�6D E
año
�6�6F I
)
�6�6I J
;
�6�6J K
}
�6�6 	
public
�6�6 
void
�6�6 ,
InsertarIndicadoresCalidadDtll
�6�6 2
(
�6�62 3
List
�6�63 7
<
�6�67 8/
!insumos_capacidad_resolutiva_dtll
�6�68 Y
>
�6�6Y Z
List
�6�6[ _
,
�6�6_ `*
insumos_capacidad_resolutiva
�6�6a }
objbase�6�6~ �
,�6�6� �
ref�6�6� �"
MessageResponseOBJ�6�6� �
MsgRes�6�6� �
)�6�6� �
{
�6�6 	

DACInserta
�6�6 
.
�6�6 ,
InsertarIndicadoresCalidadDtll
�6�6 5
(
�6�65 6
List
�6�66 :
,
�6�6: ;
objbase
�6�6< C
,
�6�6C D
ref
�6�6E H
MsgRes
�6�6I O
)
�6�6O P
;
�6�6P Q
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 &
calidad_ref_especialidad
�6�6 ,
>
�6�6, -
GetEspecialidades
�6�6. ?
(
�6�6? @
)
�6�6@ A
{
�6�6 	
return
�6�6 
DACComonClass
�6�6  
.
�6�6  !
GetEspecialidades
�6�6! 2
(
�6�62 3
)
�6�63 4
;
�6�64 5
}
�6�6 	
public
�6�6 
int
�6�6 -
InsertarBaseBeneficiariosMasivo
�6�6 2
(
�6�62 3
List
�6�63 7
<
�6�67 8 
base_beneficiarios
�6�68 J
>
�6�6J K
List
�6�6L P
,
�6�6P Q
ref
�6�6R U 
MessageResponseOBJ
�6�6V h
MsgRes
�6�6i o
)
�6�6o p
{
�6�6 	
return
�6�6 

DACInserta
�6�6 
.
�6�6 -
InsertarBaseBeneficiariosMasivo
�6�6 =
(
�6�6= >
List
�6�6> B
,
�6�6B C
ref
�6�6D G
MsgRes
�6�6H N
)
�6�6N O
;
�6�6O P
}
�6�6 	
public
�6�6 
int
�6�6 *
InsertarLogBaseBeneficiarios
�6�6 /
(
�6�6/ 0+
log_cargue_base_beneficiarios
�6�60 M
obj
�6�6N Q
,
�6�6Q R
ref
�6�6S V 
MessageResponseOBJ
�6�6W i
MsgRes
�6�6j p
)
�6�6p q
{
�6�6 	
return
�6�6 

DACInserta
�6�6 
.
�6�6 *
InsertarLogBaseBeneficiarios
�6�6 :
(
�6�6: ;
obj
�6�6; >
,
�6�6> ?
ref
�6�6@ C
MsgRes
�6�6D J
)
�6�6J K
;
�6�6K L
}
�6�6 	
public
�6�6 
void
�6�6 *
EliminarBaseBeneficiariosEco
�6�6 0
(
�6�60 1
ref
�6�61 4 
MessageResponseOBJ
�6�65 G
MsgRes
�6�6H N
)
�6�6N O
{
�6�6 	

DACElimina
�6�6 
.
�6�6 *
EliminarBaseBeneficiariosEco
�6�6 3
(
�6�63 4
ref
�6�64 7
MsgRes
�6�68 >
)
�6�6> ?
;
�6�6? @
}
�6�6 	
public
�6�6  
base_beneficiarios
�6�6 !+
getUltimoPeriodoBeneficiarios
�6�6" ?
(
�6�6? @
)
�6�6@ A
{
�6�6 	 
base_beneficiarios
�6�6 
list
�6�6 #
=
�6�6$ %
DACConsulta
�6�6& 1
.
�6�61 2+
getUltimoPeriodoBeneficiarios
�6�62 O
(
�6�6O P
)
�6�6P Q
;
�6�6Q R
return
�6�6 
list
�6�6 
;
�6�6 
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 #
ref_adherencia_ciudad
�6�6 )
>
�6�6) *
	GetCiudad
�6�6+ 4
(
�6�64 5
)
�6�65 6
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 
	GetCiudad
�6�6 (
(
�6�6( )
)
�6�6) *
;
�6�6* +
}
�6�6 	
public
�6�6 
int
�6�6 
insertarPrestador
�6�6 $
(
�6�6$ %&
ref_adherencia_prestador
�6�6% =
obj
�6�6> A
,
�6�6A B
List
�6�6C G
<
�6�6G H(
ref_adherencia_profesional
�6�6H b
>
�6�6b c
lista
�6�6d i
,
�6�6i j
int
�6�6k n
creado
�6�6o u
)
�6�6u v
{
�6�6 	
return
�6�6 

DACInserta
�6�6 
.
�6�6 
insertarPrestador
�6�6 /
(
�6�6/ 0
obj
�6�60 3
,
�6�63 4
lista
�6�65 :
,
�6�6: ;
creado
�6�6< B
)
�6�6B C
;
�6�6C D
}
�6�6 	
public
�6�6 
int
�6�6 %
insertarPrestadorCiudad
�6�6 *
(
�6�6* +-
ref_adherencia_prestador_ciudad
�6�6+ J
obj
�6�6K N
)
�6�6N O
{
�6�6 	
return
�6�6 

DACInserta
�6�6 
.
�6�6 %
insertarPrestadorCiudad
�6�6 5
(
�6�65 6
obj
�6�66 9
)
�6�69 :
;
�6�6: ;
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 &
ref_adherencia_prestador
�6�6 ,
>
�6�6, -
traerPrestadores
�6�6. >
(
�6�6> ?
)
�6�6? @
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 
traerPrestadores
�6�6 /
(
�6�6/ 0
)
�6�60 1
;
�6�61 2
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 /
!management_traerPrestadoresResult
�6�6 5
>
�6�65 6 
traerPrestadoresId
�6�67 I
(
�6�6I J
string
�6�6J P
id
�6�6Q S
)
�6�6S T
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6  
traerPrestadoresId
�6�6 1
(
�6�61 2
id
�6�62 4
)
�6�64 5
;
�6�65 6
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 =
/management_baseBeneficiariosPeriodoValidoResult
�6�6 C
>
�6�6C D*
GetBeneficiariosPerodoValido
�6�6E a
(
�6�6a b
int
�6�6b e
mes
�6�6f i
,
�6�6i j
int
�6�6k n
año
�6�6o r
)
�6�6r s
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 *
GetBeneficiariosPerodoValido
�6�6 ;
(
�6�6; <
mes
�6�6< ?
,
�6�6? @
año
�6�6A D
)
�6�6D E
;
�6�6E F
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 #
ref_adherencia_ciudad
�6�6 )
>
�6�6) *
getCiudadesUnis
�6�6+ :
(
�6�6: ;
int
�6�6; >
idUnis
�6�6? E
)
�6�6E F
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 
getCiudadesUnis
�6�6 .
(
�6�6. /
idUnis
�6�6/ 5
)
�6�65 6
;
�6�66 7
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 "
ref_ver_tipoCriterio
�6�6 (
>
�6�6( )!
Get_refTipoCriterio
�6�6* =
(
�6�6= >
)
�6�6> ?
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 !
Get_refTipoCriterio
�6�6 2
(
�6�62 3
)
�6�63 4
;
�6�64 5
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 +
ref_verificacion_farmaceutico
�6�6 1
>
�6�61 2-
Get_refVerificacionFarmaceutita
�6�63 R
(
�6�6R S
)
�6�6S T
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 -
Get_refVerificacionFarmaceutita
�6�6 >
(
�6�6> ?
)
�6�6? @
;
�6�6@ A
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 0
"management_verificacionListaResult
�6�6 6
>
�6�66 7
getTipoCriterioId
�6�68 I
(
�6�6I J
int
�6�6J M
idTipo
�6�6N T
)
�6�6T U
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 
getTipoCriterioId
�6�6 0
(
�6�60 1
idTipo
�6�61 7
)
�6�67 8
;
�6�68 9
}
�6�6 	
public
�6�6 
List
�6�6 
<
�6�6 0
"management_verificacionListaResult
�6�6 6
>
�6�66 7!
getTotalDatosDispen
�6�68 K
(
�6�6K L
)
�6�6L M
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 !
getTotalDatosDispen
�6�6 2
(
�6�62 3
)
�6�63 4
;
�6�64 5
}
�6�6 	
public
�6�6 +
ref_verificacion_farmaceutico
�6�6 ,1
#Get_refVerificacionFarmaceutitaById
�6�6- P
(
�6�6P Q
int
�6�6Q T
	idTipoVer
�6�6U ^
)
�6�6^ _
{
�6�6 	
return
�6�6 
DACConsulta
�6�6 
.
�6�6 1
#Get_refVerificacionFarmaceutitaById
�6�6 B
(
�6�6B C
	idTipoVer
�6�6C L
)
�6�6L M
;
�6�6M N
}
�6�6 	
public
�6�6 
void
�6�6 "
InsertarVerificacion
�6�6 (
(
�6�6( )+
ref_verificacion_farmaceutico
�6�6) F
obj
�6�6G J
,
�6�6J K
ref
�6�6L O 
MessageResponseOBJ
�6�6P b
MsgRes
�6�6c i
)
�6�6i j
{
�6�6 	

DACInserta
�6�6 
.
�6�6 "
InsertarVerificacion
�6�6 +
(
�6�6+ ,
obj
�6�6, /
,
�6�6/ 0
ref
�6�61 4
MsgRes
�6�65 ;
)
�6�6; <
;
�6�6< =
}
�6�6 	
public
�6�6 
void
�6�6 $
ActualizarVerificacion
�6�6 *
(
�6�6* ++
ref_verificacion_farmaceutico
�6�6+ H
obj
�6�6I L
,
�6�6L M
ref
�6�6N Q 
MessageResponseOBJ
�6�6R d
MsgRes
�6�6e k
)
�6�6k l
{
�7�7 	
DACActualiza
�7�7 
.
�7�7 $
ActualizarVerificacion
�7�7 /
(
�7�7/ 0
obj
�7�70 3
,
�7�73 4
ref
�7�75 8
MsgRes
�7�79 ?
)
�7�7? @
;
�7�7@ A
}
�7�7 	
public
�7�7 
void
�7�7 %
InsertarTipoCriteriover
�7�7 +
(
�7�7+ ,"
ref_ver_tipoCriterio
�7�7, @
obj
�7�7A D
,
�7�7D E
ref
�7�7F I 
MessageResponseOBJ
�7�7J \
MsgRes
�7�7] c
)
�7�7c d
{
�7�7 	

DACInserta
�7�7 
.
�7�7 %
InsertarTipoCriteriover
�7�7 .
(
�7�7. /
obj
�7�7/ 2
,
�7�72 3
ref
�7�74 7
MsgRes
�7�78 >
)
�7�7> ?
;
�7�7? @
}
�7�7 	
public
�7�7 
void
�7�7 '
ActualizarTipoCriteriover
�7�7 -
(
�7�7- ."
ref_ver_tipoCriterio
�7�7. B
obj
�7�7C F
,
�7�7F G
ref
�7�7H K 
MessageResponseOBJ
�7�7L ^
MsgRes
�7�7_ e
)
�7�7e f
{
�7�7 	
DACActualiza
�7�7 
.
�7�7 '
ActualizarTipoCriteriover
�7�7 2
(
�7�72 3
obj
�7�73 6
,
�7�76 7
ref
�7�78 ;
MsgRes
�7�7< B
)
�7�7B C
;
�7�7C D
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 
ver_tipocriterio
�7�7 $
>
�7�7$ %"
get_ref_tipoCriterio
�7�7& :
(
�7�7: ;
int
�7�7; >
idVerificacion
�7�7? M
)
�7�7M N
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 "
get_ref_tipoCriterio
�7�7 3
(
�7�73 4
idVerificacion
�7�74 B
)
�7�7B C
;
�7�7C D
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 '
ref_ver_grupo_tpocriterio
�7�7 -
>
�7�7- .&
get_ver_grupoTipoCritero
�7�7/ G
(
�7�7G H
)
�7�7H I
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 &
get_ver_grupoTipoCritero
�7�7 7
(
�7�77 8
)
�7�78 9
;
�7�79 :
}
�7�7 	
public
�7�7 
void
�7�7 '
InsertarAdminCriteriosver
�7�7 -
(
�7�7- .
int
�7�7. 1
tipoVerificacion
�7�72 B
,
�7�7B C
List
�7�7D H
<
�7�7H I
int
�7�7I L
>
�7�7L M
seleccionados
�7�7N [
,
�7�7[ \
List
�7�7] a
<
�7�7a b
int
�7�7b e
>
�7�7e f
seleccionados2
�7�7g u
,
�7�7u v
string
�7�7w }
usuario�7�7~ �
,�7�7� �
ref�7�7� �"
MessageResponseOBJ�7�7� �
MsgRes�7�7� �
)�7�7� �
{
�7�7 	

DACInserta
�7�7 
.
�7�7 '
InsertarAdminCriteriosver
�7�7 0
(
�7�70 1
tipoVerificacion
�7�71 A
,
�7�7A B
seleccionados
�7�7C P
,
�7�7P Q
seleccionados2
�7�7R `
,
�7�7` a
usuario
�7�7b i
,
�7�7i j
ref
�7�7k n
MsgRes
�7�7o u
)
�7�7u v
;
�7�7v w
}
�7�7 	
public
�7�7 
void
�7�7 %
EliminarTipoCriteriover
�7�7 +
(
�7�7+ ,
int
�7�7, /
idtipocriterio
�7�70 >
,
�7�7> ?
ref
�7�7@ C 
MessageResponseOBJ
�7�7D V
MsgRes
�7�7W ]
)
�7�7] ^
{
�7�7 	

DACElimina
�7�7 
.
�7�7 %
EliminarTipoCriteriover
�7�7 .
(
�7�7. /
idtipocriterio
�7�7/ =
,
�7�7= >
ref
�7�7? B
MsgRes
�7�7C I
)
�7�7I J
;
�7�7J K
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 
ver_criterio
�7�7  
>
�7�7  !,
getcriteriosbytipoverificacion
�7�7" @
(
�7�7@ A
int
�7�7A D
tipoverificacion
�7�7E U
)
�7�7U V
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 ,
getcriteriosbytipoverificacion
�7�7 =
(
�7�7= >
tipoverificacion
�7�7> N
)
�7�7N O
;
�7�7O P
}
�7�7 	
public
�7�7 
ver_criterio
�7�7 $
ConsultarCriterioById2
�7�7 2
(
�7�72 3
int
�7�73 6

idcriterio
�7�77 A
)
�7�7A B
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 $
ConsultarCriterioById2
�7�7 5
(
�7�75 6

idcriterio
�7�76 @
)
�7�7@ A
;
�7�7A B
}
�7�7 	
public
�7�7 
void
�7�7 !
InsertarCriteriover
�7�7 '
(
�7�7' (
ver_criterio
�7�7( 4
criterio
�7�75 =
,
�7�7= >
ref
�7�7? B 
MessageResponseOBJ
�7�7C U
MsgRes
�7�7V \
)
�7�7\ ]
{
�7�7 	

DACInserta
�7�7 
.
�7�7 !
InsertarCriteriover
�7�7 *
(
�7�7* +
criterio
�7�7+ 3
,
�7�73 4
ref
�7�75 8
MsgRes
�7�79 ?
)
�7�7? @
;
�7�7@ A
}
�7�7 	
public
�7�7 
void
�7�7 #
ActualizarCriteriover
�7�7 )
(
�7�7) *
ver_criterio
�7�7* 6
criterio
�7�77 ?
,
�7�7? @
ref
�7�7A D 
MessageResponseOBJ
�7�7E W
MsgRes
�7�7X ^
)
�7�7^ _
{
�7�7 	
DACActualiza
�7�7 
.
�7�7 #
ActualizarCriteriover
�7�7 .
(
�7�7. /
criterio
�7�7/ 7
,
�7�77 8
ref
�7�79 <
MsgRes
�7�7= C
)
�7�7C D
;
�7�7D E
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 0
"ref_verificacionFarmaceutica_tipos
�7�7 6
>
�7�76 7"
getTiposVerificacion
�7�78 L
(
�7�7L M
)
�7�7M N
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 "
getTiposVerificacion
�7�7 3
(
�7�73 4
)
�7�74 5
;
�7�75 6
}
�7�7 	
public
�7�7 
void
�7�7 *
EliminarCriterioVerificacion
�7�7 0
(
�7�70 1
int
�7�71 4

idcriterio
�7�75 ?
,
�7�7? @
ref
�7�7A D 
MessageResponseOBJ
�7�7E W
MsgRes
�7�7X ^
)
�7�7^ _
{
�7�7 	

DACElimina
�7�7 
.
�7�7 *
EliminarCriterioVerificacion
�7�7 3
(
�7�73 4

idcriterio
�7�74 >
,
�7�7> ?
ref
�7�7@ C
MsgRes
�7�7D J
)
�7�7J K
;
�7�7K L
}
�7�7 	
public
�7�7 
void
�7�7 -
InsertarCarguePuntoDispensacion
�7�7 3
(
�7�73 4
List
�7�74 8
<
�7�78 9#
ver_puntoDispensacion
�7�79 N
>
�7�7N O
List
�7�7P T
,
�7�7T U
ref
�7�7V Y 
MessageResponseOBJ
�7�7Z l
MsgRes
�7�7m s
)
�7�7s t
{
�7�7 	

DACInserta
�7�7 
.
�7�7 -
InsertarCarguePuntoDispensacion
�7�7 6
(
�7�76 7
List
�7�77 ;
,
�7�7; <
ref
�7�7= @
MsgRes
�7�7A G
)
�7�7G H
;
�7�7H I
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 #
ver_puntoDispensacion
�7�7 )
>
�7�7) *&
getPuntoDispensacionList
�7�7+ C
(
�7�7C D
)
�7�7D E
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 &
getPuntoDispensacionList
�7�7 7
(
�7�77 8
)
�7�78 9
;
�7�79 :
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 ?
1management_dispensacion_archivosRepositorioResult
�7�7 E
>
�7�7E F0
"MostrarArchivosEvaluacionVisitasMD
�7�7G i
(
�7�7i j
int
�7�7j m
?
�7�7m n
idEvaluacion
�7�7o {
)
�7�7{ |
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 0
"MostrarArchivosEvaluacionVisitasMD
�7�7 A
(
�7�7A B
idEvaluacion
�7�7B N
)
�7�7N O
;
�7�7O P
}
�7�7 	
public
�7�7 
int
�7�7 )
ActualizarPuntoDispensacion
�7�7 .
(
�7�7. /#
ver_puntoDispensacion
�7�7/ D
obj
�7�7E H
)
�7�7H I
{
�7�7 	
return
�7�7 
DACActualiza
�7�7 
.
�7�7  )
ActualizarPuntoDispensacion
�7�7  ;
(
�7�7; <
obj
�7�7< ?
)
�7�7? @
;
�7�7@ A
}
�7�7 	
public
�7�7 
int
�7�7 3
%ActualizarAuditadoVisitasDispensacion
�7�7 8
(
�7�78 9#
ver_puntoDispensacion
�7�79 N
obj
�7�7O R
)
�7�7R S
{
�7�7 	
return
�7�7 
DACActualiza
�7�7 
.
�7�7  3
%ActualizarAuditadoVisitasDispensacion
�7�7  E
(
�7�7E F
obj
�7�7F I
)
�7�7I J
;
�7�7J K
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 %
ver_evaluacion_archivos
�7�7 +
>
�7�7+ ,.
 TraerArchivosVisitasDispensacion
�7�7- M
(
�7�7M N
int
�7�7N Q
idEvaluacion
�7�7R ^
)
�7�7^ _
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 .
 TraerArchivosVisitasDispensacion
�7�7 ?
(
�7�7? @
idEvaluacion
�7�7@ L
)
�7�7L M
;
�7�7M N
}
�7�7 	
public
�7�7 
int
�7�7 ,
InsertarEvaluacionDispensacion
�7�7 1
(
�7�71 2#
ver_dispen_evaluacion
�7�72 G
obj
�7�7H K
)
�7�7K L
{
�7�7 	
return
�7�7 

DACInserta
�7�7 
.
�7�7 ,
InsertarEvaluacionDispensacion
�7�7 <
(
�7�7< =
obj
�7�7= @
)
�7�7@ A
;
�7�7A B
}
�7�7 	
public
�7�7 
int
�7�7 1
#InsertarEvaluacionDispensacionTotal
�7�7 6
(
�7�76 7
List
�7�77 ;
<
�7�7; <)
ver_dispen_evaluacion_total
�7�7< W
>
�7�7W X
List
�7�7Y ]
)
�7�7] ^
{
�7�7 	
return
�7�7 

DACInserta
�7�7 
.
�7�7 1
#InsertarEvaluacionDispensacionTotal
�7�7 A
(
�7�7A B
List
�7�7B F
)
�7�7F G
;
�7�7G H
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 >
0management_dispensacion_evaluacionRelacionResult
�7�7 D
>
�7�7D E'
getDispensacionEvaluacion
�7�7F _
(
�7�7_ `
)
�7�7` a
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 (
getDispensacionEvaluacionl
�7�7 9
(
�7�79 :
)
�7�7: ;
;
�7�7; <
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 D
6management_dispensacion_evaluacionRelacion_totalResult
�7�7 J
>
�7�7J K,
getDispensacionEvaluacionTotal
�7�7L j
(
�7�7j k
)
�7�7k l
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 ,
getDispensacionEvaluacionTotal
�7�7 =
(
�7�7= >
)
�7�7> ?
;
�7�7? @
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 >
0management_dispensacion_evaluacionRelacionResult
�7�7 D
>
�7�7D E)
getDispensacionEvaluacionId
�7�7F a
(
�7�7a b
int
�7�7b e
Id
�7�7f h
)
�7�7h i
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 )
getDispensacionEvaluacionId
�7�7 :
(
�7�7: ;
Id
�7�7; =
)
�7�7= >
;
�7�7> ?
}
�7�7 	
public
�7�7 
List
�7�7 
<
�7�7 D
6management_dispensacion_evaluacionRelacion_totalResult
�7�7 J
>
�7�7J K.
 getDispensacionEvaluacionTotalId
�7�7L l
(
�7�7l m
int
�7�7m p
id
�7�7q s
)
�7�7s t
{
�7�7 	
return
�7�7 
DACConsulta
�7�7 
.
�7�7 .
 getDispensacionEvaluacionTotalId
�7�7 ?
(
�7�7? @
id
�7�7@ B
)
�7�7B C
;
�7�7C D
}
�7�7 	
public
�7�7 
int
�7�7 (
InsertarArchivosEvaluacion
�7�7 -
(
�7�7- .%
ver_evaluacion_archivos
�7�7. E
obj
�7�7F I
)
�7�7I J
{
�7�7 	
return
�7�7 

DACInserta
�7�7 
.
�7�7 (
InsertarArchivosEvaluacion
�7�7 8
(
�7�78 9
obj
�7�79 <
)
�7�7< =
;
�7�7= >
}
�7�7 	
public
�7�7 
int
�7�7 ,
InsertarArchivosEvaluacionPDFS
�7�7 1
(
�7�71 2!
ver_evaluacion_pdfs
�7�72 E
obj
�7�7F I
)
�7�7I J
{
�7�7 	
return
�7�7 

DACInserta
�7�7 
.
�7�7 ,
InsertarArchivosEvaluacionPDFS
�7�7 <
(
�7�7< =
obj
�7�7= @
)
�7�7@ A
;
�7�7A B
}
�7�7 	
public
�8�8 !
ver_evaluacion_pdfs
�8�8 "'
TraerPDFEvaluacionVisitas
�8�8# <
(
�8�8< =
int
�8�8= @
idEvaluacion
�8�8A M
)
�8�8M N
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 '
TraerPDFEvaluacionVisitas
�8�8 8
(
�8�88 9
idEvaluacion
�8�89 E
)
�8�8E F
;
�8�8F G
}
�8�8 	
public
�8�8 
int
�8�8 7
)EliminarArchivosPDFevaluacionDispensacion
�8�8 <
(
�8�8< =
int
�8�8= @
idEvaluacion
�8�8A M
)
�8�8M N
{
�8�8 	
return
�8�8 

DACElimina
�8�8 
.
�8�8 7
)EliminarArchivosPDFevaluacionDispensacion
�8�8 G
(
�8�8G H
idEvaluacion
�8�8H T
)
�8�8T U
;
�8�8U V
}
�8�8 	
public
�8�8 
int
�8�8 /
!EliminarArchivosEvaluacionVisitas
�8�8 4
(
�8�84 5
int
�8�85 8
idEvaluacion
�8�89 E
,
�8�8E F
int
�8�8G J
	idArchivo
�8�8K T
)
�8�8T U
{
�8�8 	
return
�8�8 

DACElimina
�8�8 
.
�8�8 /
!EliminarArchivosEvaluacionVisitas
�8�8 ?
(
�8�8? @
idEvaluacion
�8�8@ L
,
�8�8L M
	idArchivo
�8�8N W
)
�8�8W X
;
�8�8X Y
}
�8�8 	
public
�8�8 %
ver_evaluacion_archivos
�8�8 &/
!DescargarArchivoEvaluacionVisitas
�8�8' H
(
�8�8H I
int
�8�8I L
	idArchivo
�8�8M V
)
�8�8V W
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 /
!DescargarArchivoEvaluacionVisitas
�8�8 @
(
�8�8@ A
	idArchivo
�8�8A J
)
�8�8J K
;
�8�8K L
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 G
9management_dispensacion_evaluacionRelacion_hallazgoResult
�8�8 M
>
�8�8M N#
getEvolucionHallazgos
�8�8O d
(
�8�8d e
)
�8�8e f
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 #
getEvolucionHallazgos
�8�8 4
(
�8�84 5
)
�8�85 6
;
�8�86 7
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 M
?management_dispensacion_evaluacionRelacion_total_hallazgoResult
�8�8 S
>
�8�8S T8
*getDispensacionEvaluacionTotalIdHallazgoId
�8�8U 
(�8�8 �
int�8�8� �
id�8�8� �
)�8�8� �
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 8
*getDispensacionEvaluacionTotalIdHallazgoId
�8�8 I
(
�8�8I J
id
�8�8J L
)
�8�8L M
;
�8�8M N
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 (
ref_evaluacion_estadoTotal
�8�8 .
>
�8�8. /+
getEstadosEvaluacionHallazgos
�8�80 M
(
�8�8M N
)
�8�8N O
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 +
getEstadosEvaluacionHallazgos
�8�8 <
(
�8�8< =
)
�8�8= >
;
�8�8> ?
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 )
ref_evaluacion_tipoHallazgo
�8�8 /
>
�8�8/ 0'
getTipoHallazgoEvaluacion
�8�81 J
(
�8�8J K
)
�8�8K L
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 '
getTipoHallazgoEvaluacion
�8�8 8
(
�8�88 9
)
�8�89 :
;
�8�8: ;
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 )
ref_evaluacion_cumplimiento
�8�8 /
>
�8�8/ 0'
getCumplimientoEvaluacion
�8�81 J
(
�8�8J K
)
�8�8K L
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 '
getCumplimientoEvaluacion
�8�8 8
(
�8�88 9
)
�8�89 :
;
�8�8: ;
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 (
ref_evaluacion_tipoSoporte
�8�8 .
>
�8�8. /&
getTipoSoporteEvaluacion
�8�80 H
(
�8�8H I
)
�8�8I J
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 &
getTipoSoporteEvaluacion
�8�8 7
(
�8�87 8
)
�8�88 9
;
�8�89 :
}
�8�8 	
public
�8�8 
int
�8�8 ,
insertarHallazgoItemEvaluacion
�8�8 1
(
�8�81 2%
ver_evaluacion_hallazgo
�8�82 I
obj
�8�8J M
)
�8�8M N
{
�8�8 	
return
�8�8 

DACInserta
�8�8 
.
�8�8 ,
insertarHallazgoItemEvaluacion
�8�8 <
(
�8�8< =
obj
�8�8= @
)
�8�8@ A
;
�8�8A B
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 %
ver_evaluacion_hallazgo
�8�8 +
>
�8�8+ ,%
ExisteHallazgoSubsanado
�8�8- D
(
�8�8D E
int
�8�8E H
idTotal
�8�8I P
,
�8�8P Q
int
�8�8R U
id_tipoCriterio
�8�8V e
)
�8�8e f
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 %
ExisteHallazgoSubsanado
�8�8 6
(
�8�86 7
idTotal
�8�87 >
,
�8�8> ?
id_tipoCriterio
�8�8@ O
)
�8�8O P
;
�8�8P Q
}
�8�8 	
public
�8�8 
int
�8�8 '
ActualizarHallazgoVisitas
�8�8 ,
(
�8�8, -%
ver_evaluacion_hallazgo
�8�8- D
obj
�8�8E H
)
�8�8H I
{
�8�8 	
return
�8�8 
DACActualiza
�8�8 
.
�8�8  '
ActualizarHallazgoVisitas
�8�8  9
(
�8�89 :
obj
�8�8: =
)
�8�8= >
;
�8�8> ?
}
�8�8 	
public
�8�8 
int
�8�8 4
&insertarHallazgoItemEvaluacionArchivos
�8�8 9
(
�8�89 :.
 ver_evaluacion_hallazgo_archivos
�8�8: Z
obj
�8�8[ ^
)
�8�8^ _
{
�8�8 	
return
�8�8 

DACInserta
�8�8 
.
�8�8 4
&insertarHallazgoItemEvaluacionArchivos
�8�8 D
(
�8�8D E
obj
�8�8E H
)
�8�8H I
;
�8�8I J
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 M
?management_dispensacion_evaluacionRelacion_total_hallazgoResult
�8�8 S
>
�8�8S T4
&getDispensacionEvaluacionTotalHallazgo
�8�8U {
(
�8�8{ |
)
�8�8| }
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 4
&getDispensacionEvaluacionTotalHallazgo
�8�8 E
(
�8�8E F
)
�8�8F G
;
�8�8G H
}
�8�8 	
public
�8�8 
int
�8�8 $
SaveCuidadosPaliativos
�8�8 )
(
�8�8) *&
ffmm_cuidados_paliativos
�8�8* B
objeto
�8�8C I
,
�8�8I J
ref
�8�8K N 
MessageResponseOBJ
�8�8O a
MsgRes
�8�8b h
)
�8�8h i
{
�8�8 	
return
�8�8 

DACInserta
�8�8 
.
�8�8 $
SaveCuidadosPaliativos
�8�8 4
(
�8�84 5
objeto
�8�85 ;
,
�8�8; <
ref
�8�8= @
MsgRes
�8�8A G
)
�8�8G H
;
�8�8H I
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 &
Ref_ffmm_unidad_satelite
�8�8 ,
>
�8�8, -
GetUnidadSatelite
�8�8. ?
(
�8�8? @
ref
�8�8@ C 
MessageResponseOBJ
�8�8D V
MsgRes
�8�8W ]
)
�8�8] ^
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 
GetUnidadSatelite
�8�8 0
(
�8�80 1
ref
�8�81 4
MsgRes
�8�85 ;
)
�8�8; <
;
�8�8< =
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8  
Ref_ffmm_unidad_cp
�8�8 &
>
�8�8& '
	GetUnidad
�8�8( 1
(
�8�81 2
ref
�8�82 5 
MessageResponseOBJ
�8�86 H
MsgRes
�8�8I O
)
�8�8O P
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 
	GetUnidad
�8�8 (
(
�8�8( )
ref
�8�8) ,
MsgRes
�8�8- 3
)
�8�83 4
;
�8�84 5
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 "
vw_ffmm_departamento
�8�8 (
>
�8�8( )
GetDepartamentos
�8�8* :
(
�8�8: ;
ref
�8�8; > 
MessageResponseOBJ
�8�8? Q
MsgRes
�8�8R X
)
�8�8X Y
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 
GetDepartamentos
�8�8 /
(
�8�8/ 0
ref
�8�80 3
MsgRes
�8�84 :
)
�8�8: ;
;
�8�8; <
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 
vw_ffmm_municipio
�8�8 %
>
�8�8% &
GetMunicipios
�8�8' 4
(
�8�84 5
ref
�8�85 8 
MessageResponseOBJ
�8�89 K
MsgRes
�8�8L R
)
�8�8R S
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 
GetMunicipios
�8�8 ,
(
�8�8, -
ref
�8�8- 0
MsgRes
�8�81 7
)
�8�87 8
;
�8�88 9
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 
vw_ffmm_municipio
�8�8 %
>
�8�8% &
GetMunicipiosFM
�8�8' 6
(
�8�86 7
int
�8�87 :
idDepartamento
�8�8; I
,
�8�8I J
ref
�8�8K N 
MessageResponseOBJ
�8�8O a
MsgRes
�8�8b h
)
�8�8h i
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 
GetMunicipiosFM
�8�8 .
(
�8�8. /
idDepartamento
�8�8/ =
,
�8�8= >
ref
�8�8? B
MsgRes
�8�8C I
)
�8�8I J
;
�8�8J K
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 %
Ref_ffmm_tipo_visita_cp
�8�8 +
>
�8�8+ ,
GetTipoVisita
�8�8- :
(
�8�8: ;
ref
�8�8; > 
MessageResponseOBJ
�8�8? Q
MsgRes
�8�8R X
)
�8�8X Y
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 
GetTipoVisita
�8�8 ,
(
�8�8, -
ref
�8�8- 0
MsgRes
�8�81 7
)
�8�87 8
;
�8�88 9
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 
vw_ffmm_ips
�8�8 
>
�8�8  
GetIPS
�8�8! '
(
�8�8' (
ref
�8�8( + 
MessageResponseOBJ
�8�8, >
MsgRes
�8�8? E
)
�8�8E F
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 
GetIPS
�8�8 %
(
�8�8% &
ref
�8�8& )
MsgRes
�8�8* 0
)
�8�80 1
;
�8�81 2
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 !
Ref_tipo_documental
�8�8 '
>
�8�8' (#
GetTipoIdentificacion
�8�8) >
(
�8�8> ?
ref
�8�8? B 
MessageResponseOBJ
�8�8C U
MsgRes
�8�8V \
)
�8�8\ ]
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 #
GetTipoIdentificacion
�8�8 4
(
�8�84 5
ref
�8�85 8
MsgRes
�8�89 ?
)
�8�8? @
;
�8�8@ A
}
�8�8 	
public
�8�8 
ref_solucionador
�8�8 #
UltimaRegionalUsuario
�8�8  5
(
�8�85 6
string
�8�86 <
nombre
�8�8= C
)
�8�8C D
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 #
UltimaRegionalUsuario
�8�8 4
(
�8�84 5
nombre
�8�85 ;
)
�8�8; <
;
�8�8< =
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 
Ref_ffmm_sexo
�8�8 !
>
�8�8! "
GetSexo
�8�8# *
(
�8�8* +
ref
�8�8+ . 
MessageResponseOBJ
�8�8/ A
MsgRes
�8�8B H
)
�8�8H I
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 
GetSexo
�8�8 &
(
�8�8& '
ref
�8�8' *
MsgRes
�8�8+ 1
)
�8�81 2
;
�8�82 3
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8  
Ref_ffmm_unidad_cp
�8�8 &
>
�8�8& '!
GetSitioAdscripcion
�8�8( ;
(
�8�8; <
ref
�8�8< ? 
MessageResponseOBJ
�8�8@ R
MsgRes
�8�8S Y
)
�8�8Y Z
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 !
GetSitioAdscripcion
�8�8 2
(
�8�82 3
ref
�8�83 6
MsgRes
�8�87 =
)
�8�8= >
;
�8�8> ?
}
�8�8 	
public
�8�8 
List
�8�8 
<
�8�8 &
Ref_ffmm_tipo_afiliacion
�8�8 ,
>
�8�8, -
GetTipoAfiliacion
�8�8. ?
(
�8�8? @
ref
�8�8@ C 
MessageResponseOBJ
�8�8D V
MsgRes
�8�8W ]
)
�8�8] ^
{
�8�8 	
return
�8�8 
DACConsulta
�8�8 
.
�8�8 
GetTipoAfiliacion
�8�8 0
(
�8�80 1
ref
�8�81 4
MsgRes
�8�85 ;
)
�8�8; <
;
�8�8< =
}
�9�9 	
public
�9�9 
List
�9�9 
<
�9�9 
Ref_ffmm_estado
�9�9 #
>
�9�9# $
	GetEstado
�9�9% .
(
�9�9. /
ref
�9�9/ 2 
MessageResponseOBJ
�9�93 E
MsgRes
�9�9F L
)
�9�9L M
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 
	GetEstado
�9�9 (
(
�9�9( )
ref
�9�9) ,
MsgRes
�9�9- 3
)
�9�93 4
;
�9�94 5
}
�9�9 	
public
�9�9 
List
�9�9 
<
�9�9 
Ref_ffmm_fuerza
�9�9 #
>
�9�9# $
	GetFuerza
�9�9% .
(
�9�9. /
ref
�9�9/ 2 
MessageResponseOBJ
�9�93 E
MsgRes
�9�9F L
)
�9�9L M
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 
	GetFuerza
�9�9 (
(
�9�9( )
ref
�9�9) ,
MsgRes
�9�9- 3
)
�9�93 4
;
�9�94 5
}
�9�9 	
public
�9�9 
List
�9�9 
<
�9�9 
Ref_ffmm_sino
�9�9 !
>
�9�9! "
GetSiNo
�9�9# *
(
�9�9* +
ref
�9�9+ . 
MessageResponseOBJ
�9�9/ A
MsgRes
�9�9B H
)
�9�9H I
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 
GetSiNo
�9�9 &
(
�9�9& '
ref
�9�9' *
MsgRes
�9�9+ 1
)
�9�91 2
;
�9�92 3
}
�9�9 	
public
�9�9 
List
�9�9 
<
�9�9 
vw_cie10
�9�9 
>
�9�9 
GetCie10
�9�9 &
(
�9�9& '
ref
�9�9' * 
MessageResponseOBJ
�9�9+ =
MsgRes
�9�9> D
)
�9�9D E
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 
GetCie10
�9�9 '
(
�9�9' (
ref
�9�9( +
MsgRes
�9�9, 2
)
�9�92 3
;
�9�93 4
}
�9�9 	
public
�9�9 
List
�9�9 
<
�9�9 
vw_ffmm_glosas
�9�9 "
>
�9�9" #
GetFFMMGlosas
�9�9$ 1
(
�9�91 2
ref
�9�92 5 
MessageResponseOBJ
�9�96 H
MsgRes
�9�9I O
)
�9�9O P
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 
GetFFMMGlosas
�9�9 ,
(
�9�9, -
ref
�9�9- 0
MsgRes
�9�91 7
)
�9�97 8
;
�9�98 9
}
�9�9 	
public
�9�9 
int
�9�9 
CargueCorreosPPE
�9�9 #
(
�9�9# $
List
�9�9$ (
<
�9�9( )(
ecop_directorioPPE_correos
�9�9) C
>
�9�9C D
listadoCargue
�9�9E R
,
�9�9R S
ref
�9�9T W 
MessageResponseOBJ
�9�9X j
MsgRes
�9�9k q
)
�9�9q r
{
�9�9 	
return
�9�9 

DACInserta
�9�9 
.
�9�9 
CargueCorreosPPE
�9�9 .
(
�9�9. /
listadoCargue
�9�9/ <
,
�9�9< =
ref
�9�9> A
MsgRes
�9�9B H
)
�9�9H I
;
�9�9I J
}
�9�9 	
public
�9�9 
int
�9�9  
eliminarCorreosPPE
�9�9 %
(
�9�9% &
ref
�9�9& ) 
MessageResponseOBJ
�9�9* <
MsgRes
�9�9= C
)
�9�9C D
{
�9�9 	
return
�9�9 

DACElimina
�9�9 
.
�9�9  
eliminarCorreosPPE
�9�9 0
(
�9�90 1
ref
�9�91 4
MsgRes
�9�95 ;
)
�9�9; <
;
�9�9< =
}
�9�9 	
public
�9�9 
Int32
�9�9 &
SaveProgramarVisitaGlosa
�9�9 -
(
�9�9- .
ffmm_glosas
�9�9. 9
objeto
�9�9: @
,
�9�9@ A
ref
�9�9B E 
MessageResponseOBJ
�9�9F X
MsgRes
�9�9Y _
)
�9�9_ `
{
�9�9 	
return
�9�9 

DACInserta
�9�9 
.
�9�9 &
SaveProgramarVisitaGlosa
�9�9 6
(
�9�96 7
objeto
�9�97 =
,
�9�9= >
ref
�9�9? B
MsgRes
�9�9C I
)
�9�9I J
;
�9�9J K
}
�9�9 	
public
�9�9 
Int32
�9�9 
UpdateGlosa
�9�9  
(
�9�9  !
ffmm_glosas
�9�9! ,
objeto
�9�9- 3
,
�9�93 4
string
�9�95 ;
caso
�9�9< @
,
�9�9@ A
ref
�9�9B E 
MessageResponseOBJ
�9�9F X
MsgRes
�9�9Y _
)
�9�9_ `
{
�9�9 	
return
�9�9 
DACActualiza
�9�9 
.
�9�9  
UpdateGlosa
�9�9  +
(
�9�9+ ,
objeto
�9�9, 2
,
�9�92 3
caso
�9�94 8
,
�9�98 9
ref
�9�9: =
MsgRes
�9�9> D
)
�9�9D E
;
�9�9E F
}
�9�9 	
public
�9�9 
List
�9�9 
<
�9�9 $
ffmm_Cuentas_auditoria
�9�9 *
>
�9�9* +!
GetCuentasAuditoria
�9�9, ?
(
�9�9? @
ref
�9�9@ C 
MessageResponseOBJ
�9�9D V
MsgRes
�9�9W ]
)
�9�9] ^
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 !
GetCuentasAuditoria
�9�9 2
(
�9�92 3
ref
�9�93 6
MsgRes
�9�97 =
)
�9�9= >
;
�9�9> ?
}
�9�9 	
public
�9�9 
Int32
�9�9 (
UpdateProgramarVisitaGlosa
�9�9 /
(
�9�9/ 0
ffmm_glosas
�9�90 ;
objeto
�9�9< B
,
�9�9B C
ref
�9�9D G 
MessageResponseOBJ
�9�9H Z
MsgRes
�9�9[ a
)
�9�9a b
{
�9�9 	
return
�9�9 
DACActualiza
�9�9 
.
�9�9  (
UpdateProgramarVisitaGlosa
�9�9  :
(
�9�9: ;
objeto
�9�9; A
,
�9�9A B
ref
�9�9C F
MsgRes
�9�9G M
)
�9�9M N
;
�9�9N O
}
�9�9 	
public
�9�9 !
ffmm_cuentas_glosas
�9�9 "
GetCuentasGlosas
�9�9# 3
(
�9�93 4
int
�9�94 7
id_glosa
�9�98 @
,
�9�9@ A
ref
�9�9B E 
MessageResponseOBJ
�9�9F X
MsgRes
�9�9Y _
)
�9�9_ `
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 
GetCuentasGlosas
�9�9 /
(
�9�9/ 0
id_glosa
�9�90 8
,
�9�98 9
ref
�9�9: =
MsgRes
�9�9> D
)
�9�9D E
;
�9�9E F
}
�9�9 	
public
�9�9 
ffmm_glosas
�9�9 
	GetGlosas
�9�9 $
(
�9�9$ %
int
�9�9% (
id_glosa
�9�9) 1
,
�9�91 2
ref
�9�93 6 
MessageResponseOBJ
�9�97 I
MsgRes
�9�9J P
)
�9�9P Q
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 
	GetGlosas
�9�9 (
(
�9�9( )
id_glosa
�9�9) 1
,
�9�91 2
ref
�9�93 6
MsgRes
�9�97 =
)
�9�9= >
;
�9�9> ?
}
�9�9 	
public
�9�9 $
ffmm_Cuentas_auditoria
�9�9 %%
ultimoPagoyConciliacion
�9�9& =
(
�9�9= >
)
�9�9> ?
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 %
ultimoPagoyConciliacion
�9�9 6
(
�9�96 7
)
�9�97 8
;
�9�98 9
}
�9�9 	
public
�9�9 
List
�9�9 
<
�9�9 1
#management_unionFuerzasgradosResult
�9�9 7
>
�9�97 8
getUnionFuerzas
�9�99 H
(
�9�9H I
int
�9�9I L
idFuerza
�9�9M U
)
�9�9U V
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 
getUnionFuerzas
�9�9 .
(
�9�9. /
idFuerza
�9�9/ 7
)
�9�97 8
;
�9�98 9
}
�9�9 	
public
�9�9 
int
�9�9 4
&InsertarDispensacionMedicamentosCargue
�9�9 9
(
�9�99 :(
medicamentos_dispen_cargue
�9�9: T
objbase
�9�9U \
,
�9�9\ ]
ref
�9�9^ a 
MessageResponseOBJ
�9�9b t
MsgRes
�9�9u {
)
�9�9{ |
{
�9�9 	
return
�9�9 

DACInserta
�9�9 
.
�9�9 4
&InsertarDispensacionMedicamentosCargue
�9�9 D
(
�9�9D E
objbase
�9�9E L
,
�9�9L M
ref
�9�9N Q
MsgRes
�9�9R X
)
�9�9X Y
;
�9�9Y Z
}
�9�9 	
public
�9�9 
void
�9�9 "
EliminarCargueDispen
�9�9 (
(
�9�9( )
int
�9�9) ,
idCargue
�9�9- 5
,
�9�95 6
ref
�9�97 : 
MessageResponseOBJ
�9�9; M
MsgRes
�9�9N T
)
�9�9T U
{
�9�9 	

DACElimina
�9�9 
.
�9�9 "
EliminarCargueDispen
�9�9 +
(
�9�9+ ,
idCargue
�9�9, 4
,
�9�94 5
ref
�9�96 9
MsgRes
�9�9: @
)
�9�9@ A
;
�9�9A B
}
�9�9 	
public
�9�9 
void
�9�9 &
EliminarCargueDispendtll
�9�9 ,
(
�9�9, -
int
�9�9- 0
idCargue
�9�91 9
,
�9�99 :
ref
�9�9; > 
MessageResponseOBJ
�9�9? Q
MsgRes
�9�9R X
)
�9�9X Y
{
�9�9 	

DACElimina
�9�9 
.
�9�9 &
EliminarCargueDispendtll
�9�9 /
(
�9�9/ 0
idCargue
�9�90 8
,
�9�98 9
ref
�9�9: =
MsgRes
�9�9> D
)
�9�9D E
;
�9�9E F
}
�9�9 	
public
�9�9 
int
�9�9 5
'InsertarDispensacionMedicamentosCalidad
�9�9 :
(
�9�9: ;
List
�9�9; ?
<
�9�9? @)
medicamentos_dispen_calidad
�9�9@ [
>
�9�9[ \
List
�9�9] a
,
�9�9a b
Int32
�9�9c h
	id_cargue
�9�9i r
,
�9�9r s
ref
�9�9t w!
MessageResponseOBJ�9�9x �
MsgRes�9�9� �
)�9�9� �
{
�9�9 	
return
�9�9 

DACInserta
�9�9 
.
�9�9 5
'InsertarDispensacionMedicamentosCalidad
�9�9 E
(
�9�9E F
List
�9�9F J
,
�9�9J K
	id_cargue
�9�9L U
,
�9�9U V
ref
�9�9W Z
MsgRes
�9�9[ a
)
�9�9a b
;
�9�9b c
}
�9�9 	
public
�9�9 
List
�9�9 
<
�9�9 6
(management_medicamentosDispen_listResult
�9�9 <
>
�9�9< =+
ListaMedicamentosDispensacion
�9�9> [
(
�9�9[ \
)
�9�9\ ]
{
�9�9 	
return
�9�9 
DACConsulta
�9�9 
.
�9�9 +
ListaMedicamentosDispensacion
�9�9 <
(
�9�9< =
)
�9�9= >
;
�9�9> ?
}
�9�9 	
public
�9�9 
List
�9�9 
<
�9�9 9
+management_medicamentosDispen_reporteResult
�9�9 ?
>
�9�9? @2
$ListaMedicamentosDispensacionReporte
�9�9A e
(
�9�9e f
int
�9�9f i
idCargue
�9�9j r
)
�9�9r s
{
�9�9 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: 2
$ListaMedicamentosDispensacionReporte
�:�: C
(
�:�:C D
idCargue
�:�:D L
)
�:�:L M
;
�:�:M N
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: 4
&management_listaMedicDspensacionResult
�:�: :
>
�:�:: ;6
(ListaMedicamentosDispensacionPrestadores
�:�:< d
(
�:�:d e
int
�:�:e h
mes
�:�:i l
,
�:�:l m
int
�:�:n q
año
�:�:r u
)
�:�:u v
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: 6
(ListaMedicamentosDispensacionPrestadores
�:�: G
(
�:�:G H
mes
�:�:H K
,
�:�:K L
año
�:�:M P
)
�:�:P Q
;
�:�:Q R
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: ?
1management_listaMedicDspensacion_agrupacionResult
�:�: E
>
�:�:E F@
2ListaMedicamentosDispensacionPrestadoresAgrupacion
�:�:G y
(
�:�:y z
int
�:�:z }
mes�:�:~ �
,�:�:� �
int�:�:� �
año�:�:� �
)�:�:� �
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: @
2ListaMedicamentosDispensacionPrestadoresAgrupacion
�:�: Q
(
�:�:Q R
mes
�:�:R U
,
�:�:U V
año
�:�:W Z
)
�:�:Z [
;
�:�:[ \
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: :
,management_medicamentosDispen_consultaResult
�:�: @
>
�:�:@ A-
ListaMedicamentosDispenConsulta
�:�:B a
(
�:�:a b
DateTime
�:�:b j
fechaIni
�:�:k s
,
�:�:s t
DateTime
�:�:u }
fechaFin�:�:~ �
)�:�:� �
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: -
ListaMedicamentosDispenConsulta
�:�: >
(
�:�:> ?
fechaIni
�:�:? G
,
�:�:G H
fechaFin
�:�:I Q
)
�:�:Q R
;
�:�:R S
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: A
3management_medicamentosDispen_consulta_armadaResult
�:�: G
>
�:�:G H3
%ListaMedicamentosDispenConsultaArmada
�:�:I n
(
�:�:n o
DateTime
�:�:o w
fechaIni�:�:x �
,�:�:� �
DateTime�:�:� �
fechaFin�:�:� �
,�:�:� �
string�:�:� �
	documento�:�:� �
,�:�:� �
string�:�:� �
familiar�:�:� �
,�:�:� �
string�:�:� �
formula�:�:� �
)�:�:� �
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: 3
%ListaMedicamentosDispenConsultaArmada
�:�: D
(
�:�:D E
fechaIni
�:�:E M
,
�:�:M N
fechaFin
�:�:O W
,
�:�:W X
	documento
�:�:Y b
,
�:�:b c
familiar
�:�:d l
,
�:�:l m
formula
�:�:n u
)
�:�:u v
;
�:�:v w
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: F
8management_medicamentosDispen_consulta_filtros_docResult
�:�: L
>
�:�:L M6
(ListaMedicamentosDispenConsultaFiltroDoc
�:�:N v
(
�:�:v w
string
�:�:w }
	documento�:�:~ �
)�:�:� �
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: 6
(ListaMedicamentosDispenConsultaFiltroDoc
�:�: G
(
�:�:G H
	documento
�:�:H Q
)
�:�:Q R
;
�:�:R S
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: K
=management_medicamentosDispen_consulta_filtros_familiarResult
�:�: Q
>
�:�:Q R<
-ListaMedicamentosDispenConsultaFiltroFamiliar�:�:S �
(�:�:� �
string�:�:� �
familia�:�:� �
)�:�:� �
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: ;
-ListaMedicamentosDispenConsultaFiltroFamiliar
�:�: L
(
�:�:L M
familia
�:�:M T
)
�:�:T U
;
�:�:U V
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: J
<management_medicamentosDispen_consulta_filtros_formulaResult
�:�: P
>
�:�:P Q8
*ListaMedicamentosDispenConsultaFiltroFormu
�:�:R |
(
�:�:| }
string�:�:} �
	documento�:�:� �
)�:�:� �
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: 8
*ListaMedicamentosDispenConsultaFiltroFormu
�:�: I
(
�:�:I J
	documento
�:�:J S
)
�:�:S T
;
�:�:T U
}
�:�: 	
public
�:�: 
int
�:�: "
ValidaExisteAnalista
�:�: '
(
�:�:' (
int
�:�:( +
regional
�:�:, 4
,
�:�:4 5
int
�:�:6 9
unis
�:�:: >
,
�:�:> ?
int
�:�:@ C
analista
�:�:D L
)
�:�:L M
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: "
ValidaExisteAnalista
�:�: 3
(
�:�:3 4
regional
�:�:4 <
,
�:�:< =
unis
�:�:> B
,
�:�:B C
analista
�:�:D L
)
�:�:L M
;
�:�:M N
}
�:�: 	
public
�:�: *
ref_cuentas_medicas_analista
�:�: +&
TraerAnalistasIngresados
�:�:, D
(
�:�:D E*
ref_cuentas_medicas_analista
�:�:E a
obj
�:�:b e
)
�:�:e f
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: &
TraerAnalistasIngresados
�:�: 7
(
�:�:7 8
obj
�:�:8 ;
)
�:�:; <
;
�:�:< =
}
�:�: 	
public
�:�: $
vw_analistas_recepcion
�:�: %
TraerAnalista
�:�:& 3
(
�:�:3 4
int
�:�:4 7
	idUsuario
�:�:8 A
)
�:�:A B
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: 
TraerAnalista
�:�: ,
(
�:�:, -
	idUsuario
�:�:- 6
)
�:�:6 7
;
�:�:7 8
}
�:�: 	
public
�:�: 
int
�:�: 
InsertarAnalistas
�:�: $
(
�:�:$ %
List
�:�:% )
<
�:�:) **
ref_cuentas_medicas_analista
�:�:* F
>
�:�:F G
obj
�:�:H K
)
�:�:K L
{
�:�: 	
return
�:�: 

DACInserta
�:�: 
.
�:�: 
InsertarAnalistas
�:�: /
(
�:�:/ 0
obj
�:�:0 3
)
�:�:3 4
;
�:�:4 5
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: 0
"ManagmentRipsHomologacionFacResult
�:�: 6
>
�:�:6 7%
ConsultaHomologacionFac
�:�:8 O
(
�:�:O P
String
�:�:P V
num_factura
�:�:W b
,
�:�:b c
String
�:�:d j
tipo_id_prestador
�:�:k |
,
�:�:| }
String�:�:~ � 
num_id_prestador�:�:� �
)�:�:� �
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: %
ConsultaHomologacionFac
�:�: 6
(
�:�:6 7
num_factura
�:�:7 B
,
�:�:B C
tipo_id_prestador
�:�:D U
,
�:�:U V
num_id_prestador
�:�:W g
)
�:�:g h
;
�:�:h i
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: 4
&ManagmentRipsHomologacionFacDTLLResult
�:�: :
>
�:�:: ;)
ConsultaHomologacionFacdtll
�:�:< W
(
�:�:W X
String
�:�:X ^
num_factura
�:�:_ j
,
�:�:j k
String
�:�:l r 
tipo_id_prestador�:�:s �
,�:�:� �
String�:�:� � 
num_id_prestador�:�:� �
,�:�:� �
Int32�:�:� �
id_rips�:�:� �
)�:�:� �
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: )
ConsultaHomologacionFacdtll
�:�: :
(
�:�:: ;
num_factura
�:�:; F
,
�:�:F G
tipo_id_prestador
�:�:H Y
,
�:�:Y Z
num_id_prestador
�:�:[ k
,
�:�:k l
id_rips
�:�:m t
)
�:�:t u
;
�:�:u v
}
�:�: 	
public
�:�: 
int
�:�: (
Insertar_rips_homologacion
�:�: -
(
�:�:- .
rips_homologacion
�:�:. ?
objbase
�:�:@ G
,
�:�:G H
ref
�:�:I L 
MessageResponseOBJ
�:�:M _
MsgRes
�:�:` f
)
�:�:f g
{
�:�: 	
return
�:�: 

DACInserta
�:�: 
.
�:�: (
Insertar_rips_homologacion
�:�: 8
(
�:�:8 9
objbase
�:�:9 @
,
�:�:@ A
ref
�:�:B E
MsgRes
�:�:F L
)
�:�:L M
;
�:�:M N
}
�:�: 	
public
�:�: 
int
�:�: -
Insertar_rips_homologacion_dtll
�:�: 2
(
�:�:2 3
List
�:�:3 7
<
�:�:7 8$
rips_homologacion_dtll
�:�:8 N
>
�:�:N O
objbase
�:�:P W
,
�:�:W X
ref
�:�:Y \ 
MessageResponseOBJ
�:�:] o
MsgRes
�:�:p v
)
�:�:v w
{
�:�: 	
return
�:�: 

DACInserta
�:�: 
.
�:�: -
Insertar_rips_homologacion_dtll
�:�: =
(
�:�:= >
objbase
�:�:> E
,
�:�:E F
ref
�:�:G J
MsgRes
�:�:K Q
)
�:�:Q R
;
�:�:R S
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: 
rips_homologacion
�:�: %
>
�:�:% &$
Traerhomologacion_dtll
�:�:' =
(
�:�:= >
rips_homologacion
�:�:> O
obj
�:�:P S
)
�:�:S T
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: $
Traerhomologacion_dtll
�:�: 5
(
�:�:5 6
obj
�:�:6 9
)
�:�:9 :
;
�:�:: ;
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: 9
+ManagmentRipsHomologacionFacDTLLFinalResult
�:�: ?
>
�:�:? @.
 ConsultaHomologacionFacdtllFinal
�:�:A a
(
�:�:a b
String
�:�:b h
num_factura
�:�:i t
,
�:�:t u
Int32
�:�:v {
id_rips�:�:| �
)�:�:� �
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: .
 ConsultaHomologacionFacdtllFinal
�:�: ?
(
�:�:? @
num_factura
�:�:@ K
,
�:�:K L
id_rips
�:�:M T
)
�:�:T U
;
�:�:U V
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: ,
vw_rips_homologacion_tarifario
�:�: 2
>
�:�:2 3
TarifarioRips
�:�:4 A
(
�:�:A B
)
�:�:B C
{
�:�: 	
return
�:�: 
DACComonClass
�:�:  
.
�:�:  !
TarifarioRips
�:�:! .
(
�:�:. /
)
�:�:/ 0
;
�:�:0 1
}
�:�: 	
public
�:�: 
int
�:�: .
 Actualizar_md_Lupe_cargue_base_H
�:�: 3
(
�:�:3 4$
rips_homologacion_dtll
�:�:4 J
obj
�:�:K N
,
�:�:N O
ref
�:�:P S 
MessageResponseOBJ
�:�:T f
MsgRes
�:�:g m
)
�:�:m n
{
�:�: 	
return
�:�: 
DACActualiza
�:�: 
.
�:�:  .
 Actualizar_md_Lupe_cargue_base_H
�:�:  @
(
�:�:@ A
obj
�:�:A D
,
�:�:D E
ref
�:�:F I
MsgRes
�:�:J P
)
�:�:P Q
;
�:�:Q R
}
�:�: 	
public
�:�: 
int
�:�: .
 Actualizar_md_Lupe_cargue_base_G
�:�: 3
(
�:�:3 4$
rips_homologacion_dtll
�:�:4 J
obj
�:�:K N
,
�:�:N O
ref
�:�:P S 
MessageResponseOBJ
�:�:T f
MsgRes
�:�:g m
)
�:�:m n
{
�:�: 	
return
�:�: 
DACActualiza
�:�: 
.
�:�:  .
 Actualizar_md_Lupe_cargue_base_G
�:�:  @
(
�:�:@ A
obj
�:�:A D
,
�:�:D E
ref
�:�:F I
MsgRes
�:�:J P
)
�:�:P Q
;
�:�:Q R
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: 5
'ManagmentRipsHomologacionFacFinalResult
�:�: ;
>
�:�:; <*
ConsultaHomologacionFacFinal
�:�:= Y
(
�:�:Y Z
String
�:�:Z `
num_factura
�:�:a l
,
�:�:l m
String
�:�:n t 
tipo_id_prestador�:�:u �
,�:�:� �
String�:�:� � 
num_id_prestador�:�:� �
,�:�:� �
Int32�:�:� �
id_rips�:�:� �
)�:�:� �
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: *
ConsultaHomologacionFacFinal
�:�: ;
(
�:�:; <
num_factura
�:�:< G
,
�:�:G H
tipo_id_prestador
�:�:I Z
,
�:�:Z [
num_id_prestador
�:�:\ l
,
�:�:l m
id_rips
�:�:n u
)
�:�:u v
;
�:�:v w
}
�:�: 	
public
�:�: 
int
�:�: *
Actualizar_rips_homologacion
�:�: /
(
�:�:/ 0
rips_homologacion
�:�:0 A
obj
�:�:B E
,
�:�:E F
ref
�:�:G J 
MessageResponseOBJ
�:�:K ]
MsgRes
�:�:^ d
)
�:�:d e
{
�:�: 	
return
�:�: 
DACActualiza
�:�: 
.
�:�:  *
Actualizar_rips_homologacion
�:�:  <
(
�:�:< =
obj
�:�:= @
,
�:�:@ A
ref
�:�:B E
MsgRes
�:�:F L
)
�:�:L M
;
�:�:M N
}
�:�: 	
public
�:�: 
void
�:�: ,
ActualizarFacturas_automaticas
�:�: 2
(
�:�:2 3
int
�:�:3 6
idBaseFactura
�:�:7 D
)
�:�:D E
{
�:�: 	
DACActualiza
�:�: 
.
�:�: ,
ActualizarFacturas_automaticas
�:�: 7
(
�:�:7 8
idBaseFactura
�:�:8 E
)
�:�:E F
;
�:�:F G
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: ;
-management_gastoServicio_nombreServicioResult
�:�: A
>
�:�:A B(
ConsultarNombreServicioGXS
�:�:C ]
(
�:�:] ^
string
�:�:^ d
nombre
�:�:e k
)
�:�:k l
{
�:�: 	
return
�:�: 
DACConsulta
�:�: 
.
�:�: (
ConsultarNombreServicioGXS
�:�: 9
(
�:�:9 :
nombre
�:�:: @
)
�:�:@ A
;
�:�:A B
}
�:�: 	
public
�:�: 
int
�:�: %
EliminarTotalEvaluacion
�:�: *
(
�:�:* +
int
�:�:+ .
idEvaluacion
�:�:/ ;
)
�:�:; <
{
�:�: 	
return
�:�: 

DACElimina
�:�: 
.
�:�: %
EliminarTotalEvaluacion
�:�: 5
(
�:�:5 6
idEvaluacion
�:�:6 B
)
�:�:B C
;
�:�:C D
}
�:�: 	
public
�:�: 
int
�:�: *
ActualizarVisitaDispensacion
�:�: /
(
�:�:/ 0#
ver_dispen_evaluacion
�:�:0 E
obj
�:�:F I
)
�:�:I J
{
�:�: 	
return
�:�: 
DACActualiza
�:�: 
.
�:�:  *
ActualizarVisitaDispensacion
�:�:  <
(
�:�:< =
obj
�:�:= @
)
�:�:@ A
;
�:�:A B
}
�:�: 	
public
�:�: 
int
�:�: 3
%EliminarEvaluacionVisitasAutoguardado
�:�: 8
(
�:�:8 9
int
�:�:9 <
idEvaluacion
�:�:= I
)
�:�:I J
{
�:�: 	
return
�:�: 

DACElimina
�:�: 
.
�:�: 3
%EliminarEvaluacionVisitasAutoguardado
�:�: C
(
�:�:C D
idEvaluacion
�:�:D P
)
�:�:P Q
;
�:�:Q R
}
�:�: 	
public
�:�: 
List
�:�: 
<
�:�: >
0management_informacionUsuarios_prestadoresResult
�:�: D
>
�:�:D E!
UsuariosPrestadores
�:�:F Y
(
�:�:Y Z
string
�:�:Z `
nit
�:�:a d
,
�:�:d e
string
�:�:f l
nombre
�:�:m s
,
�:�:s t
string
�:�:u {
cedula�:�:| �
)�:�:� �
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; !
UsuariosPrestadores
�;�; 2
(
�;�;2 3
nit
�;�;3 6
,
�;�;6 7
nombre
�;�;8 >
,
�;�;> ?
cedula
�;�;@ F
)
�;�;F G
;
�;�;G H
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; E
7management_informacionUsuarios_prestadoresDetalleResult
�;�; K
>
�;�;K L(
UsuariosPrestadoresDetalle
�;�;M g
(
�;�;g h
int
�;�;h k
	idUsuario
�;�;l u
)
�;�;u v
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; (
UsuariosPrestadoresDetalle
�;�; 9
(
�;�;9 :
	idUsuario
�;�;: C
)
�;�;C D
;
�;�;D E
}
�;�; 	
public
�;�; 
int
�;�; "
EliminarLoteFacturas
�;�; '
(
�;�;' (
int
�;�;( +
id
�;�;, .
)
�;�;. /
{
�;�; 	
return
�;�; 

DACElimina
�;�; 
.
�;�; "
EliminarLoteFacturas
�;�; 2
(
�;�;2 3
id
�;�;3 5
)
�;�;5 6
;
�;�;6 7
}
�;�; 	
public
�;�; 
sis_usuario
�;�; 
datosUsuarioId
�;�; )
(
�;�;) *
int
�;�;* -
	idUsuario
�;�;. 7
)
�;�;7 8
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; 
datosUsuarioId
�;�; -
(
�;�;- .
	idUsuario
�;�;. 7
)
�;�;7 8
;
�;�;8 9
}
�;�; 	
public
�;�; 
sis_usuario
�;�; 
datosUsuarioRol
�;�; *
(
�;�;* +
int
�;�;+ .
?
�;�;. /
idRol
�;�;0 5
)
�;�;5 6
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; 
datosUsuarioRol
�;�; .
(
�;�;. /
idRol
�;�;/ 4
)
�;�;4 5
;
�;�;5 6
}
�;�; 	
public
�;�; 
sis_usuario
�;�; 
datosUsuarioUser
�;�; +
(
�;�;+ ,
string
�;�;, 2
usuario
�;�;3 :
)
�;�;: ;
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; 
datosUsuarioUser
�;�; /
(
�;�;/ 0
usuario
�;�;0 7
)
�;�;7 8
;
�;�;8 9
}
�;�; 	
public
�;�; 
sis_usuario
�;�;  
datosUsuarioNombre
�;�; -
(
�;�;- .
string
�;�;. 4
nombre
�;�;5 ;
)
�;�;; <
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�;  
datosUsuarioNombre
�;�; 1
(
�;�;1 2
nombre
�;�;2 8
)
�;�;8 9
;
�;�;9 :
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; 9
+management_existeLoteAsignado_FacturaResult
�;�; ?
>
�;�;? @ 
ExisteLoteAsignado
�;�;A S
(
�;�;S T
int
�;�;T W
idFac
�;�;X ]
)
�;�;] ^
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�;  
ExisteLoteAsignado
�;�; 1
(
�;�;1 2
idFac
�;�;2 7
)
�;�;7 8
;
�;�;8 9
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; 
Ref_ips_cuentas
�;�; #
>
�;�;# $$
getprestadoresEspecial
�;�;% ;
(
�;�;; <
string
�;�;< B
nit
�;�;C F
,
�;�;F G
string
�;�;H N
	prestador
�;�;O X
)
�;�;X Y
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; $
getprestadoresEspecial
�;�; 5
(
�;�;5 6
nit
�;�;6 9
,
�;�;9 :
	prestador
�;�;; D
)
�;�;D E
;
�;�;E F
}
�;�; 	
public
�;�; 6
(management_prestadoresRegionalIdPrResult
�;�; 7
PrestadorRegional
�;�;8 I
(
�;�;I J
int
�;�;J M
idPrestador
�;�;N Y
)
�;�;Y Z
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; 
PrestadorRegional
�;�; 0
(
�;�;0 1
idPrestador
�;�;1 <
)
�;�;< =
;
�;�;= >
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; %
vw_sis_auditor_regional
�;�; +
>
�;�;+ ,
UsuariosxRegional
�;�;- >
(
�;�;> ?
int
�;�;? B

idRegional
�;�;C M
)
�;�;M N
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; 
UsuariosxRegional
�;�; 0
(
�;�;0 1

idRegional
�;�;1 ;
)
�;�;; <
;
�;�;< =
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; *
ref_cuentas_medicas_analista
�;�; 0
>
�;�;0 1,
getAnalistasAsignadosprestador
�;�;2 P
(
�;�;P Q
int
�;�;Q T
idPrestador
�;�;U `
)
�;�;` a
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; ,
getAnalistasAsignadosprestador
�;�; =
(
�;�;= >
idPrestador
�;�;> I
)
�;�;I J
;
�;�;J K
}
�;�; 	
public
�;�; 
int
�;�; -
ActualizarAsignacion_automatica
�;�; 2
(
�;�;2 3
int
�;�;3 6
idPrestador
�;�;7 B
)
�;�;B C
{
�;�; 	
return
�;�; 
DACActualiza
�;�; 
.
�;�;  -
ActualizarAsignacion_automatica
�;�;  ?
(
�;�;? @
idPrestador
�;�;@ K
)
�;�;K L
;
�;�;L M
}
�;�; 	
public
�;�; *
ref_cuentas_medicas_analista
�;�; +(
ListadoActualizarAnalistas
�;�;, F
(
�;�;F G
int
�;�;G J
idPrestador
�;�;K V
,
�;�;V W
int
�;�;X [

idAnalista
�;�;\ f
)
�;�;f g
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; (
ListadoActualizarAnalistas
�;�; 9
(
�;�;9 :
idPrestador
�;�;: E
,
�;�;E F

idAnalista
�;�;G Q
)
�;�;Q R
;
�;�;R S
}
�;�; 	
public
�;�; 
int
�;�; ,
ActualizarAsignacionAutomatica
�;�; 1
(
�;�;1 2*
ref_cuentas_medicas_analista
�;�;2 N
obj
�;�;O R
)
�;�;R S
{
�;�; 	
return
�;�; 
DACActualiza
�;�; 
.
�;�;  ,
ActualizarAsignacionAutomatica
�;�;  >
(
�;�;> ?
obj
�;�;? B
)
�;�;B C
;
�;�;C D
}
�;�; 	
public
�;�; 
int
�;�; :
,InsertarNuevosAnalistas_asignacionAutomatica
�;�; ?
(
�;�;? @
List
�;�;@ D
<
�;�;D E*
ref_cuentas_medicas_analista
�;�;E a
>
�;�;a b
obj
�;�;c f
)
�;�;f g
{
�;�; 	
return
�;�; 

DACInserta
�;�; 
.
�;�; :
,InsertarNuevosAnalistas_asignacionAutomatica
�;�; J
(
�;�;J K
obj
�;�;K N
)
�;�;N O
;
�;�;O P
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; 9
+management_facturacion_tableroControlResult
�;�; ?
>
�;�;? @)
ListadoMedicamentosFacturas
�;�;A \
(
�;�;\ ]
DateTime
�;�;] e
fechaInicio
�;�;f q
,
�;�;q r
DateTime
�;�;s {
fechaFin�;�;| �
,�;�;� �
string�;�;� �
identificacion�;�;� �
)�;�;� �
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; )
ListadoMedicamentosFacturas
�;�; :
(
�;�;: ;
fechaInicio
�;�;; F
,
�;�;F G
fechaFin
�;�;H P
,
�;�;P Q
identificacion
�;�;R `
)
�;�;` a
;
�;�;a b
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; <
.management_facturacion_consolidado_listaResult
�;�; B
>
�;�;B C9
+ListadoMedicamentosFacturasConsolidadoLista
�;�;D o
(
�;�;o p
DateTime
�;�;p x
fechaIni�;�;y �
,�;�;� �
DateTime�;�;� �
fechaFin�;�;� �
)�;�;� �
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; 9
+ListadoMedicamentosFacturasConsolidadoLista
�;�; J
(
�;�;J K
fechaIni
�;�;K S
,
�;�;S T
fechaFin
�;�;U ]
)
�;�;] ^
;
�;�;^ _
}
�;�; 	
public
�;�; >
0managemenet_prestadores_traerDatosFacturasResult
�;�; ?!
ListadoFacturasIdAf
�;�;@ S
(
�;�;S T
int
�;�;T W
id_af
�;�;X ]
)
�;�;] ^
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; !
ListadoFacturasIdAf
�;�; 2
(
�;�;2 3
id_af
�;�;3 8
)
�;�;8 9
;
�;�;9 :
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; )
ref_componente_hospitalario
�;�; /
>
�;�;/ 0)
GetComponentesHospitalarios
�;�;1 L
(
�;�;L M
)
�;�;M N
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; )
GetComponentesHospitalarios
�;�; :
(
�;�;: ;
)
�;�;; <
;
�;�;< =
}
�;�; 	
public
�;�; 
int
�;�; /
!EliminarCarguePrefacturasCompleto
�;�; 4
(
�;�;4 5
int
�;�;5 8
idCargue
�;�;9 A
)
�;�;A B
{
�;�; 	
return
�;�; 

DACElimina
�;�; 
.
�;�; /
!EliminarCarguePrefacturasCompleto
�;�; ?
(
�;�;? @
idCargue
�;�;@ H
)
�;�;H I
;
�;�;I J
}
�;�; 	
public
�;�; 
int
�;�; .
 GuardarLogEliminacionPrefacturas
�;�; 3
(
�;�;3 4-
log_prefacturas_eliminarCargues
�;�;4 S
obj
�;�;T W
)
�;�;W X
{
�;�; 	
return
�;�; 

DACInserta
�;�; 
.
�;�; .
 GuardarLogEliminacionPrefacturas
�;�; >
(
�;�;> ?
obj
�;�;? B
)
�;�;B C
;
�;�;C D
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; 8
*management_prefacturas_tableroCierreResult
�;�; >
>
�;�;> ?1
#TableroInformacionCierrePrefacturas
�;�;@ c
(
�;�;c d
DateTime
�;�;d l
?
�;�;l m
fechaInicio
�;�;n y
,
�;�;y z
DateTime�;�;{ �
?�;�;� �
fechaFin�;�;� �
)�;�;� �
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; 1
#TableroInformacionCierrePrefacturas
�;�; B
(
�;�;B C
fechaInicio
�;�;C N
,
�;�;N O
fechaFin
�;�;P X
)
�;�;X Y
;
�;�;Y Z
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; 8
*management_prefacturas_tableroAhorroResult
�;�; >
>
�;�;> ?1
#TableroInformacionAhorroPrefacturas
�;�;@ c
(
�;�;c d
DateTime
�;�;d l
?
�;�;l m
fechaInicio
�;�;n y
,
�;�;y z
DateTime�;�;{ �
?�;�;� �
fechaFin�;�;� �
)�;�;� �
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; 1
#TableroInformacionAhorroPrefacturas
�;�; B
(
�;�;B C
fechaInicio
�;�;C N
,
�;�;N O
fechaFin
�;�;P X
)
�;�;X Y
;
�;�;Y Z
}
�;�; 	
public
�;�; 
List
�;�; 
<
�;�; 
ref_analista_lote
�;�; %
>
�;�;% &(
TraerAnalistaLoteExistente
�;�;' A
(
�;�;A B
int
�;�;B E
idlote
�;�;F L
)
�;�;L M
{
�;�; 	
return
�;�; 
DACConsulta
�;�; 
.
�;�; (
TraerAnalistaLoteExistente
�;�; 9
(
�;�;9 :
idlote
�;�;: @
)
�;�;@ A
;
�;�;A B
}
�;�; 	
public
�;�; 
int
�;�; $
ActualizarLoteAnalista
�;�; )
(
�;�;) *
ref_analista_lote
�;�;* ;
obj
�;�;< ?
,
�;�;? @
ref
�;�;A D 
MessageResponseOBJ
�;�;E W
MsgRes
�;�;X ^
)
�;�;^ _
{
�;�; 	
return
�;�; 
DACActualiza
�;�; 
.
�;�;  $
ActualizarLoteAnalista
�;�;  6
(
�;�;6 7
obj
�;�;7 :
,
�;�;: ;
ref
�;�;< ?
MsgRes
�;�;@ F
)
�;�;F G
;
�;�;G H
}
�;�; 	
public
�;�; 
Int32
�;�; @
2InsertarInventarioFacturasContabilizadasCargueBase
�;�; G
(
�;�;G H;
-inventario_facturas_contabilizadas_carguebase
�;�;H u
obj
�;�;v y
,
�;�;y z
ref
�;�;{ ~!
MessageResponseOBJ�;�; �
MsgRes�;�;� �
)�;�;� �
{
�;�; 	
return
�<�< 

DACInserta
�<�< 
.
�<�< @
2InsertarInventarioFacturasContabilizadasCargueBase
�<�< P
(
�<�<P Q
obj
�<�<Q T
,
�<�<T U
ref
�<�<V Y
MsgRes
�<�<Z `
)
�<�<` a
;
�<�<a b
}
�<�< 	
public
�<�< 
void
�<�< @
2InsertarInventarioFacturasContabilizadasCargueDtll
�<�< F
(
�<�<F G
List
�<�<G K
<
�<�<K L;
-inventario_facturas_contabilizadas_carguedtll
�<�<L y
>
�<�<y z
dtll
�<�<{ 
,�<�< �
ref�<�<� �"
MessageResponseOBJ�<�<� �
MsgRes�<�<� �
)�<�<� �
{
�<�< 	

DACInserta
�<�< 
.
�<�< @
2InsertarInventarioFacturasContabilizadasCargueDtll
�<�< I
(
�<�<I J
dtll
�<�<J N
,
�<�<N O
ref
�<�<P S
MsgRes
�<�<T Z
)
�<�<Z [
;
�<�<[ \
}
�<�< 	
public
�<�< 
List
�<�< 
<
�<�< A
3Management_inventario_facturas_contabilizadasResult
�<�< G
>
�<�<G H2
$ConsultarInventarioFacturasPorEstado
�<�<I m
(
�<�<m n
int
�<�<n q
idEstado
�<�<r z
,
�<�<z {
DateTime�<�<| �
?�<�<� �
fechainicio�<�<� �
,�<�<� �
DateTime�<�<� �
?�<�<� �

fechafinal�<�<� �
,�<�<� �
int�<�<� �
?�<�<� �
regional�<�<� �
,�<�<� �
ref�<�<� �"
MessageResponseOBJ�<�<� �
MsgRes�<�<� �
)�<�<� �
{
�<�< 	
return
�<�< 
DACConsulta
�<�< 
.
�<�< 2
$ConsultarInventarioFacturasPorEstado
�<�< C
(
�<�<C D
idEstado
�<�<D L
,
�<�<L M
fechainicio
�<�<N Y
,
�<�<Y Z

fechafinal
�<�<[ e
,
�<�<e f
regional
�<�<g o
,
�<�<o p
ref
�<�<q t
MsgRes
�<�<u {
)
�<�<{ |
;
�<�<| }
}
�<�< 	
public
�<�< 
void
�<�< 9
+GuardarGestionInvetarioFacturaContabilizada
�<�< ?
(
�<�<? @8
*inventario_facturas_contabilizadas_gestion
�<�<@ j
obj
�<�<k n
,
�<�<n o
ref
�<�<p s!
MessageResponseOBJ�<�<t �
MsgRes�<�<� �
)�<�<� �
{
�<�< 	

DACInserta
�<�< 
.
�<�< 9
+GuardarGestionInvetarioFacturaContabilizada
�<�< B
(
�<�<B C
obj
�<�<C F
,
�<�<F G
ref
�<�<H K
MsgRes
�<�<L R
)
�<�<R S
;
�<�<S T
}
�<�< 	
public
�<�< 
List
�<�< 
<
�<�< M
?Management_inventario_facturas_contabilizadas_cordinacionResult
�<�< S
>
�<�<S T4
&ConsultarInventarioFacturasCordinacion
�<�<U {
(
�<�<{ |
int
�<�<| 
mes�<�<� �
,�<�<� �
int�<�<� �
año�<�<� �
,�<�<� �
int�<�<� �
regional�<�<� �
,�<�<� �
ref�<�<� �"
MessageResponseOBJ�<�<� �
MsgRes�<�<� �
)�<�<� �
{
�<�< 	
return
�<�< 
DACConsulta
�<�< 
.
�<�< 4
&ConsultarInventarioFacturasCordinacion
�<�< E
(
�<�<E F
mes
�<�<F I
,
�<�<I J
año
�<�<K N
,
�<�<N O
regional
�<�<P X
,
�<�<X Y
ref
�<�<Z ]
MsgRes
�<�<^ d
)
�<�<d e
;
�<�<e f
}
�<�< 	
public
�<�< 
List
�<�< 
<
�<�< M
?Management_inventario_facturas_contabilizadas_consolidadoResult
�<�< S
>
�<�<S T4
&ConsultarInventarioFacturasConsolidado
�<�<U {
(
�<�<{ |
)
�<�<| }
{
�<�< 	
return
�<�< 
DACConsulta
�<�< 
.
�<�< 4
&ConsultarInventarioFacturasConsolidado
�<�< E
(
�<�<E F
)
�<�<F G
;
�<�<G H
}
�<�< 	
public
�<�< 8
*inventario_facturas_contabilizadas_gestion
�<�< 9>
0consultarGestionFacturaContabilizadaporIdDetalle
�<�<: j
(
�<�<j k
int
�<�<k n
	idDetalle
�<�<o x
)
�<�<x y
{
�<�< 	
return
�<�< 
DACConsulta
�<�< 
.
�<�< >
0consultarGestionFacturaContabilizadaporIdDetalle
�<�< O
(
�<�<O P
	idDetalle
�<�<P Y
)
�<�<Y Z
;
�<�<Z [
}
�<�< 	
public
�<�< 8
*inventario_facturas_contabilizadas_gestion
�<�< 9>
0consultarGestionFacturaContabilizadaporIdGestion
�<�<: j
(
�<�<j k
int
�<�<k n
	idGestion
�<�<o x
)
�<�<x y
{
�<�< 	
return
�<�< 
DACConsulta
�<�< 
.
�<�< >
0consultarGestionFacturaContabilizadaporIdGestion
�<�< O
(
�<�<O P
	idGestion
�<�<P Y
)
�<�<Y Z
;
�<�<Z [
}
�<�< 	
public
�<�< 
void
�<�< O
AinsertarDatosArchivoCargadoInventarioFacContabilizadasConsolidado
�<�< U
(
�<�<U VC
4inventario_facturas_contabilizadas_gestor_documental�<�<V �
doc�<�<� �
,�<�<� �
ref�<�<� �"
MessageResponseOBJ�<�<� �
MsgRes�<�<� �
)�<�<� �
{
�<�< 	

DACInserta
�<�< 
.
�<�< O
AinsertarDatosArchivoCargadoInventarioFacContabilizadasConsolidado
�<�< X
(
�<�<X Y
doc
�<�<Y \
,
�<�<\ ]
ref
�<�<^ a
MsgRes
�<�<b h
)
�<�<h i
;
�<�<i j
}
�<�< 	
public
�<�< B
4inventario_facturas_contabilizadas_gestor_documental
�<�< C2
$ConsultarRegistroArchivoCargadoPorId
�<�<D h
(
�<�<h i
int
�<�<i l
	idArchivo
�<�<m v
,
�<�<v w
ref
�<�<x {!
MessageResponseOBJ�<�<| �
MsgRes�<�<� �
)�<�<� �
{
�<�< 	
return
�<�< 
DACConsulta
�<�< 
.
�<�< 2
$ConsultarRegistroArchivoCargadoPorId
�<�< C
(
�<�<C D
	idArchivo
�<�<D M
,
�<�<M N
ref
�<�<O R
MsgRes
�<�<S Y
)
�<�<Y Z
;
�<�<Z [
}
�<�< 	
public
�<�< 
List
�<�< 
<
�<�< B
4inventario_facturas_contabilizadas_gestor_documental
�<�< H
>
�<�<H I7
)ConsultarRegistroArchivoCargadoPorIdLista
�<�<J s
(
�<�<s t
int
�<�<t w
?
�<�<w x
mes
�<�<y |
,
�<�<| }
int�<�<~ �
?�<�<� �
año�<�<� �
,�<�<� �
int�<�<� �
?�<�<� �
regional�<�<� �
,�<�<� �
ref�<�<� �"
MessageResponseOBJ�<�<� �
MsgRes�<�<� �
)�<�<� �
{
�<�< 	
return
�<�< 
DACConsulta
�<�< 
.
�<�< 7
)ConsultarRegistroArchivoCargadoPorIdLista
�<�< H
(
�<�<H I
mes
�<�<I L
,
�<�<L M
año
�<�<N Q
,
�<�<Q R
regional
�<�<S [
,
�<�<[ \
ref
�<�<] `
MsgRes
�<�<a g
)
�<�<g h
;
�<�<h i
}
�<�< 	
public
�<�< ;
-inventario_facturas_contabilizadas_carguebase
�<�< </
!ConsultarExistenciaPeriodoCargado
�<�<= ^
(
�<�<^ _
int
�<�<_ b
mes
�<�<c f
,
�<�<f g
int
�<�<h k
año
�<�<l o
,
�<�<o p
int
�<�<q t
regional
�<�<u }
)
�<�<} ~
{
�<�< 	
return
�<�< 
DACConsulta
�<�< 
.
�<�< /
!ConsultarExistenciaPeriodoCargado
�<�< @
(
�<�<@ A
mes
�<�<A D
,
�<�<D E
año
�<�<F I
,
�<�<I J
regional
�<�<K S
)
�<�<S T
;
�<�<T U
}
�<�< 	
public
�<�< 
List
�<�< 
<
�<�< I
;management_inventario_facturas_contabilizadas_reporteResult
�<�< O
>
�<�<O P-
ReporteInventarioContabilizadas
�<�<Q p
(
�<�<p q
int
�<�<q t
estado
�<�<u {
)
�<�<{ |
{
�<�< 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= -
ReporteInventarioContabilizadas
�=�= >
(
�=�=> ?
estado
�=�=? E
)
�=�=E F
;
�=�=F G
}
�=�= 	
public
�=�= 
int
�=�= &
insercionMasivaAltoCosto
�=�= +
(
�=�=+ ,-
inventario_altoCosto_carguebase
�=�=, K
obj
�=�=L O
,
�=�=O P
List
�=�=Q U
<
�=�=U V*
inventario_altoCosto_detalle
�=�=V r
>
�=�=r s
dtl
�=�=t w
,
�=�=w x
ref
�=�=y |!
MessageResponseOBJ�=�=} �
MsgRes�=�=� �
)�=�=� �
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= &
insercionMasivaAltoCosto
�=�= 6
(
�=�=6 7
obj
�=�=7 :
,
�=�=: ;
dtl
�=�=< ?
,
�=�=? @
ref
�=�=A D
MsgRes
�=�=E K
)
�=�=K L
;
�=�=L M
}
�=�= 	
public
�=�= 
List
�=�= 
<
�=�= ;
-management_inventario_altoCosto_tableroResult
�=�= A
>
�=�=A B(
ListadoInventarioAltoCosto
�=�=C ]
(
�=�=] ^
)
�=�=^ _
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= (
ListadoInventarioAltoCosto
�=�= 9
(
�=�=9 :
)
�=�=: ;
;
�=�=; <
}
�=�= 	
public
�=�= 
int
�=�= (
insercionGestionInventario
�=�= -
(
�=�=- .,
inventario_altoCosto_gestiones
�=�=. L
obj
�=�=M P
,
�=�=P Q
ref
�=�=R U 
MessageResponseOBJ
�=�=V h
MsgRes
�=�=i o
)
�=�=o p
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= (
insercionGestionInventario
�=�= 8
(
�=�=8 9
obj
�=�=9 <
,
�=�=< =
ref
�=�=> A
MsgRes
�=�=B H
)
�=�=H I
;
�=�=I J
}
�=�= 	
public
�=�= 
List
�=�= 
<
�=�= 0
"ref_inventario_altoCostoCancer_atc
�=�= 6
>
�=�=6 7"
listadoInventarioATC
�=�=8 L
(
�=�=L M
)
�=�=M N
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= "
listadoInventarioATC
�=�= 3
(
�=�=3 4
)
�=�=4 5
;
�=�=5 6
}
�=�= 	
public
�=�= 
List
�=�= 
<
�=�= ,
inventario_altoCosto_gestiones
�=�= 2
>
�=�=2 3*
listaInvAltoCostoGestionadas
�=�=4 P
(
�=�=P Q
int
�=�=Q T
?
�=�=T U
	idDetalle
�=�=V _
)
�=�=_ `
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= *
listaInvAltoCostoGestionadas
�=�= ;
(
�=�=; <
	idDetalle
�=�=< E
)
�=�=E F
;
�=�=F G
}
�=�= 	
public
�=�= ,
inventario_altoCosto_gestiones
�=�= -'
DatoInvAltoCostoGestionID
�=�=. G
(
�=�=G H
int
�=�=H K
?
�=�=K L
	idGestion
�=�=M V
)
�=�=V W
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= '
DatoInvAltoCostoGestionID
�=�= 8
(
�=�=8 9
	idGestion
�=�=9 B
)
�=�=B C
;
�=�=C D
}
�=�= 	
public
�=�= ,
inventario_altoCosto_gestiones
�=�= --
DatoUltimoInvAltoCostoGestionID
�=�=. M
(
�=�=M N
int
�=�=N Q
?
�=�=Q R
	idDetalle
�=�=S \
)
�=�=\ ]
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= -
DatoUltimoInvAltoCostoGestionID
�=�= >
(
�=�=> ?
	idDetalle
�=�=? H
)
�=�=H I
;
�=�=I J
}
�=�= 	
public
�=�= 
Int32
�=�= .
 InsertarArchivoisAltoCostoCancer
�=�= 5
(
�=�=5 6
List
�=�=6 :
<
�=�=: ;+
inventario_altoCosto_archivos
�=�=; X
>
�=�=X Y
archivos
�=�=Z b
,
�=�=b c
ref
�=�=d g 
MessageResponseOBJ
�=�=h z
MsgRes�=�={ �
)�=�=� �
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= .
 InsertarArchivoisAltoCostoCancer
�=�= >
(
�=�=> ?
archivos
�=�=? G
,
�=�=G H
ref
�=�=I L
MsgRes
�=�=M S
)
�=�=S T
;
�=�=T U
}
�=�= 	
public
�=�= 
List
�=�= 
<
�=�= ?
1management_inventario_altoCosto_verArchivosResult
�=�= E
>
�=�=E F(
ListadoArchivosGestionados
�=�=G a
(
�=�=a b
int
�=�=b e
?
�=�=e f
	idGestion
�=�=g p
)
�=�=p q
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= (
ListadoArchivosGestionados
�=�= 9
(
�=�=9 :
	idGestion
�=�=: C
)
�=�=C D
;
�=�=D E
}
�=�= 	
public
�=�= +
inventario_altoCosto_archivos
�=�= ,,
traerArchivoAltoCostoIdArchivo
�=�=- K
(
�=�=K L
int
�=�=L O
?
�=�=O P
	idArchivo
�=�=Q Z
)
�=�=Z [
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= ,
traerArchivoAltoCostoIdArchivo
�=�= =
(
�=�== >
	idArchivo
�=�=> G
)
�=�=G H
;
�=�=H I
}
�=�= 	
public
�=�= 
int
�=�= 5
'eliminarArchivoAltoCostoCanceridArchivo
�=�= :
(
�=�=: ;
int
�=�=; >
	idArchivo
�=�=? H
)
�=�=H I
{
�=�= 	
return
�=�= 

DACElimina
�=�= 
.
�=�= 5
'eliminarArchivoAltoCostoCanceridArchivo
�=�= E
(
�=�=E F
	idArchivo
�=�=F O
)
�=�=O P
;
�=�=P Q
}
�=�= 	
public
�=�= 
int
�=�= 4
&InsertarLogEliminacionArchivoAltoCosto
�=�= 9
(
�=�=9 ::
,log_inventario_altoCosto_eliminacionArchivos
�=�=: f
obj
�=�=g j
)
�=�=j k
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= 4
&InsertarLogEliminacionArchivoAltoCosto
�=�= D
(
�=�=D E
obj
�=�=E H
)
�=�=H I
;
�=�=I J
}
�=�= 	
public
�=�= 
List
�=�= 
<
�=�= D
6management_inventario_altoCosto_tableroGestionesResult
�=�= J
>
�=�=J K%
ListaAltoCostoGestiones
�=�=L c
(
�=�=c d
int
�=�=d g
?
�=�=g h
	idDetalle
�=�=i r
)
�=�=r s
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= %
ListaAltoCostoGestiones
�=�= 6
(
�=�=6 7
	idDetalle
�=�=7 @
)
�=�=@ A
;
�=�=A B
}
�=�= 	
public
�=�= 
List
�=�= 
<
�=�= *
ref_cargue_cuentas_altoCosto
�=�= 0
>
�=�=0 1%
listadoCargueGsdRastreo
�=�=2 I
(
�=�=I J
)
�=�=J K
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= %
listadoCargueGsdRastreo
�=�= 6
(
�=�=6 7
)
�=�=7 8
;
�=�=8 9
}
�=�= 	
public
�=�= 
List
�=�= 
<
�=�= 2
$ref_cargue_cuentas_altoCosto_estados
�=�= 8
>
�=�=8 9+
listadoEstadosCuentaAltoCosto
�=�=: W
(
�=�=W X
)
�=�=X Y
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= +
listadoEstadosCuentaAltoCosto
�=�= <
(
�=�=< =
)
�=�== >
;
�=�=> ?
}
�=�= 	
public
�=�= 
int
�=�= +
eliminarDatosCuentasAltoCosto
�=�= 0
(
�=�=0 1
int
�=�=1 4
idCargue
�=�=5 =
,
�=�== >
int
�=�=? B
?
�=�=B C
tipo
�=�=D H
)
�=�=H I
{
�=�= 	
return
�=�= 

DACElimina
�=�= 
.
�=�= +
eliminarDatosCuentasAltoCosto
�=�= ;
(
�=�=; <
idCargue
�=�=< D
,
�=�=D E
tipo
�=�=F J
)
�=�=J K
;
�=�=K L
}
�=�= 	
public
�=�= 
int
�=�= &
cargue_cuentas_altoCosto
�=�= +
(
�=�=+ ,&
cargue_cuentas_altoCosto
�=�=, D
obj
�=�=E H
,
�=�=H I
ref
�=�=J M 
MessageResponseOBJ
�=�=N `
MsgRes
�=�=a g
)
�=�=g h
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= &
cargue_cuentas_altoCosto
�=�= 6
(
�=�=6 7
obj
�=�=7 :
,
�=�=: ;
ref
�=�=< ?
MsgRes
�=�=@ F
)
�=�=F G
;
�=�=G H
}
�=�= 	
public
�=�= 
int
�=�= 1
#InsertarCuentasAltoCostoConfirmnada
�=�= 6
(
�=�=6 7
List
�=�=7 ;
<
�=�=; <1
#cargue_cuentas_altoCosto_confirmada
�=�=< _
>
�=�=_ `
List
�=�=a e
,
�=�=e f
ref
�=�=g j 
MessageResponseOBJ
�=�=k }
MsgRes�=�=~ �
)�=�=� �
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= 1
#InsertarCuentasAltoCostoConfirmnada
�=�= A
(
�=�=A B
List
�=�=B F
,
�=�=F G
ref
�=�=H K
MsgRes
�=�=L R
)
�=�=R S
;
�=�=S T
}
�=�= 	
public
�=�= 
int
�=�= ,
InsertarCuentasAltoCostoCancer
�=�= 1
(
�=�=1 2
List
�=�=2 6
<
�=�=6 7-
cargue_cuentas_altoCosto_cancer
�=�=7 V
>
�=�=V W
List
�=�=X \
,
�=�=\ ]
ref
�=�=^ a 
MessageResponseOBJ
�=�=b t
MsgRes
�=�=u {
)
�=�={ |
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= ,
InsertarCuentasAltoCostoCancer
�=�= <
(
�=�=< =
List
�=�== A
,
�=�=A B
ref
�=�=C F
MsgRes
�=�=G M
)
�=�=M N
;
�=�=N O
}
�=�= 	
public
�=�= 
int
�=�= ,
GuardarGestionCuentasAltoCosto
�=�= 1
(
�=�=1 20
"cargue_cuentas_altoCosto_gestiones
�=�=2 T
obj
�=�=U X
,
�=�=X Y
ref
�=�=Z ] 
MessageResponseOBJ
�=�=^ p
MsgRes
�=�=q w
)
�=�=w x
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= ,
GuardarGestionCuentasAltoCosto
�=�= <
(
�=�=< =
obj
�=�== @
,
�=�=@ A
ref
�=�=B E
MsgRes
�=�=F L
)
�=�=L M
;
�=�=M N
}
�=�= 	
public
�=�= 
List
�=�= 
<
�=�= 9
+management_cuentasAltoCosto_gestionesResult
�=�= ?
>
�=�=? @'
listadoGestionesAltoCosto
�=�=A Z
(
�=�=Z [
int
�=�=[ ^
?
�=�=^ _

idRegistro
�=�=` j
,
�=�=j k
int
�=�=l o
?
�=�=o p
tipo
�=�=q u
)
�=�=u v
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= '
listadoGestionesAltoCosto
�=�= 8
(
�=�=8 9

idRegistro
�=�=9 C
,
�=�=C D
tipo
�=�=E I
)
�=�=I J
;
�=�=J K
}
�=�= 	
public
�=�= 
int
�=�= /
!InsertarCuentasAltoCostoHemofilia
�=�= 4
(
�=�=4 5
List
�=�=5 9
<
�=�=9 :0
"cargue_cuentas_altoCosto_hemofilia
�=�=: \
>
�=�=\ ]
List
�=�=^ b
,
�=�=b c
ref
�=�=d g 
MessageResponseOBJ
�=�=h z
MsgRes�=�={ �
)�=�=� �
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= /
!InsertarCuentasAltoCostoHemofilia
�=�= ?
(
�=�=? @
List
�=�=@ D
,
�=�=D E
ref
�=�=F I
MsgRes
�=�=J P
)
�=�=P Q
;
�=�=Q R
}
�=�= 	
public
�=�= 
int
�=�= .
 InsertarCuentasAltoCostoArtritis
�=�= 3
(
�=�=3 4
List
�=�=4 8
<
�=�=8 9/
!cargue_cuentas_altoCosto_artritis
�=�=9 Z
>
�=�=Z [
List
�=�=\ `
,
�=�=` a
ref
�=�=b e 
MessageResponseOBJ
�=�=f x
MsgRes
�=�=y 
)�=�= �
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= .
 InsertarCuentasAltoCostoArtritis
�=�= >
(
�=�=> ?
List
�=�=? C
,
�=�=C D
ref
�=�=E H
MsgRes
�=�=I O
)
�=�=O P
;
�=�=P Q
}
�=�= 	
public
�=�= 
int
�=�= )
InsertarCuentasAltoCostoVIH
�=�= .
(
�=�=. /
List
�=�=/ 3
<
�=�=3 4*
cargue_cuentas_altoCosto_vih
�=�=4 P
>
�=�=P Q
List
�=�=R V
,
�=�=V W
ref
�=�=X [ 
MessageResponseOBJ
�=�=\ n
MsgRes
�=�=o u
)
�=�=u v
{
�=�= 	
return
�=�= 

DACInserta
�=�= 
.
�=�= )
InsertarCuentasAltoCostoVIH
�=�= 9
(
�=�=9 :
List
�=�=: >
,
�=�=> ?
ref
�=�=@ C
MsgRes
�=�=D J
)
�=�=J K
;
�=�=K L
}
�=�= 	
public
�=�= 
List
�=�= 
<
�=�= 8
*management_cuentasAltoCosto_rastreosResult
�=�= >
>
�=�=> ?&
ListadoDatosRastreoTotal
�=�=@ X
(
�=�=X Y
int
�=�=Y \
?
�=�=\ ]
tipo
�=�=^ b
)
�=�=b c
{
�=�= 	
return
�=�= 
DACConsulta
�=�= 
.
�=�= &
ListadoDatosRastreoTotal
�=�= 7
(
�=�=7 8
tipo
�=�=8 <
)
�=�=< =
;
�=�== >
}
�=�= 	
public
�>�> 
List
�>�> 
<
�>�> C
5management_cuentasAltoCosto_rastreosConfirmadosResult
�>�> I
>
�>�>I J5
'ListadoDatosCuentasAltoCostoConfirmados
�>�>K r
(
�>�>r s
int
�>�>s v
?
�>�>v w
tipo
�>�>x |
)
�>�>| }
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> 5
'ListadoDatosCuentasAltoCostoConfirmados
�>�> F
(
�>�>F G
tipo
�>�>G K
)
�>�>K L
;
�>�>L M
}
�>�> 	
public
�>�> 
List
�>�> 
<
�>�> ;
-management_cuentasAltoCosto_repositorioResult
�>�> A
>
�>�>A B)
CuentasAltoCostoRepositorio
�>�>C ^
(
�>�>^ _
int
�>�>_ b
?
�>�>b c

idRegistro
�>�>d n
,
�>�>n o
int
�>�>p s
?
�>�>s t
tipo
�>�>u y
)
�>�>y z
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> )
CuentasAltoCostoRepositorio
�>�> :
(
�>�>: ;

idRegistro
�>�>; E
,
�>�>E F
tipo
�>�>G K
)
�>�>K L
;
�>�>L M
}
�>�> 	
public
�>�> 
List
�>�> 
<
�>�> /
!ref_cuentas_altocosto_tipoSoporte
�>�> 5
>
�>�>5 6
tipoSoporteCAC
�>�>7 E
(
�>�>E F
)
�>�>F G
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> 
tipoSoporteCAC
�>�> -
(
�>�>- .
)
�>�>. /
;
�>�>/ 0
}
�>�> 	
public
�>�> /
!cargue_cuentas_altoCosto_archivos
�>�> 0%
TraerArchivoRepositorio
�>�>1 H
(
�>�>H I
int
�>�>I L
?
�>�>L M
	idArchivo
�>�>N W
)
�>�>W X
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> %
TraerArchivoRepositorio
�>�> 6
(
�>�>6 7
	idArchivo
�>�>7 @
)
�>�>@ A
;
�>�>A B
}
�>�> 	
public
�>�> 
Int32
�>�> +
InsertarArchivoReposAltoCosto
�>�> 2
(
�>�>2 3/
!cargue_cuentas_altoCosto_archivos
�>�>3 T
OBJ
�>�>U X
,
�>�>X Y
ref
�>�>Z ] 
MessageResponseOBJ
�>�>^ p
MsgRes
�>�>q w
)
�>�>w x
{
�>�> 	
return
�>�> 

DACInserta
�>�> 
.
�>�> +
InsertarArchivoReposAltoCosto
�>�> ;
(
�>�>; <
OBJ
�>�>< ?
,
�>�>? @
ref
�>�>A D
MsgRes
�>�>E K
)
�>�>K L
;
�>�>L M
}
�>�> 	
public
�>�> 
int
�>�> 1
#eliminarArchivoRepositorioAltoCosto
�>�> 6
(
�>�>6 7
int
�>�>7 :
id
�>�>; =
)
�>�>= >
{
�>�> 	
return
�>�> 

DACElimina
�>�> 
.
�>�> 1
#eliminarArchivoRepositorioAltoCosto
�>�> A
(
�>�>A B
id
�>�>B D
)
�>�>D E
;
�>�>E F
}
�>�> 	
public
�>�> 
Int32
�>�> &
LogArchivoReposAltoCosto
�>�> -
(
�>�>- .3
%log_cargue_cuentas_altoCosto_archivos
�>�>. S
OBJ
�>�>T W
,
�>�>W X
ref
�>�>Y \ 
MessageResponseOBJ
�>�>] o
MsgRes
�>�>p v
)
�>�>v w
{
�>�> 	
return
�>�> 

DACInserta
�>�> 
.
�>�> &
LogArchivoReposAltoCosto
�>�> 6
(
�>�>6 7
OBJ
�>�>7 :
,
�>�>: ;
ref
�>�>< ?
MsgRes
�>�>@ F
)
�>�>F G
;
�>�>G H
}
�>�> 	
public
�>�> 
List
�>�> 
<
�>�> N
@management_cuentasAltoCosto_rastreosConfirmados_conArchivoResult
�>�> T
>
�>�>T UA
2ListadoDatosCuentasAltoCostoConfirmadosConArchivos�>�>V �
(�>�>� �
)�>�>� �
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> @
2ListadoDatosCuentasAltoCostoConfirmadosConArchivos
�>�> Q
(
�>�>Q R
)
�>�>R S
;
�>�>S T
}
�>�> 	
public
�>�> 
List
�>�> 
<
�>�> V
Hmanagement_cuentasAltoCosto_rastreosConfirmados_conArchivoCompletaResult
�>�> \
>
�>�>\ ]J
;ListadoDatosCuentasAltoCostoConfirmadosConArchivosDetallada�>�>^ �
(�>�>� �
)�>�>� �
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> I
;ListadoDatosCuentasAltoCostoConfirmadosConArchivosDetallada
�>�> Z
(
�>�>Z [
)
�>�>[ \
;
�>�>\ ]
}
�>�> 	
public
�>�> 
List
�>�> 
<
�>�> Q
Cmanagement_cuentasAltoCosto_rastreosConfirmados_observacionesResult
�>�> W
>
�>�>W X>
/ListadoObservacionesCuentasAltoCostoGestionadas�>�>Y �
(�>�>� �
int�>�>� �
?�>�>� �

idRegistro�>�>� �
,�>�>� �
int�>�>� �
?�>�>� �
tipo�>�>� �
)�>�>� �
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> =
/ListadoObservacionesCuentasAltoCostoGestionadas
�>�> N
(
�>�>N O

idRegistro
�>�>O Y
,
�>�>Y Z
tipo
�>�>[ _
)
�>�>_ `
;
�>�>` a
}
�>�> 	
public
�>�> 
Int32
�>�> 2
$GuardarObservacionesCuentasAltoCosto
�>�> 9
(
�>�>9 :4
&cargue_cuentas_altoCosto_observaciones
�>�>: `
OBJ
�>�>a d
,
�>�>d e
ref
�>�>f i 
MessageResponseOBJ
�>�>j |
MsgRes�>�>} �
)�>�>� �
{
�>�> 	
return
�>�> 

DACInserta
�>�> 
.
�>�> 2
$GuardarObservacionesCuentasAltoCosto
�>�> B
(
�>�>B C
OBJ
�>�>C F
,
�>�>F G
ref
�>�>H K
MsgRes
�>�>L R
)
�>�>R S
;
�>�>S T
}
�>�> 	
public
�>�> 
int
�>�> *
eliminarObservacionAltoCosto
�>�> /
(
�>�>/ 0
int
�>�>0 3
id
�>�>4 6
)
�>�>6 7
{
�>�> 	
return
�>�> 

DACElimina
�>�> 
.
�>�> *
eliminarObservacionAltoCosto
�>�> :
(
�>�>: ;
id
�>�>; =
)
�>�>= >
;
�>�>> ?
}
�>�> 	
public
�>�> 
List
�>�> 
<
�>�> C
5management_cuentasAltoCosto_consolidadoArchivosResult
�>�> I
>
�>�>I J*
ListaArchivosPorDocumentoCAC
�>�>K g
(
�>�>g h
string
�>�>h n
	documento
�>�>o x
,
�>�>x y
int
�>�>z }
?
�>�>} ~
tipo�>�> �
)�>�>� �
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> *
ListaArchivosPorDocumentoCAC
�>�> ;
(
�>�>; <
	documento
�>�>< E
,
�>�>E F
tipo
�>�>G K
)
�>�>K L
;
�>�>L M
}
�>�> 	
public
�>�> 
List
�>�> 
<
�>�> B
4management_cuentasAltoCosto_documentosArchivosResult
�>�> H
>
�>�>H I#
DocumentosConArchivos
�>�>J _
(
�>�>_ `
int
�>�>` c
?
�>�>c d
tipo
�>�>e i
)
�>�>i j
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> #
DocumentosConArchivos
�>�> 4
(
�>�>4 5
tipo
�>�>5 9
)
�>�>9 :
;
�>�>: ;
}
�>�> 	
public
�>�> 
int
�>�> #
CargueMasivoContratos
�>�> (
(
�>�>( )
contratos_cargue
�>�>) 9
obj
�>�>: =
,
�>�>= >
List
�>�>? C
<
�>�>C D
contratos_detalle
�>�>D U
>
�>�>U V
detalle
�>�>W ^
,
�>�>^ _
ref
�>�>` c 
MessageResponseOBJ
�>�>d v
MsgRes
�>�>w }
)
�>�>} ~
{
�>�> 	
return
�>�> 

DACInserta
�>�> 
.
�>�> #
CargueMasivoContratos
�>�> 3
(
�>�>3 4
obj
�>�>4 7
,
�>�>7 8
detalle
�>�>9 @
,
�>�>@ A
ref
�>�>B E
MsgRes
�>�>F L
)
�>�>L M
;
�>�>M N
}
�>�> 	
public
�>�> 
List
�>�> 
<
�>�> 0
"management_contratos_listadoResult
�>�> 6
>
�>�>6 7
listadoContratos
�>�>8 H
(
�>�>H I
)
�>�>I J
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> 
listadoContratos
�>�> /
(
�>�>/ 0
)
�>�>0 1
;
�>�>1 2
}
�>�> 	
public
�>�> 
contratos_detalle
�>�>  $
MostrarDatosContratoId
�>�>! 7
(
�>�>7 8
int
�>�>8 ;
?
�>�>; <

idContrato
�>�>= G
)
�>�>G H
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> $
MostrarDatosContratoId
�>�> 5
(
�>�>5 6

idContrato
�>�>6 @
)
�>�>@ A
;
�>�>A B
}
�>�> 	
public
�>�> 
int
�>�>  
ActualizarContrato
�>�> %
(
�>�>% &
contratos_detalle
�>�>& 7
obj
�>�>8 ;
,
�>�>; <
ref
�>�>= @ 
MessageResponseOBJ
�>�>A S
MsgRes
�>�>T Z
)
�>�>Z [
{
�>�> 	
return
�>�> 
DACActualiza
�>�> 
.
�>�>   
ActualizarContrato
�>�>  2
(
�>�>2 3
obj
�>�>3 6
,
�>�>6 7
ref
�>�>8 ;
MsgRes
�>�>< B
)
�>�>B C
;
�>�>C D
}
�>�> 	
public
�>�> 
contratos_detalle
�>�>  %
MostrarDetallePContrato
�>�>! 8
(
�>�>8 9
string
�>�>9 ?
sap
�>�>@ C
)
�>�>C D
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> %
MostrarDetallePContrato
�>�> 6
(
�>�>6 7
sap
�>�>7 :
)
�>�>: ;
;
�>�>; <
}
�>�> 	
public
�>�> 
int
�>�> ,
InsertarContratoNuevoPrestador
�>�> 1
(
�>�>1 2
contratos_detalle
�>�>2 C
obj
�>�>D G
)
�>�>G H
{
�>�> 	
return
�>�> 

DACInserta
�>�> 
.
�>�> ,
InsertarContratoNuevoPrestador
�>�> <
(
�>�>< =
obj
�>�>= @
)
�>�>@ A
;
�>�>A B
}
�>�> 	
public
�>�> 
List
�>�> 
<
�>�> 0
"management_usuarios_regionalResult
�>�> 6
>
�>�>6 7$
ListadoRegionalUsuario
�>�>8 N
(
�>�>N O
)
�>�>O P
{
�>�> 	
return
�>�> 
DACComonClass
�>�>  
.
�>�>  !$
ListadoRegionalUsuario
�>�>! 7
(
�>�>7 8
)
�>�>8 9
;
�>�>9 :
}
�>�> 	
public
�>�> '
rips_depurados_carguebase
�>�> (.
 ConsultarCargueBaseRipsDepurados
�>�>) I
(
�>�>I J
string
�>�>J P
tipoRips
�>�>Q Y
,
�>�>Y Z
int
�>�>[ ^
mes
�>�>_ b
,
�>�>b c
int
�>�>d g
año
�>�>h k
)
�>�>k l
{
�>�> 	
return
�>�> 
DACConsulta
�>�> 
.
�>�> .
 ConsultarCargueBaseRipsDepurados
�>�> ?
(
�>�>? @
tipoRips
�>�>@ H
,
�>�>H I
mes
�>�>J M
,
�>�>M N
año
�>�>O R
)
�>�>R S
;
�>�>S T
}
�>�> 	
public
�?�? 
int
�?�? ,
GuardarCargueBaseRipsDepurados
�?�? 1
(
�?�?1 2'
rips_depurados_carguebase
�?�?2 K
cb
�?�?L N
,
�?�?N O
ref
�?�?P S 
MessageResponseOBJ
�?�?T f
MsgRes
�?�?g m
)
�?�?m n
{
�?�? 	
return
�?�? 

DACInserta
�?�? 
.
�?�? ,
GuardarCargueBaseRipsDepurados
�?�? <
(
�?�?< =
cb
�?�?= ?
,
�?�?? @
ref
�?�?A D
MsgRes
�?�?E K
)
�?�?K L
;
�?�?L M
}
�?�? 	
public
�?�? 
void
�?�? 1
#InsertarCargueMasivoRipsDepuradosAC
�?�? 7
(
�?�?7 8
List
�?�?8 <
<
�?�?< =*
rips_depurados_ac_carguedtll
�?�?= Y
>
�?�?Y Z
cdtll
�?�?[ `
,
�?�?` a
ref
�?�?b e 
MessageResponseOBJ
�?�?f x
MsgRes
�?�?y 
)�?�? �
{
�?�? 	

DACInserta
�?�? 
.
�?�? 1
#InsertarCargueMasivoRipsDepuradosAC
�?�? :
(
�?�?: ;
cdtll
�?�?; @
,
�?�?@ A
ref
�?�?B E
MsgRes
�?�?F L
)
�?�?L M
;
�?�?M N
}
�?�? 	
public
�?�? 
void
�?�? 1
#InsertarCargueMasivoRipsDepuradosAP
�?�? 7
(
�?�?7 8
List
�?�?8 <
<
�?�?< =*
rips_depurados_ap_carguedtll
�?�?= Y
>
�?�?Y Z
cdtll
�?�?[ `
,
�?�?` a
ref
�?�?b e 
MessageResponseOBJ
�?�?f x
MsgRes
�?�?y 
)�?�? �
{
�?�? 	

DACInserta
�?�? 
.
�?�? 1
#InsertarCargueMasivoRipsDepuradosAP
�?�? :
(
�?�?: ;
cdtll
�?�?; @
,
�?�?@ A
ref
�?�?B E
MsgRes
�?�?F L
)
�?�?L M
;
�?�?M N
}
�?�? 	
public
�?�? 
void
�?�? 1
#InsertarCargueMasivoRipsDepuradosAU
�?�? 7
(
�?�?7 8
List
�?�?8 <
<
�?�?< =*
rips_depurados_au_carguedtll
�?�?= Y
>
�?�?Y Z
cdtll
�?�?[ `
,
�?�?` a
ref
�?�?b e 
MessageResponseOBJ
�?�?f x
MsgRes
�?�?y 
)�?�? �
{
�?�? 	

DACInserta
�?�? 
.
�?�? 1
#InsertarCargueMasivoRipsDepuradosAU
�?�? :
(
�?�?: ;
cdtll
�?�?; @
,
�?�?@ A
ref
�?�?B E
MsgRes
�?�?F L
)
�?�?L M
;
�?�?M N
}
�?�? 	
public
�?�? 
void
�?�? 1
#InsertarCargueMasivoRipsDepuradosAM
�?�? 7
(
�?�?7 8
List
�?�?8 <
<
�?�?< =*
rips_depurados_am_carguedtll
�?�?= Y
>
�?�?Y Z
cdtll
�?�?[ `
,
�?�?` a
ref
�?�?b e 
MessageResponseOBJ
�?�?f x
MsgRes
�?�?y 
)�?�? �
{
�?�? 	

DACInserta
�?�? 
.
�?�? 1
#InsertarCargueMasivoRipsDepuradosAM
�?�? :
(
�?�?: ;
cdtll
�?�?; @
,
�?�?@ A
ref
�?�?B E
MsgRes
�?�?F L
)
�?�?L M
;
�?�?M N
}
�?�? 	
public
�?�? 
void
�?�? 1
#InsertarCargueMasivoRipsDepuradosAN
�?�? 7
(
�?�?7 8
List
�?�?8 <
<
�?�?< =*
rips_depurados_an_carguedtll
�?�?= Y
>
�?�?Y Z
cdtll
�?�?[ `
,
�?�?` a
ref
�?�?b e 
MessageResponseOBJ
�?�?f x
MsgRes
�?�?y 
)�?�? �
{
�?�? 	

DACInserta
�?�? 
.
�?�? 1
#InsertarCargueMasivoRipsDepuradosAN
�?�? :
(
�?�?: ;
cdtll
�?�?; @
,
�?�?@ A
ref
�?�?B E
MsgRes
�?�?F L
)
�?�?L M
;
�?�?M N
}
�?�? 	
public
�?�? 
int
�?�? #
InsertarPrestadorRIPS
�?�? (
(
�?�?( )"
Ref_RIPS_Prestadores
�?�?) =
obj
�?�?> A
)
�?�?A B
{
�?�? 	
return
�?�? 

DACInserta
�?�? 
.
�?�? #
InsertarPrestadorRIPS
�?�? 3
(
�?�?3 4
obj
�?�?4 7
)
�?�?7 8
;
�?�?8 9
}
�?�? 	
public
�?�? 
int
�?�? $
InsertarPrestadorRIPS2
�?�? )
(
�?�?) *"
Ref_RIPS_Prestadores
�?�?* >
obj
�?�?? B
)
�?�?B C
{
�?�? 	
return
�?�? 

DACInserta
�?�? 
.
�?�? $
InsertarPrestadorRIPS2
�?�? 4
(
�?�?4 5
obj
�?�?5 8
)
�?�?8 9
;
�?�?9 :
}
�?�? 	
public
�?�? 
List
�?�? 
<
�?�? "
Ref_RIPS_Prestadores
�?�? (
>
�?�?( )(
ConsultaPrestadoresRipsNit
�?�?* D
(
�?�?D E
double
�?�?E K
nit
�?�?L O
,
�?�?O P
ref
�?�?Q T 
MessageResponseOBJ
�?�?U g
MsgRes
�?�?h n
)
�?�?n o
{
�?�? 	
return
�?�? 
DACConsulta
�?�? 
.
�?�? (
ConsultaPrestadoresRipsNit
�?�? 9
(
�?�?9 :
nit
�?�?: =
,
�?�?= >
ref
�?�?? B
MsgRes
�?�?C I
)
�?�?I J
;
�?�?J K
}
�?�? 	
public
�?�? 
List
�?�? 
<
�?�? "
Ref_RIPS_Prestadores
�?�? (
>
�?�?( )0
"ConsultaPrestadoresRipsIdPrestador
�?�?* L
(
�?�?L M
string
�?�?M S
IDPrestador
�?�?T _
,
�?�?_ `
ref
�?�?a d 
MessageResponseOBJ
�?�?e w
MsgRes
�?�?x ~
)
�?�?~ 
{
�?�? 	
return
�?�? 
DACConsulta
�?�? 
.
�?�? 0
"ConsultaPrestadoresRipsIdPrestador
�?�? A
(
�?�?A B
IDPrestador
�?�?B M
,
�?�?M N
ref
�?�?O R
MsgRes
�?�?S Y
)
�?�?Y Z
;
�?�?Z [
}
�?�? 	
public
�?�? 
void
�?�? 1
#InsertarCargueMasivoRipsDepuradosAH
�?�? 7
(
�?�?7 8
List
�?�?8 <
<
�?�?< =*
rips_depurados_ah_carguedtll
�?�?= Y
>
�?�?Y Z
cdtll
�?�?[ `
,
�?�?` a
ref
�?�?b e 
MessageResponseOBJ
�?�?f x
MsgRes
�?�?y 
)�?�? �
{
�?�? 	

DACInserta
�?�? 
.
�?�? 1
#InsertarCargueMasivoRipsDepuradosAH
�?�? :
(
�?�?: ;
cdtll
�?�?; @
,
�?�?@ A
ref
�?�?B E
MsgRes
�?�?F L
)
�?�?L M
;
�?�?M N
}
�?�? 	
public
�?�? 
void
�?�? 2
$EliminarRipsDepuradosCargueBasePorId
�?�? 8
(
�?�?8 9
int
�?�?9 <
idCargueBase
�?�?= I
)
�?�?I J
{
�?�? 	

DACElimina
�?�? 
.
�?�? 2
$EliminarRipsDepuradosCargueBasePorId
�?�? ;
(
�?�?; <
idCargueBase
�?�?< H
)
�?�?H I
;
�?�?I J
}
�?�? 	
public
�?�? 
int
�?�? 
InsertarRembolso
�?�? #
(
�?�?# $ 
cuentas_reembolsos
�?�?$ 6
obj
�?�?7 :
)
�?�?: ;
{
�?�? 	
return
�?�? 

DACInserta
�?�? 
.
�?�? 
InsertarRembolso
�?�? .
(
�?�?. /
obj
�?�?/ 2
)
�?�?2 3
;
�?�?3 4
}
�@�@ 	
public
�@�@ 
int
�@�@ %
InsertarRembolsoDetalle
�@�@ *
(
�@�@* +'
cuentas_reembolso_detalle
�@�@+ D
obj
�@�@E H
)
�@�@H I
{
�@�@ 	
return
�@�@ 

DACInserta
�@�@ 
.
�@�@ %
InsertarRembolsoDetalle
�@�@ 5
(
�@�@5 6
obj
�@�@6 9
)
�@�@9 :
;
�@�@: ;
}
�@�@ 	
public
�@�@ 
int
�@�@ &
InsertarRembolsoArchivos
�@�@ +
(
�@�@+ ,)
cuentas_reembolsos_archivos
�@�@, G
obj
�@�@H K
)
�@�@K L
{
�@�@ 	
return
�@�@ 

DACInserta
�@�@ 
.
�@�@ &
InsertarRembolsoArchivos
�@�@ 6
(
�@�@6 7
obj
�@�@7 :
)
�@�@: ;
;
�@�@; <
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ 
ref_tipo_moneda
�@�@ #
>
�@�@# $

TipoMoneda
�@�@% /
(
�@�@/ 0
)
�@�@0 1
{
�@�@ 	
return
�@�@ 
DACComonClass
�@�@  
.
�@�@  !

TipoMoneda
�@�@! +
(
�@�@+ ,
)
�@�@, -
;
�@�@- .
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ "
ref_estado_reembolso
�@�@ (
>
�@�@( )
EstadoReembolso
�@�@* 9
(
�@�@9 :
)
�@�@: ;
{
�@�@ 	
return
�@�@ 
DACComonClass
�@�@  
.
�@�@  !
EstadoReembolso
�@�@! 0
(
�@�@0 1
)
�@�@1 2
;
�@�@2 3
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@  
ref_tipo_reembolso
�@�@ &
>
�@�@& '
TipoReembolso
�@�@( 5
(
�@�@5 6
)
�@�@6 7
{
�@�@ 	
return
�@�@ 
DACComonClass
�@�@  
.
�@�@  !
TipoReembolso
�@�@! .
(
�@�@. /
)
�@�@/ 0
;
�@�@0 1
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ 1
#management_reembolsos_tableroResult
�@�@ 7
>
�@�@7 8)
listadoReembolsosIngresados
�@�@9 T
(
�@�@T U
int
�@�@U X
?
�@�@X Y

idRegional
�@�@Z d
)
�@�@d e
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ )
listadoReembolsosIngresados
�@�@ :
(
�@�@: ;

idRegional
�@�@; E
)
�@�@E F
;
�@�@F G
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ =
/management_reembolsos_tablero_gestionadosResult
�@�@ C
>
�@�@C D*
listadoReembolsosGestionados
�@�@E a
(
�@�@a b
int
�@�@b e
?
�@�@e f

idRegional
�@�@g q
)
�@�@q r
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ *
listadoReembolsosGestionados
�@�@ ;
(
�@�@; <

idRegional
�@�@< F
)
�@�@F G
;
�@�@G H
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ !
ref_unis_reembolsos
�@�@ '
>
�@�@' (
UnisReembolsos
�@�@) 7
(
�@�@7 8
)
�@�@8 9
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ 
UnisReembolsos
�@�@ -
(
�@�@- .
)
�@�@. /
;
�@�@/ 0
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ 1
#management_reembolsos_gestionResult
�@�@ 7
>
�@�@7 82
$listadoReembolsosIngresadosGestiones
�@�@9 ]
(
�@�@] ^
int
�@�@^ a
?
�@�@a b
idReembolso
�@�@c n
)
�@�@n o
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ 2
$listadoReembolsosIngresadosGestiones
�@�@ C
(
�@�@C D
idReembolso
�@�@D O
)
�@�@O P
;
�@�@P Q
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ 9
+management_cuentas_reembolso_ArchivosResult
�@�@ ?
>
�@�@? @'
listadoReembolsosArchivos
�@�@A Z
(
�@�@Z [
int
�@�@[ ^
?
�@�@^ _
idReembolso
�@�@` k
)
�@�@k l
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ '
listadoReembolsosArchivos
�@�@ 8
(
�@�@8 9
idReembolso
�@�@9 D
)
�@�@D E
;
�@�@E F
}
�@�@ 	
public
�@�@ 
int
�@�@ '
ActualizarEstadoReembolso
�@�@ ,
(
�@�@, - 
cuentas_reembolsos
�@�@- ?
reem
�@�@@ D
)
�@�@D E
{
�@�@ 	
return
�@�@ 
DACActualiza
�@�@ 
.
�@�@  '
ActualizarEstadoReembolso
�@�@  9
(
�@�@9 :
reem
�@�@: >
)
�@�@> ?
;
�@�@? @
}
�@�@ 	
public
�@�@ 
int
�@�@ &
ActualizarDatosReembolso
�@�@ +
(
�@�@+ , 
cuentas_reembolsos
�@�@, >
reem
�@�@? C
)
�@�@C D
{
�@�@ 	
return
�@�@ 
DACActualiza
�@�@ 
.
�@�@  &
ActualizarDatosReembolso
�@�@  8
(
�@�@8 9
reem
�@�@9 =
)
�@�@= >
;
�@�@> ?
}
�@�@ 	
public
�@�@ 
int
�@�@ '
EliminarArchivoReembolsos
�@�@ ,
(
�@�@, -
int
�@�@- 0
?
�@�@0 1
	idArchivo
�@�@2 ;
)
�@�@; <
{
�@�@ 	
return
�@�@ 

DACElimina
�@�@ 
.
�@�@ '
EliminarArchivoReembolsos
�@�@ 7
(
�@�@7 8
	idArchivo
�@�@8 A
)
�@�@A B
;
�@�@B C
}
�@�@ 	
public
�@�@  
cuentas_reembolsos
�@�@ !!
TraerDatosReembolso
�@�@" 5
(
�@�@5 6
int
�@�@6 9
?
�@�@9 :
idReembolso
�@�@; F
)
�@�@F G
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ !
TraerDatosReembolso
�@�@ 2
(
�@�@2 3
idReembolso
�@�@3 >
)
�@�@> ?
;
�@�@? @
}
�@�@ 	
public
�@�@ )
cuentas_reembolsos_archivos
�@�@ *"
TrarArchivoReembolso
�@�@+ ?
(
�@�@? @
int
�@�@@ C
?
�@�@C D
	idArchivo
�@�@E N
)
�@�@N O
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ "
TrarArchivoReembolso
�@�@ 3
(
�@�@3 4
	idArchivo
�@�@4 =
)
�@�@= >
;
�@�@> ?
}
�@�@ 	
public
�@�@ 
int
�@�@ 
InsertarNoRips
�@�@ !
(
�@�@! "$
cuentas_medicas_norips
�@�@" 8
obj
�@�@9 <
,
�@�@< =
ref
�@�@> A 
MessageResponseOBJ
�@�@B T
MsgRes
�@�@U [
)
�@�@[ \
{
�@�@ 	
return
�@�@ 

DACInserta
�@�@ 
.
�@�@ 
InsertarNoRips
�@�@ ,
(
�@�@, -
obj
�@�@- 0
,
�@�@0 1
ref
�@�@2 5
MsgRes
�@�@6 <
)
�@�@< =
;
�@�@= >
}
�@�@ 	
public
�@�@ 
int
�@�@  
EliminarCasoNoRips
�@�@ %
(
�@�@% &
int
�@�@& )
?
�@�@) *
idMed
�@�@+ 0
)
�@�@0 1
{
�@�@ 	
return
�@�@ 

DACElimina
�@�@ 
.
�@�@  
EliminarCasoNoRips
�@�@ 0
(
�@�@0 1
idMed
�@�@1 6
)
�@�@6 7
;
�@�@7 8
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ 7
)management_usuariosAnalistas_noripsResult
�@�@ =
>
�@�@= >
ListadoAnalistas
�@�@? O
(
�@�@O P
)
�@�@P Q
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ 
ListadoAnalistas
�@�@ /
(
�@�@/ 0
)
�@�@0 1
;
�@�@1 2
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@  
Total_Departamento
�@�@ &
>
�@�@& ' 
TraerDepartamentos
�@�@( :
(
�@�@: ;
)
�@�@; <
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@  
TraerDepartamentos
�@�@ 1
(
�@�@1 2
)
�@�@2 3
;
�@�@3 4
}
�@�@ 	
public
�@�@  
Total_Departamento
�@�@ !!
TraerDepartamentoId
�@�@" 5
(
�@�@5 6
int
�@�@6 9
?
�@�@9 :
id
�@�@; =
)
�@�@= >
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ !
TraerDepartamentoId
�@�@ 2
(
�@�@2 3
id
�@�@3 5
)
�@�@5 6
;
�@�@6 7
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ 2
$management_total_departamentosResult
�@�@ 8
>
�@�@8 9(
TraerDepartamentosRegional
�@�@: T
(
�@�@T U
int
�@�@U X
?
�@�@X Y
regional
�@�@Z b
)
�@�@b c
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ (
TraerDepartamentosRegional
�@�@ 9
(
�@�@9 :
regional
�@�@: B
)
�@�@B C
;
�@�@C D
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ /
!ref_cuentasmedicas_notips_motivos
�@�@ 5
>
�@�@5 6 
ListaMotivosNoRips
�@�@7 I
(
�@�@I J
)
�@�@J K
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@  
ListaMotivosNoRips
�@�@ 1
(
�@�@1 2
)
�@�@2 3
;
�@�@3 4
}
�@�@ 	
public
�@�@ 
Int32
�@�@ )
IngresoArchivosRipsNoEsalud
�@�@ 0
(
�@�@0 1-
cuentas_medicas_norips_Archivos
�@�@1 P
OBJ
�@�@Q T
,
�@�@T U
ref
�@�@V Y 
MessageResponseOBJ
�@�@Z l
MsgRes
�@�@m s
)
�@�@s t
{
�@�@ 	
return
�@�@ 

DACInserta
�@�@ 
.
�@�@ )
IngresoArchivosRipsNoEsalud
�@�@ 9
(
�@�@9 :
OBJ
�@�@: =
,
�@�@= >
ref
�@�@? B
MsgRes
�@�@C I
)
�@�@I J
;
�@�@J K
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ :
,management_cuentasMedicas_ripsNoEsaludResult
�@�@ @
>
�@�@@ A!
TableroRipsNoEsalud
�@�@B U
(
�@�@U V
DateTime
�@�@V ^
?
�@�@^ _
fechaInicio
�@�@` k
,
�@�@k l
DateTime
�@�@m u
?
�@�@u v
fechaFin
�@�@w 
,�@�@ �
int�@�@� �
?�@�@� �
regional�@�@� �
)�@�@� �
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ !
TableroRipsNoEsalud
�@�@ 2
(
�@�@2 3
fechaInicio
�@�@3 >
,
�@�@> ?
fechaFin
�@�@@ H
,
�@�@H I
regional
�@�@J R
)
�@�@R S
;
�@�@S T
}
�@�@ 	
public
�@�@ 
List
�@�@ 
<
�@�@ C
5management_cuentasMedicas_ripsNoEsalud_archivosResult
�@�@ I
>
�@�@I J,
TableroRepositorioRipsNoEsalud
�@�@K i
(
�@�@i j
int
�@�@j m
?
�@�@m n
idMed
�@�@o t
)
�@�@t u
{
�@�@ 	
return
�@�@ 
DACConsulta
�@�@ 
.
�@�@ ,
TableroRepositorioRipsNoEsalud
�@�@ =
(
�@�@= >
idMed
�@�@> C
)
�@�@C D
;
�@�@D E
}
�@�@ 	
public
�@�@ -
cuentas_medicas_norips_Archivos
�@�@ . 
traerArchivoNoRips
�@�@/ A
(
�@�@A B
int
�@�@B E
	idArchivo
�@�@F O
)
�@�@O P
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A  
traerArchivoNoRips
�A�A 1
(
�A�A1 2
	idArchivo
�A�A2 ;
)
�A�A; <
;
�A�A< =
}
�A�A 	
public
�A�A 
List
�A�A 
<
�A�A H
:management_cuentasMedicas_ripsNoEsalud_TodosArchivosResult
�A�A N
>
�A�AN O/
!TraerTodosLosArchivosRipsNoEsalud
�A�AP q
(
�A�Aq r
DateTime
�A�Ar z
?
�A�Az {
fechaInicio�A�A| �
,�A�A� �
DateTime�A�A� �
?�A�A� �
fechaFin�A�A� �
,�A�A� �
int�A�A� �
?�A�A� �
regional�A�A� �
)�A�A� �
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A /
!TraerTodosLosArchivosRipsNoEsalud
�A�A @
(
�A�A@ A
fechaInicio
�A�AA L
,
�A�AL M
fechaFin
�A�AN V
,
�A�AV W
regional
�A�AX `
)
�A�A` a
;
�A�Aa b
}
�A�A 	
public
�A�A 
List
�A�A 
<
�A�A ;
-management_baseBeneficiarios_xDocumentoResult
�A�A A
>
�A�AA B(
BusquedaBeneficiarioCedula
�A�AC ]
(
�A�A] ^
string
�A�A^ d
	documento
�A�Ae n
)
�A�An o
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A (
BusquedaBeneficiarioCedula
�A�A 9
(
�A�A9 :
	documento
�A�A: C
)
�A�AC D
;
�A�AD E
}
�A�A 	
public
�A�A 
int
�A�A &
InsertarCreacionCampanas
�A�A +
(
�A�A+ ,
creacion_campana
�A�A, <
obj
�A�A= @
)
�A�A@ A
{
�A�A 	
return
�A�A 

DACInserta
�A�A 
.
�A�A &
InsertarCreacionCampanas
�A�A 6
(
�A�A6 7
obj
�A�A7 :
)
�A�A: ;
;
�A�A; <
}
�A�A 	
public
�A�A 
int
�A�A -
InsertarCreacionCampanasDetalle
�A�A 2
(
�A�A2 3&
creacion_campana_detalle
�A�A3 K
obj
�A�AL O
)
�A�AO P
{
�A�A 	
return
�A�A 

DACInserta
�A�A 
.
�A�A -
InsertarCreacionCampanasDetalle
�A�A =
(
�A�A= >
obj
�A�A> A
)
�A�AA B
;
�A�AB C
}
�A�A 	
public
�A�A 
int
�A�A 5
'InsertarCreacionCampanasDetalleListados
�A�A :
(
�A�A: ;
List
�A�A; ?
<
�A�A? @%
creacion_campana_listas
�A�A@ W
>
�A�AW X
listas
�A�AY _
,
�A�A_ `
List
�A�Aa e
<
�A�Ae f-
creacion_campana_camposSimples�A�Af �
>�A�A� �
simples�A�A� �
)�A�A� �
{
�A�A 	
return
�A�A 

DACInserta
�A�A 
.
�A�A 5
'InsertarCreacionCampanasDetalleListados
�A�A E
(
�A�AE F
listas
�A�AF L
,
�A�AL M
simples
�A�AN U
)
�A�AU V
;
�A�AV W
}
�A�A 	
public
�A�A 
List
�A�A 
<
�A�A 5
'management_campana_tableroControlResult
�A�A ;
>
�A�A; <
ListadoCampanas
�A�A= L
(
�A�AL M
)
�A�AM N
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A 
ListadoCampanas
�A�A .
(
�A�A. /
)
�A�A/ 0
;
�A�A0 1
}
�A�A 	
public
�A�A *
ref_campanas_permisosEdicion
�A�A +)
traerPermisosEdicionCampana
�A�A, G
(
�A�AG H
int
�A�AH K
?
�A�AK L
	idUsuario
�A�AM V
)
�A�AV W
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A )
traerPermisosEdicionCampana
�A�A :
(
�A�A: ;
	idUsuario
�A�A; D
)
�A�AD E
;
�A�AE F
}
�A�A 	
public
�A�A 
creacion_campana
�A�A #
TraerCampanaGeneralId
�A�A  5
(
�A�A5 6
int
�A�A6 9
?
�A�A9 :
id
�A�A; =
)
�A�A= >
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A #
TraerCampanaGeneralId
�A�A 4
(
�A�A4 5
id
�A�A5 7
)
�A�A7 8
;
�A�A8 9
}
�A�A 	
public
�A�A 
creacion_campana
�A�A *
TraerCampanaVersionGeneralId
�A�A  <
(
�A�A< =
int
�A�A= @
?
�A�A@ A
id
�A�AB D
)
�A�AD E
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A *
TraerCampanaVersionGeneralId
�A�A ;
(
�A�A; <
id
�A�A< >
)
�A�A> ?
;
�A�A? @
}
�A�A 	
public
�A�A 
List
�A�A 
<
�A�A &
creacion_campana_detalle
�A�A ,
>
�A�A, -*
TraerCampanaGeneraDetallelId
�A�A. J
(
�A�AJ K
int
�A�AK N
?
�A�AN O
id
�A�AP R
)
�A�AR S
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A *
TraerCampanaGeneraDetallelId
�A�A ;
(
�A�A; <
id
�A�A< >
)
�A�A> ?
;
�A�A? @
}
�A�A 	
public
�A�A 
List
�A�A 
<
�A�A &
ref_campana_tipoPregunta
�A�A ,
>
�A�A, -'
TraerTipoPreguntaCampaña
�A�A. F
(
�A�AF G
)
�A�AG H
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A '
TraerTipoPreguntaCampaña
�A�A 7
(
�A�A7 8
)
�A�A8 9
;
�A�A9 :
}
�A�A 	
public
�A�A 
List
�A�A 
<
�A�A ,
creacion_campana_camposSimples
�A�A 2
>
�A�A2 30
"TraerCampanaCamposSimplesIdCampana
�A�A4 V
(
�A�AV W
int
�A�AW Z
?
�A�AZ [
id
�A�A\ ^
)
�A�A^ _
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A 0
"TraerCampanaCamposSimplesIdCampana
�A�A A
(
�A�AA B
id
�A�AB D
)
�A�AD E
;
�A�AE F
}
�A�A 	
public
�A�A 
List
�A�A 
<
�A�A %
creacion_campana_listas
�A�A +
>
�A�A+ ,.
 TraerCampanaCamposListaIdCampana
�A�A- M
(
�A�AM N
int
�A�AN Q
?
�A�AQ R
id
�A�AS U
)
�A�AU V
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A .
 TraerCampanaCamposListaIdCampana
�A�A ?
(
�A�A? @
id
�A�A@ B
)
�A�AB C
;
�A�AC D
}
�A�A 	
public
�A�A 
int
�A�A (
insertarRespuestasCamapana
�A�A -
(
�A�A- .
List
�A�A. 2
<
�A�A2 3 
campana_respuestas
�A�A3 E
>
�A�AE F
listaPreguntas
�A�AG U
,
�A�AU V
ref
�A�AW Z 
MessageResponseOBJ
�A�A[ m
MsgRes
�A�An t
)
�A�At u
{
�A�A 	
return
�A�A 

DACInserta
�A�A 
.
�A�A (
insertarRespuestasCamapana
�A�A 8
(
�A�A8 9
listaPreguntas
�A�A9 G
,
�A�AG H
ref
�A�AI L
MsgRes
�A�AM S
)
�A�AS T
;
�A�AT U
}
�A�A 	
public
�A�A 
int
�A�A <
.IngresarRespuestaCampañaVerificacion_Archivos
�A�A @
(
�A�A@ A 
campana_respuestas
�A�AA S
obj
�A�AT W
)
�A�AW X
{
�A�A 	
return
�A�A 

DACInserta
�A�A 
.
�A�A <
.IngresarRespuestaCampañaVerificacion_Archivos
�A�A K
(
�A�AK L
obj
�A�AL O
)
�A�AO P
;
�A�AP Q
}
�A�A 	
public
�A�A 
int
�A�A 5
'insertarRespuestasCampanaVerificaciones
�A�A :
(
�A�A: ;
List
�A�A; ?
<
�A�A? @3
%campana_respuestas_tipoVerificaciones
�A�A@ e
>
�A�Ae f!
listaVerificaciones
�A�Ag z
,
�A�Az {
ref
�A�A| "
MessageResponseOBJ�A�A� �
MsgRes�A�A� �
)�A�A� �
{
�A�A 	
return
�A�A 

DACInserta
�A�A 
.
�A�A 5
'insertarRespuestasCampanaVerificaciones
�A�A E
(
�A�AE F!
listaVerificaciones
�A�AF Y
,
�A�AY Z
ref
�A�A[ ^
MsgRes
�A�A_ e
)
�A�Ae f
;
�A�Af g
}
�A�A 	
public
�A�A 
int
�A�A /
!insertarRespuestasCampanaArchivos
�A�A 4
(
�A�A4 5
List
�A�A5 9
<
�A�A9 :,
campana_respuestas_tipoArchivo
�A�A: X
>
�A�AX Y
listaArchivos
�A�AZ g
,
�A�Ag h
ref
�A�Ai l 
MessageResponseOBJ
�A�Am 
MsgRes�A�A� �
)�A�A� �
{
�A�A 	
return
�A�A 

DACInserta
�A�A 
.
�A�A /
!insertarRespuestasCampanaArchivos
�A�A ?
(
�A�A? @
listaArchivos
�A�A@ M
,
�A�AM N
ref
�A�AO R
MsgRes
�A�AS Y
)
�A�AY Z
;
�A�AZ [
}
�A�A 	
public
�A�A 
int
�A�A '
DesactivarActivarCampaña
�A�A +
(
�A�A+ ,
int
�A�A, /
?
�A�A/ 0
	idCampana
�A�A1 :
,
�A�A: ;
int
�A�A< ?
?
�A�A? @
estado
�A�AA G
)
�A�AG H
{
�A�A 	
return
�A�A 
DACActualiza
�A�A 
.
�A�A  '
DesactivarActivarCampaña
�A�A  8
(
�A�A8 9
	idCampana
�A�A9 B
,
�A�AB C
estado
�A�AD J
)
�A�AJ K
;
�A�AK L
}
�A�A 	
public
�A�A ,
creacion_campana_camposSimples
�A�A -0
"TraerCampanaCamposSimplesIdDetalle
�A�A. P
(
�A�AP Q
int
�A�AQ T
?
�A�AT U
id
�A�AV X
)
�A�AX Y
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A 0
"TraerCampanaCamposSimplesIdDetalle
�A�A A
(
�A�AA B
id
�A�AB D
)
�A�AD E
;
�A�AE F
}
�A�A 	
public
�A�A 
List
�A�A 
<
�A�A %
creacion_campana_listas
�A�A +
>
�A�A+ ,.
 TraerCampanaCamposListaIdDetalle
�A�A- M
(
�A�AM N
int
�A�AN Q
?
�A�AQ R
id
�A�AS U
)
�A�AU V
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A .
 TraerCampanaCamposListaIdDetalle
�A�A ?
(
�A�A? @
id
�A�A@ B
)
�A�AB C
;
�A�AC D
}
�A�A 	
public
�A�A &
creacion_campana_detalle
�A�A ''
TraerDatosPreguntaCampana
�A�A( A
(
�A�AA B
int
�A�AB E
?
�A�AE F
id
�A�AG I
)
�A�AI J
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A '
TraerDatosPreguntaCampana
�A�A 8
(
�A�A8 9
id
�A�A9 ;
)
�A�A; <
;
�A�A< =
}
�A�A 	
public
�A�A 
List
�A�A 
<
�A�A &
creacion_campana_detalle
�A�A ,
>
�A�A, -*
ConsultaDtllPreguntasCampana
�A�A. J
(
�A�AJ K
int
�A�AK N
?
�A�AN O
	idcampana
�A�AP Y
)
�A�AY Z
{
�A�A 	
return
�A�A 
DACConsulta
�A�A 
.
�A�A *
ConsultaDtllPreguntasCampana
�A�A ;
(
�A�A; <
	idcampana
�A�A< E
)
�A�AE F
;
�A�AF G
}
�A�A 	
public
�A�A 
int
�A�A '
ActualizarVersionCampaña
�A�A +
(
�A�A+ ,
creacion_campana
�A�A, <
cam
�A�A= @
)
�A�A@ A
{
�A�A 	
return
�A�A 
DACActualiza
�A�A 
.
�A�A  '
ActualizarVersionCampaña
�A�A  8
(
�A�A8 9
cam
�A�A9 <
)
�A�A< =
;
�A�A= >
}
�A�A 	
public
�A�A 
int
�A�A -
ActualizarDatosCampañaPregunta
�A�A 1
(
�A�A1 2&
creacion_campana_detalle
�A�A2 J
cam
�A�AK N
)
�A�AN O
{
�A�A 	
return
�A�A 
DACActualiza
�A�A 
.
�A�A  -
ActualizarDatosCampañaPregunta
�A�A  >
(
�A�A> ?
cam
�A�A? B
)
�A�AB C
;
�A�AC D
}
�A�A 	
public
�B�B 
void
�B�B !
ActualizarInactivas
�B�B '
(
�B�B' (
List
�B�B( ,
<
�B�B, -&
creacion_campana_detalle
�B�B- E
>
�B�BE F!
ActualizarInactivas
�B�BG Z
,
�B�BZ [
ref
�B�B\ _ 
MessageResponseOBJ
�B�B` r
msg
�B�Bs v
)
�B�Bv w
{
�B�B 	
DACActualiza
�B�B 
.
�B�B !
ActualizarInactivas
�B�B ,
(
�B�B, -!
ActualizarInactivas
�B�B- @
,
�B�B@ A
ref
�B�BB E
msg
�B�BF I
)
�B�BI J
;
�B�BJ K
}
�B�B 	
public
�B�B 
int
�B�B "
InsertarLoteCampaña
�B�B &
(
�B�B& '%
campana_respuestas_lote
�B�B' >
lote
�B�B? C
)
�B�BC D
{
�B�B 	
return
�B�B 

DACInserta
�B�B 
.
�B�B "
InsertarLoteCampaña
�B�B 1
(
�B�B1 2
lote
�B�B2 6
)
�B�B6 7
;
�B�B7 8
}
�B�B 	
public
�B�B 
int
�B�B '
ActualizarCamposPreguntas
�B�B ,
(
�B�B, -
int
�B�B- 0
?
�B�B0 1

idPregunta
�B�B2 <
)
�B�B< =
{
�B�B 	
return
�B�B 
DACActualiza
�B�B 
.
�B�B  '
ActualizarCamposPreguntas
�B�B  9
(
�B�B9 :

idPregunta
�B�B: D
)
�B�BD E
;
�B�BE F
}
�B�B 	
public
�B�B 
int
�B�B '
InsertarLogCarguesMasivos
�B�B ,
(
�B�B, -!
log_cargues_masivos
�B�B- @
obj
�B�BA D
)
�B�BD E
{
�B�B 	
return
�B�B 

DACInserta
�B�B 
.
�B�B '
InsertarLogCarguesMasivos
�B�B 7
(
�B�B7 8
obj
�B�B8 ;
)
�B�B; <
;
�B�B< =
}
�B�B 	
public
�B�B 
int
�B�B '
CargueAlertasDispensacion
�B�B ,
(
�B�B, -"
alertas_dispensacion
�B�B- A
obj
�B�BB E
,
�B�BE F
List
�B�BG K
<
�B�BK L,
alertas_dispensacion_registros
�B�BL j
>
�B�Bj k
detalle
�B�Bl s
,
�B�Bs t
ref
�B�Bu x!
MessageResponseOBJ�B�By �
MsgRes�B�B� �
)�B�B� �
{
�B�B 	
return
�B�B 

DACInserta
�B�B 
.
�B�B '
CargueAlertasDispensacion
�B�B 7
(
�B�B7 8
obj
�B�B8 ;
,
�B�B; <
detalle
�B�B= D
,
�B�BD E
ref
�B�BF I
MsgRes
�B�BJ P
)
�B�BP Q
;
�B�BQ R
}
�B�B 	
public
�B�B 
List
�B�B 
<
�B�B A
3management_alertasDispensacion_tableroControlResult
�B�B G
>
�B�BG H(
ListadoAlertasDispensacion
�B�BI c
(
�B�Bc d
DateTime
�B�Bd l
?
�B�Bl m!
fecha_prescripcion�B�Bn �
,�B�B� �
string�B�B� �
numero_formula�B�B� �
,�B�B� �
string�B�B� �&
documento_beneficiario�B�B� �
,�B�B� �
string�B�B� �
id_prescriptor�B�B� �
,�B�B� �
string�B�B� � 
nombre_comercial�B�B� �
)�B�B� �
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B (
ListadoAlertasDispensacion
�B�B 9
(
�B�B9 : 
fecha_prescripcion
�B�B: L
,
�B�BL M
numero_formula
�B�BN \
,
�B�B\ ]$
documento_beneficiario
�B�B^ t
,
�B�Bt u
id_prescriptor�B�Bv �
,�B�B� � 
nombre_comercial�B�B� �
)�B�B� �
;�B�B� �
}
�B�B 	
public
�B�B 
List
�B�B 
<
�B�B D
6management_alertasDispensacion_tableroControl_idResult
�B�B J
>
�B�BJ K!
TraerRegistroAlerta
�B�BL _
(
�B�B_ `
int
�B�B` c
?
�B�Bc d

idRegistro
�B�Be o
)
�B�Bo p
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B !
TraerRegistroAlerta
�B�B 2
(
�B�B2 3

idRegistro
�B�B3 =
)
�B�B= >
;
�B�B> ?
}
�B�B 	
public
�B�B 
List
�B�B 
<
�B�B H
:management_alertasDispensacion_buscarNombreComercialResult
�B�B N
>
�B�BN O"
TraerNombreComercial
�B�BP d
(
�B�Bd e
string
�B�Be k
nombre_comercial
�B�Bl |
)
�B�B| }
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B "
TraerNombreComercial
�B�B 3
(
�B�B3 4
nombre_comercial
�B�B4 D
)
�B�BD E
;
�B�BE F
}
�B�B 	
public
�B�B 
List
�B�B 
<
�B�B M
?management_alertasDispensacion_tableroControl_gestionadasResult
�B�B S
>
�B�BS T3
%ListadoAlertasDispensacionGestionadas
�B�BU z
(
�B�Bz {
)
�B�B{ |
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B 3
%ListadoAlertasDispensacionGestionadas
�B�B D
(
�B�BD E
)
�B�BE F
;
�B�BF G
}
�B�B 	
public
�B�B 
List
�B�B 
<
�B�B V
Hmanagement_alertasDispensacion_tableroControl_gestionadasDetalladaResult
�B�B \
>
�B�B\ ]=
.ListadoAlertasDispensacionGestionadasDetallada�B�B^ �
(�B�B� �
)�B�B� �
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B <
.ListadoAlertasDispensacionGestionadasDetallada
�B�B M
(
�B�BM N
)
�B�BN O
;
�B�BO P
}
�B�B 	
public
�B�B 
List
�B�B 
<
�B�B U
Gmanagement_alertasDispensacion_tableroControl_gestionadasArchivosResult
�B�B [
>
�B�B[ \<
-ListadoAlertasDispensacionGestionadasArchivos�B�B] �
(�B�B� �
int�B�B� �
?�B�B� �
	idDetalle�B�B� �
)�B�B� �
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B ;
-ListadoAlertasDispensacionGestionadasArchivos
�B�B L
(
�B�BL M
	idDetalle
�B�BM V
)
�B�BV W
;
�B�BW X
}
�B�B 	
public
�B�B 
int
�B�B 0
"InsertarRespuestaAlertaDiepnsacion
�B�B 5
(
�B�B5 6*
alertas_dispensacion_detalle
�B�B6 R
obj
�B�BS V
)
�B�BV W
{
�B�B 	
return
�B�B 

DACInserta
�B�B 
.
�B�B 0
"InsertarRespuestaAlertaDiepnsacion
�B�B @
(
�B�B@ A
obj
�B�BA D
)
�B�BD E
;
�B�BE F
}
�B�B 	
public
�B�B 
int
�B�B *
InsertarArchivoAlertasDispen
�B�B /
(
�B�B/ 03
%alertas_dispensacion_detalle_archivos
�B�B0 U
obj
�B�BV Y
)
�B�BY Z
{
�B�B 	
return
�B�B 

DACInserta
�B�B 
.
�B�B *
InsertarArchivoAlertasDispen
�B�B :
(
�B�B: ;
obj
�B�B; >
)
�B�B> ?
;
�B�B? @
}
�B�B 	
public
�B�B 3
%alertas_dispensacion_detalle_archivos
�B�B 4'
TraerArchivoAlertasDispen
�B�B5 N
(
�B�BN O
int
�B�BO R
?
�B�BR S
	idArchivo
�B�BT ]
)
�B�B] ^
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B '
TraerArchivoAlertasDispen
�B�B 8
(
�B�B8 9
	idArchivo
�B�B9 B
)
�B�BB C
;
�B�BC D
}
�B�B 	
public
�B�B 
List
�B�B 
<
�B�B L
>management_alertasDispensacion_tableroControl_respuestasResult
�B�B R
>
�B�BR S1
#ListadoAlertasDispensacionGestiones
�B�BT w
(
�B�Bw x
int
�B�Bx {
?
�B�B{ |
	idDetalle�B�B} �
)�B�B� �
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B 1
#ListadoAlertasDispensacionGestiones
�B�B B
(
�B�BB C
	idDetalle
�B�BC L
)
�B�BL M
;
�B�BM N
}
�B�B 	
public
�B�B 
int
�B�B )
InsercionMortalidadRegistro
�B�B .
(
�B�B. /"
mortalidad_registros
�B�B/ C
obj
�B�BD G
)
�B�BG H
{
�B�B 	
return
�B�B 

DACInserta
�B�B 
.
�B�B )
InsercionMortalidadRegistro
�B�B 9
(
�B�B9 :
obj
�B�B: =
)
�B�B= >
;
�B�B> ?
}
�B�B 	
public
�B�B 
List
�B�B 
<
�B�B 0
"management_tiposBeneficiarioResult
�B�B 6
>
�B�B6 7$
TraerTiposBeneficiario
�B�B8 N
(
�B�BN O
)
�B�BO P
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B $
TraerTiposBeneficiario
�B�B 5
(
�B�B5 6
)
�B�B6 7
;
�B�B7 8
}
�B�B 	
public
�B�B "
mortalidad_registros
�B�B #"
TraerDatosMortalidad
�B�B$ 8
(
�B�B8 9
int
�B�B9 <
?
�B�B< =
idMortalidad
�B�B> J
)
�B�BJ K
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B "
TraerDatosMortalidad
�B�B 3
(
�B�B3 4
idMortalidad
�B�B4 @
)
�B�B@ A
;
�B�BA B
}
�B�B 	
public
�B�B "
mortalidad_registros
�B�B #0
"TraerDatosMortalidadIdentificacion
�B�B$ F
(
�B�BF G
string
�B�BG M
identificacion
�B�BN \
)
�B�B\ ]
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B 0
"TraerDatosMortalidadIdentificacion
�B�B A
(
�B�BA B
identificacion
�B�BB P
)
�B�BP Q
;
�B�BQ R
}
�B�B 	
public
�B�B 
List
�B�B 
<
�B�B 0
"management_TableroMortalidadResult
�B�B 6
>
�B�B6 7&
TraerMortalidadesTablero
�B�B8 P
(
�B�BP Q
int
�B�BQ T
?
�B�BT U
año
�B�BV Y
,
�B�BY Z
int
�B�B[ ^
?
�B�B^ _
	trimestre
�B�B` i
,
�B�Bi j
int
�B�Bk n
?
�B�Bn o
mes
�B�Bp s
,
�B�Bs t
int
�B�Bu x
?
�B�Bx y
unis
�B�Bz ~
,
�B�B~ 
int�B�B� �
?�B�B� �
regional�B�B� �
,�B�B� �
string�B�B� �
	documento�B�B� �
,�B�B� �
DateTime�B�B� �
?�B�B� �
fechaFiltro�B�B� �
)�B�B� �
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B &
TraerMortalidadesTablero
�B�B 7
(
�B�B7 8
año
�B�B8 ;
,
�B�B; <
	trimestre
�B�B= F
,
�B�BF G
mes
�B�BH K
,
�B�BK L
unis
�B�BM Q
,
�B�BQ R
regional
�B�BS [
,
�B�B[ \
	documento
�B�B] f
,
�B�Bf g
fechaFiltro
�B�Bh s
)
�B�Bs t
;
�B�Bt u
}
�B�B 	
public
�B�B 
List
�B�B 
<
�B�B /
!management_TableroNatalidadResult
�B�B 5
>
�B�B5 6%
TraerNatalidadesTablero
�B�B7 N
(
�B�BN O
int
�B�BO R
?
�B�BR S
año
�B�BT W
,
�B�BW X
int
�B�BY \
?
�B�B\ ]
	trimestre
�B�B^ g
,
�B�Bg h
int
�B�Bi l
?
�B�Bl m
mes
�B�Bn q
,
�B�Bq r
int
�B�Bs v
?
�B�Bv w
unis
�B�Bx |
,
�B�B| }
int�B�B~ �
?�B�B� �
regional�B�B� �
,�B�B� �
string�B�B� �
	documento�B�B� �
,�B�B� �
DateTime�B�B� �
?�B�B� �
fechaFiltro�B�B� �
)�B�B� �
{
�B�B 	
return
�B�B 
DACConsulta
�B�B 
.
�B�B %
TraerNatalidadesTablero
�B�B 6
(
�B�B6 7
año
�B�B7 :
,
�B�B: ;
	trimestre
�B�B< E
,
�B�BE F
mes
�B�BG J
,
�B�BJ K
unis
�B�BL P
,
�B�BP Q
regional
�B�BR Z
,
�B�BZ [
	documento
�B�B\ e
,
�B�Be f
fechaFiltro
�B�Bg r
)
�B�Br s
;
�B�Bs t
}
�B�B 	
public
�B�B 
int
�B�B *
ActualizarRegistroMortalidad
�B�B /
(
�B�B/ 0"
mortalidad_registros
�B�B0 D
reg
�B�BE H
)
�B�BH I
{
�B�B 	
return
�B�B 
DACActualiza
�B�B 
.
�B�B  *
ActualizarRegistroMortalidad
�B�B  <
(
�B�B< =
reg
�B�B= @
)
�B�B@ A
;
�B�BA B
}
�B�B 	
public
�B�B 
int
�B�B (
InsercionNatalidadRegistro
�B�B -
(
�B�B- .!
natalidad_registros
�B�B. A
obj
�B�BB E
)
�B�BE F
{
�B�B 	
return
�B�B 

DACInserta
�B�B 
.
�B�B (
InsercionNatalidadRegistro
�B�B 8
(
�B�B8 9
obj
�B�B9 <
)
�B�B< =
;
�B�B= >
}
�B�B 	
public
�B�B 
int
�B�B )
ActualizarRegistroNatalidad
�B�B .
(
�B�B. /!
natalidad_registros
�B�B/ B
nat
�B�BC F
)
�B�BF G
{
�B�B 	
return
�B�B 
DACActualiza
�B�B 
.
�B�B  )
ActualizarRegistroNatalidad
�B�B  ;
(
�B�B; <
nat
�B�B< ?
)
�B�B? @
;
�B�B@ A
}
�C�C 	
public
�C�C !
natalidad_registros
�C�C "!
TraerDatosNatalidad
�C�C# 6
(
�C�C6 7
int
�C�C7 :
?
�C�C: ;
idNatalidad
�C�C< G
)
�C�CG H
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C !
TraerDatosNatalidad
�C�C 2
(
�C�C2 3
idNatalidad
�C�C3 >
)
�C�C> ?
;
�C�C? @
}
�C�C 	
public
�C�C 
int
�C�C  
CargueEventosSalud
�C�C %
(
�C�C% &!
evento_salud_cargue
�C�C& 9
obj
�C�C: =
,
�C�C= >
List
�C�C? C
<
�C�CC D%
eventos_salud_registros
�C�CD [
>
�C�C[ \
detalle
�C�C] d
,
�C�Cd e
ref
�C�Cf i 
MessageResponseOBJ
�C�Cj |
MsgRes�C�C} �
)�C�C� �
{
�C�C 	
return
�C�C 

DACInserta
�C�C 
.
�C�C  
CargueEventosSalud
�C�C 0
(
�C�C0 1
obj
�C�C1 4
,
�C�C4 5
detalle
�C�C6 =
,
�C�C= >
ref
�C�C? B
MsgRes
�C�CC I
)
�C�CI J
;
�C�CJ K
}
�C�C 	
public
�C�C 
int
�C�C !
InsertarEventoSalud
�C�C &
(
�C�C& '%
eventos_salud_registros
�C�C' >
evento
�C�C? E
)
�C�CE F
{
�C�C 	
return
�C�C 

DACInserta
�C�C 
.
�C�C !
InsertarEventoSalud
�C�C 1
(
�C�C1 2
evento
�C�C2 8
)
�C�C8 9
;
�C�C9 :
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C ,
ref_eventosSalud_fuenteReporte
�C�C 2
>
�C�C2 3,
ListaEventosSaludFuenteReporte
�C�C4 R
(
�C�CR S
)
�C�CS T
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C ,
ListaEventosSaludFuenteReporte
�C�C =
(
�C�C= >
)
�C�C> ?
;
�C�C? @
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C /
!ref_eventosSalud_ambitoOcurrencia
�C�C 5
>
�C�C5 6/
!ListaEventosSaludAmbitoOcurrencia
�C�C7 X
(
�C�CX Y
)
�C�CY Z
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C /
!ListaEventosSaludAmbitoOcurrencia
�C�C @
(
�C�C@ A
)
�C�CA B
;
�C�CB C
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C 1
#ref_eventosSalud_severidadDesenlace
�C�C 7
>
�C�C7 81
#ListaEventosSaludSeveridadDesenlace
�C�C9 \
(
�C�C\ ]
)
�C�C] ^
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C 1
#ListaEventosSaludSeveridadDesenlace
�C�C B
(
�C�CB C
)
�C�CC D
;
�C�CD E
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C 5
'ref_eventosSalud_ProbabilidadRepeticion
�C�C ;
>
�C�C; <5
'ListaEventosSaludProbabilidadRepeticion
�C�C= d
(
�C�Cd e
)
�C�Ce f
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C 5
'ListaEventosSaludProbabilidadRepeticion
�C�C F
(
�C�CF G
)
�C�CG H
;
�C�CH I
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C 0
"ref_eventosSalud_conceptoAuditoria
�C�C 6
>
�C�C6 70
"ListaEventosSaludConceptoAuditoria
�C�C8 Z
(
�C�CZ [
)
�C�C[ \
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C 0
"ListaEventosSaludConceptoAuditoria
�C�C A
(
�C�CA B
)
�C�CB C
;
�C�CC D
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C *
ref_eventosSalud_seguimiento
�C�C 0
>
�C�C0 1*
ListaEventosSaludSeguimiento
�C�C2 N
(
�C�CN O
)
�C�CO P
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C *
ListaEventosSaludSeguimiento
�C�C ;
(
�C�C; <
)
�C�C< =
;
�C�C= >
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C .
 ref_eventosSalud_categoriaEvento
�C�C 4
>
�C�C4 5(
ListaEventosSaludCategoria
�C�C6 P
(
�C�CP Q
)
�C�CQ R
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C (
ListaEventosSaludCategoria
�C�C 9
(
�C�C9 :
)
�C�C: ;
;
�C�C; <
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C 1
#ref_eventosSalud_subCategoriaEvento
�C�C 7
>
�C�C7 8+
ListaEventosSaludSubCategoria
�C�C9 V
(
�C�CV W
)
�C�CW X
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C +
ListaEventosSaludSubCategoria
�C�C <
(
�C�C< =
)
�C�C= >
;
�C�C> ?
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C 1
#ref_eventosSalud_subCategoriaEvento
�C�C 7
>
�C�C7 8-
EventosSaludTraerSubCategoriaId
�C�C9 X
(
�C�CX Y
int
�C�CY \
?
�C�C\ ]
idCategoria
�C�C^ i
)
�C�Ci j
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C -
EventosSaludTraerSubCategoriaId
�C�C >
(
�C�C> ?
idCategoria
�C�C? J
)
�C�CJ K
;
�C�CK L
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C 0
"ref_eventosSalud_resultadoNegativo
�C�C 6
>
�C�C6 7B
4ListaEventosSaludResNegativoIdCategoriaClasificacion
�C�C8 l
(
�C�Cl m
int
�C�Cm p
?
�C�Cp q
idCategoria
�C�Cr }
,
�C�C} ~
int�C�C �
?�C�C� �
idClasificacin�C�C� �
)�C�C� �
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C B
4ListaEventosSaludResNegativoIdCategoriaClasificacion
�C�C S
(
�C�CS T
idCategoria
�C�CT _
,
�C�C_ `
idClasificacin
�C�Ca o
)
�C�Co p
;
�C�Cp q
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C 0
"ref_eventosSalud_resultadoNegativo
�C�C 6
>
�C�C6 7*
ListaEventosSaludResNegativo
�C�C8 T
(
�C�CT U
)
�C�CU V
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C *
ListaEventosSaludResNegativo
�C�C ;
(
�C�C; <
)
�C�C< =
;
�C�C= >
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C 2
$ref_eventosSalud_clasificacionEvento
�C�C 8
>
�C�C8 9,
ListaEventosSaludClasificacion
�C�C: X
(
�C�CX Y
)
�C�CY Z
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C ,
ListaEventosSaludClasificacion
�C�C =
(
�C�C= >
)
�C�C> ?
;
�C�C? @
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C 3
%management_eventosSalud_tableroResult
�C�C 9
>
�C�C9 :*
ListadoEventosEnSaludTablero
�C�C; W
(
�C�CW X
int
�C�CX [
?
�C�C[ \
mes
�C�C] `
,
�C�C` a
int
�C�Cb e
?
�C�Ce f
año
�C�Cg j
)
�C�Cj k
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C *
ListadoEventosEnSaludTablero
�C�C ;
(
�C�C; <
mes
�C�C< ?
,
�C�C? @
año
�C�CA D
)
�C�CD E
;
�C�CE F
}
�C�C 	
public
�C�C %
eventos_salud_registros
�C�C &&
TraerDatosEventosSaludId
�C�C' ?
(
�C�C? @
int
�C�C@ C
?
�C�CC D
idEvento
�C�CE M
)
�C�CM N
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C &
TraerDatosEventosSaludId
�C�C 7
(
�C�C7 8
idEvento
�C�C8 @
)
�C�C@ A
;
�C�CA B
}
�C�C 	
public
�C�C 
Ref_ips_cuentas
�C�C 
getprestadoresNit
�C�C 0
(
�C�C0 1
string
�C�C1 7
nit
�C�C8 ;
)
�C�C; <
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C 
getprestadoresNit
�C�C 0
(
�C�C0 1
nit
�C�C1 4
)
�C�C4 5
;
�C�C5 6
}
�C�C 	
public
�C�C 
int
�C�C ,
ActualizarRegistroEventosSalud
�C�C 1
(
�C�C1 2%
eventos_salud_registros
�C�C2 I
even
�C�CJ N
)
�C�CN O
{
�C�C 	
return
�C�C 
DACActualiza
�C�C 
.
�C�C  ,
ActualizarRegistroEventosSalud
�C�C  >
(
�C�C> ?
even
�C�C? C
)
�C�CC D
;
�C�CD E
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C )
cronograma_visita_documento
�C�C /
>
�C�C/ 0-
ObtenerListadoDocumentosVisitas
�C�C1 P
(
�C�CP Q
)
�C�CQ R
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C -
ObtenerListadoDocumentosVisitas
�C�C >
(
�C�C> ?
)
�C�C? @
;
�C�C@ A
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C :
,management_cronograma_visita_traerByteResult
�C�C @
>
�C�C@ A4
&ObtenerListadoDocumentosVisitasSinRuta
�C�CB h
(
�C�Ch i
)
�C�Ci j
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C 4
&ObtenerListadoDocumentosVisitasSinRuta
�C�C E
(
�C�CE F
)
�C�CF G
;
�C�CG H
}
�C�C 	
public
�C�C 
List
�C�C 
<
�C�C 4
&management_encuesta_tipoPreguntaResult
�C�C :
>
�C�C: ;
listaEncuestaSAMI
�C�C< M
(
�C�CM N
)
�C�CN O
{
�C�C 	
return
�C�C 
DACConsulta
�C�C 
.
�C�C 
listaEncuestaSAMI
�C�C 0
(
�C�C0 1
)
�C�C1 2
;
�C�C2 3
}
�C�C 	
public
�C�C 
int
�C�C #
InsertarRespuestaSAMI
�C�C (
(
�C�C( )
encuesta_sami
�C�C) 6
dato
�C�C7 ;
,
�C�C; <
List
�C�C= A
<
�C�CA B&
encuesta_sami_respuestas
�C�CB Z
>
�C�CZ [
detalles
�C�C\ d
,
�C�Cd e
ref
�C�Cf i 
MessageResponseOBJ
�C�Cj |
MsgRes�C�C} �
)�C�C� �
{
�D�D 	
return
�D�D 

DACInserta
�D�D 
.
�D�D #
InsertarRespuestaSAMI
�D�D 3
(
�D�D3 4
dato
�D�D4 8
,
�D�D8 9
detalles
�D�D: B
,
�D�DB C
ref
�D�DD G
MsgRes
�D�DH N
)
�D�DN O
;
�D�DO P
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D 2
$management_encuesta_sami_datosResult
�D�D 8
>
�D�D8 9!
listaRespuestasSAMI
�D�D: M
(
�D�DM N
)
�D�DN O
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D !
listaRespuestasSAMI
�D�D 2
(
�D�D2 3
)
�D�D3 4
;
�D�D4 5
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D :
,management_encuesta_sami_datos_detalleResult
�D�D @
>
�D�D@ A(
listaRespuestasSAMIDetalle
�D�DB \
(
�D�D\ ]
)
�D�D] ^
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D (
listaRespuestasSAMIDetalle
�D�D 9
(
�D�D9 :
)
�D�D: ;
;
�D�D; <
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D <
.management_encuesta_sami_datos_promediosResult
�D�D B
>
�D�DB C*
listaRespuestasSAMIPromedios
�D�DD `
(
�D�D` a
)
�D�Da b
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D *
listaRespuestasSAMIPromedios
�D�D ;
(
�D�D; <
)
�D�D< =
;
�D�D= >
}
�D�D 	
public
�D�D 
encuesta_sami
�D�D "
TraerEncuestaEsteMes
�D�D 1
(
�D�D1 2
)
�D�D2 3
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D "
TraerEncuestaEsteMes
�D�D 3
(
�D�D3 4
)
�D�D4 5
;
�D�D5 6
}
�D�D 	
public
�D�D -
management_fisPrestadoresResult
�D�D .
TraerPrestadorId
�D�D/ ?
(
�D�D? @
int
�D�D@ C
?
�D�DC D
idPrestador
�D�DE P
)
�D�DP Q
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D 
TraerPrestadorId
�D�D /
(
�D�D/ 0
idPrestador
�D�D0 ;
)
�D�D; <
;
�D�D< =
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D 3
%management_fisPrestadores_sedesResult
�D�D 9
>
�D�D9 :#
TraerPrestadorSedesId
�D�D; P
(
�D�DP Q
int
�D�DQ T
?
�D�DT U
idPrestador
�D�DV a
)
�D�Da b
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D #
TraerPrestadorSedesId
�D�D 4
(
�D�D4 5
idPrestador
�D�D5 @
)
�D�D@ A
;
�D�DA B
}
�D�D 	
public
�D�D 
int
�D�D "
InsertarPrestadorFis
�D�D '
(
�D�D' ("
fis_rips_prestadores
�D�D( <
	prestador
�D�D= F
)
�D�DF G
{
�D�D 	
return
�D�D 

DACInserta
�D�D 
.
�D�D "
InsertarPrestadorFis
�D�D 2
(
�D�D2 3
	prestador
�D�D3 <
)
�D�D< =
;
�D�D= >
}
�D�D 	
public
�D�D 
int
�D�D &
InsertarSedePrestadorFis
�D�D +
(
�D�D+ ,(
fis_rips_prestadores_sedes
�D�D, F
sede
�D�DG K
)
�D�DK L
{
�D�D 	
return
�D�D 

DACInserta
�D�D 
.
�D�D &
InsertarSedePrestadorFis
�D�D 6
(
�D�D6 7
sede
�D�D7 ;
)
�D�D; <
;
�D�D< =
}
�D�D 	
public
�D�D 
int
�D�D ,
ActualizarEstadoPrestadoresFIS
�D�D 1
(
�D�D1 2
int
�D�D2 5
?
�D�D5 6
idPrestador
�D�D7 B
)
�D�DB C
{
�D�D 	
return
�D�D 
DACActualiza
�D�D 
.
�D�D  ,
ActualizarEstadoPrestadoresFIS
�D�D  >
(
�D�D> ?
idPrestador
�D�D? J
)
�D�DJ K
;
�D�DK L
}
�D�D 	
public
�D�D 
int
�D�D %
EliminarSedePrestadores
�D�D *
(
�D�D* +
int
�D�D+ .
?
�D�D. /
idSede
�D�D0 6
)
�D�D6 7
{
�D�D 	
return
�D�D 

DACElimina
�D�D 
.
�D�D %
EliminarSedePrestadores
�D�D 5
(
�D�D5 6
idSede
�D�D6 <
)
�D�D< =
;
�D�D= >
}
�D�D 	
public
�D�D 
int
�D�D &
ActualizarDatosPrestador
�D�D +
(
�D�D+ ,"
fis_rips_prestadores
�D�D, @
pre
�D�DA D
)
�D�DD E
{
�D�D 	
return
�D�D 
DACActualiza
�D�D 
.
�D�D  &
ActualizarDatosPrestador
�D�D  8
(
�D�D8 9
pre
�D�D9 <
)
�D�D< =
;
�D�D= >
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D /
!management_regional_usuarioResult
�D�D 5
>
�D�D5 6(
ListadoRegionalesUsuarioId
�D�D7 Q
(
�D�DQ R
int
�D�DR U
?
�D�DU V
	idUsuario
�D�DW `
)
�D�D` a
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D (
ListadoRegionalesUsuarioId
�D�D 9
(
�D�D9 :
	idUsuario
�D�D: C
)
�D�DC D
;
�D�DD E
}
�D�D 	
public
�D�D 
int
�D�D &
GuardarArchivosPrestador
�D�D +
(
�D�D+ ,+
fis_rips_prestadores_archivos
�D�D, I
archivo
�D�DJ Q
)
�D�DQ R
{
�D�D 	
return
�D�D 

DACInserta
�D�D 
.
�D�D &
GuardarArchivosPrestador
�D�D 6
(
�D�D6 7
archivo
�D�D7 >
)
�D�D> ?
;
�D�D? @
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D <
.management_fisPrestadores_tableroControlResult
�D�D B
>
�D�DB C'
TableroControlPrestadores
�D�DD ]
(
�D�D] ^
string
�D�D^ d
nit
�D�De h
,
�D�Dh i
string
�D�Dj p
sap
�D�Dq t
)
�D�Dt u
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D '
TableroControlPrestadores
�D�D 8
(
�D�D8 9
nit
�D�D9 <
,
�D�D< =
sap
�D�D> A
)
�D�DA B
;
�D�DB C
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D F
8management_fisPrestadores_tableroControl_detalladoResult
�D�D L
>
�D�DL M0
"TableroControlPrestadoresDetallado
�D�DN p
(
�D�Dp q
string
�D�Dq w
nit
�D�Dx {
,
�D�D{ |
string�D�D} �
sap�D�D� �
)�D�D� �
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D 0
"TableroControlPrestadoresDetallado
�D�D A
(
�D�DA B
nit
�D�DB E
,
�D�DE F
sap
�D�DG J
)
�D�DJ K
;
�D�DK L
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D E
7management_fisPrestadores_tableroControl_archivosResult
�D�D K
>
�D�DK L&
TraerArchivosPrestadorId
�D�DM e
(
�D�De f
int
�D�Df i
?
�D�Di j
idPrestador
�D�Dk v
)
�D�Dv w
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D &
TraerArchivosPrestadorId
�D�D 7
(
�D�D7 8
idPrestador
�D�D8 C
)
�D�DC D
;
�D�DD E
}
�D�D 	
public
�D�D +
fis_rips_prestadores_archivos
�D�D , 
ArchivoPrestadorId
�D�D- ?
(
�D�D? @
int
�D�D@ C
?
�D�DC D
	idArchivo
�D�DE N
)
�D�DN O
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D  
ArchivoPrestadorId
�D�D 1
(
�D�D1 2
	idArchivo
�D�D2 ;
)
�D�D; <
;
�D�D< =
}
�D�D 	
public
�D�D 
int
�D�D &
EliminarArchivoPrestador
�D�D +
(
�D�D+ ,
int
�D�D, /
?
�D�D/ 0
	idArchivo
�D�D1 :
)
�D�D: ;
{
�D�D 	
return
�D�D 

DACElimina
�D�D 
.
�D�D &
EliminarArchivoPrestador
�D�D 6
(
�D�D6 7
	idArchivo
�D�D7 @
)
�D�D@ A
;
�D�DA B
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D "
fis_rips_prestadores
�D�D (
>
�D�D( )$
ConsultaPrestadoresFis
�D�D* @
(
�D�D@ A
decimal
�D�DA H
?
�D�DH I
nit
�D�DJ M
,
�D�DM N
ref
�D�DO R 
MessageResponseOBJ
�D�DS e
MsgRes
�D�Df l
)
�D�Dl m
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D $
ConsultaPrestadoresFis
�D�D 5
(
�D�D5 6
nit
�D�D6 9
,
�D�D9 :
ref
�D�D; >
MsgRes
�D�D? E
)
�D�DE F
;
�D�DF G
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D ,
fis_rips_prestadores_contratos
�D�D 2
>
�D�D2 3,
ConsultaContratoPrestadoresFis
�D�D4 R
(
�D�DR S
string
�D�DS Y
numContrato
�D�DZ e
,
�D�De f
ref
�D�Dg j 
MessageResponseOBJ
�D�Dk }
MsgRes�D�D~ �
)�D�D� �
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D ,
ConsultaContratoPrestadoresFis
�D�D =
(
�D�D= >
numContrato
�D�D> I
,
�D�DI J
ref
�D�DK N
MsgRes
�D�DO U
)
�D�DU V
;
�D�DV W
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D "
fis_rips_prestadores
�D�D (
>
�D�D( )'
ConsultaPrestadoresFisSAP
�D�D* C
(
�D�DC D
decimal
�D�DD K
sap
�D�DL O
,
�D�DO P
ref
�D�DQ T 
MessageResponseOBJ
�D�DU g
MsgRes
�D�Dh n
)
�D�Dn o
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D '
ConsultaPrestadoresFisSAP
�D�D 8
(
�D�D8 9
sap
�D�D9 <
,
�D�D< =
ref
�D�D> A
MsgRes
�D�DB H
)
�D�DH I
;
�D�DI J
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D 
fis_rips_tigas
�D�D "
>
�D�D" #

TraerTigas
�D�D$ .
(
�D�D. /
)
�D�D/ 0
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D 

TraerTigas
�D�D )
(
�D�D) *
)
�D�D* +
;
�D�D+ ,
}
�D�D 	
public
�D�D 7
)management_fisPrestadores_contratosResult
�D�D 8 
TraerDatosContrato
�D�D9 K
(
�D�DK L
int
�D�DL O
?
�D�DO P

idCOntrato
�D�DQ [
)
�D�D[ \
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D  
TraerDatosContrato
�D�D 1
(
�D�D1 2

idCOntrato
�D�D2 <
)
�D�D< =
;
�D�D= >
}
�D�D 	
public
�D�D 
List
�D�D 
<
�D�D =
/management_fisPrestadores_contratos_tigasResult
�D�D C
>
�D�DC D%
TraerDatosContratoTigas
�D�DE \
(
�D�D\ ]
int
�D�D] `
?
�D�D` a

idCOntrato
�D�Db l
)
�D�Dl m
{
�D�D 	
return
�D�D 
DACConsulta
�D�D 
.
�D�D %
TraerDatosContratoTigas
�D�D 6
(
�D�D6 7

idCOntrato
�D�D7 A
)
�D�DA B
;
�D�DB C
}
�D�D 	
public
�E�E 
int
�E�E &
GuardarContratoPrestador
�E�E +
(
�E�E+ ,,
fis_rips_prestadores_contratos
�E�E, J
contrato
�E�EK S
)
�E�ES T
{
�E�E 	
return
�E�E 

DACInserta
�E�E 
.
�E�E &
GuardarContratoPrestador
�E�E 6
(
�E�E6 7
contrato
�E�E7 ?
)
�E�E? @
;
�E�E@ A
}
�E�E 	
public
�E�E 
int
�E�E .
 ActualizarDatosContratoPrestador
�E�E 3
(
�E�E3 4,
fis_rips_prestadores_contratos
�E�E4 R
contra
�E�ES Y
)
�E�EY Z
{
�E�E 	
return
�E�E 
DACActualiza
�E�E 
.
�E�E  .
 ActualizarDatosContratoPrestador
�E�E  @
(
�E�E@ A
contra
�E�EA G
)
�E�EG H
;
�E�EH I
}
�E�E 	
public
�E�E 
int
�E�E *
GuardarTigaContratoPrestador
�E�E /
(
�E�E/ 01
#fis_rips_prestadores_contrato_tigas
�E�E0 S
tiga
�E�ET X
)
�E�EX Y
{
�E�E 	
return
�E�E 

DACInserta
�E�E 
.
�E�E *
GuardarTigaContratoPrestador
�E�E :
(
�E�E: ;
tiga
�E�E; ?
)
�E�E? @
;
�E�E@ A
}
�E�E 	
public
�E�E 
int
�E�E .
 EliminarTigaContratosPrestadores
�E�E 3
(
�E�E3 4
int
�E�E4 7
?
�E�E7 8
idTiga
�E�E9 ?
)
�E�E? @
{
�E�E 	
return
�E�E 

DACElimina
�E�E 
.
�E�E .
 EliminarTigaContratosPrestadores
�E�E >
(
�E�E> ?
idTiga
�E�E? E
)
�E�EE F
;
�E�EF G
}
�E�E 	
public
�E�E 
int
�E�E +
ActualizarEstadoTigasContrato
�E�E 0
(
�E�E0 1
int
�E�E1 4
?
�E�E4 5

idContrato
�E�E6 @
)
�E�E@ A
{
�E�E 	
return
�E�E 
DACActualiza
�E�E 
.
�E�E  +
ActualizarEstadoTigasContrato
�E�E  =
(
�E�E= >

idContrato
�E�E> H
)
�E�EH I
;
�E�EI J
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E 
fis_rips_regional
�E�E %
>
�E�E% & 
TraerregionalesFis
�E�E' 9
(
�E�E9 :
)
�E�E: ;
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E  
TraerregionalesFis
�E�E 1
(
�E�E1 2
)
�E�E2 3
;
�E�E3 4
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E 8
*management_fis_departamento_regionalResult
�E�E >
>
�E�E> ?#
TraerDepartamentosFis
�E�E@ U
(
�E�EU V
int
�E�EV Y
?
�E�EY Z

idRegional
�E�E[ e
)
�E�Ee f
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E #
TraerDepartamentosFis
�E�E 4
(
�E�E4 5

idRegional
�E�E5 ?
)
�E�E? @
;
�E�E@ A
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E 
fis_rips_ciudad
�E�E #
>
�E�E# $
TraerCiudadesFis
�E�E% 5
(
�E�E5 6
int
�E�E6 9
?
�E�E9 :
idDepartamento
�E�E; I
)
�E�EI J
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E 
TraerCiudadesFis
�E�E /
(
�E�E/ 0
idDepartamento
�E�E0 >
)
�E�E> ?
;
�E�E? @
}
�E�E 	
public
�E�E 
void
�E�E #
insertarCargueTarifas
�E�E )
(
�E�E) *4
&fis_rips_prestadores_contratos_tarifas
�E�E* P
obj
�E�EQ T
,
�E�ET U
List
�E�EV Z
<
�E�EZ [?
0fis_rips_prestadores_contratos_tarifas_registros�E�E[ �
>�E�E� �
lista�E�E� �
,�E�E� �
ref�E�E� �"
MessageResponseOBJ�E�E� �
MsgRes�E�E� �
)�E�E� �
{
�E�E 	

DACInserta
�E�E 
.
�E�E #
insertarCargueTarifas
�E�E ,
(
�E�E, -
obj
�E�E- 0
,
�E�E0 1
lista
�E�E2 7
,
�E�E7 8
ref
�E�E9 <
MsgRes
�E�E= C
)
�E�EC D
;
�E�ED E
}
�E�E 	
public
�E�E 
fis_rips_cups
�E�E 
TraerCupsCodigo
�E�E ,
(
�E�E, -
string
�E�E- 3
codigo
�E�E4 :
)
�E�E: ;
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E 
TraerCupsCodigo
�E�E .
(
�E�E. /
codigo
�E�E/ 5
)
�E�E5 6
;
�E�E6 7
}
�E�E 	
public
�E�E 
int
�E�E %
ActualizarEstadoTarifas
�E�E *
(
�E�E* +
int
�E�E+ .
?
�E�E. /

idContrato
�E�E0 :
)
�E�E: ;
{
�E�E 	
return
�E�E 
DACActualiza
�E�E 
.
�E�E  %
ActualizarEstadoTarifas
�E�E  7
(
�E�E7 8

idContrato
�E�E8 B
)
�E�EB C
;
�E�EC D
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E F
8management_fisPrestadores_contratos_tableroControlResult
�E�E L
>
�E�EL M*
DatosTableroControlContratos
�E�EN j
(
�E�Ej k
)
�E�Ek l
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E *
DatosTableroControlContratos
�E�E ;
(
�E�E; <
)
�E�E< =
;
�E�E= >
}
�E�E 	
public
�E�E 
int
�E�E 
	CrearCups
�E�E 
(
�E�E 
fis_rips_cups
�E�E *
obj
�E�E+ .
)
�E�E. /
{
�E�E 	
return
�E�E 

DACInserta
�E�E 
.
�E�E 
	CrearCups
�E�E '
(
�E�E' (
obj
�E�E( +
)
�E�E+ ,
;
�E�E, -
}
�E�E 	
public
�E�E 
int
�E�E 
ActualizarCupsFis
�E�E $
(
�E�E$ %
fis_rips_cups
�E�E% 2
cups
�E�E3 7
)
�E�E7 8
{
�E�E 	
return
�E�E 
DACActualiza
�E�E 
.
�E�E  
ActualizarCupsFis
�E�E  1
(
�E�E1 2
cups
�E�E2 6
)
�E�E6 7
;
�E�E7 8
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E '
management_fis_cupsResult
�E�E -
>
�E�E- .
TraerCUpsTablero
�E�E/ ?
(
�E�E? @
)
�E�E@ A
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E 
TraerCUpsTablero
�E�E /
(
�E�E/ 0
)
�E�E0 1
;
�E�E1 2
}
�E�E 	
public
�E�E 
fis_rips_cups
�E�E 
TraerCupsId
�E�E (
(
�E�E( )
int
�E�E) ,
?
�E�E, -
idCups
�E�E. 4
)
�E�E4 5
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E 
TraerCupsId
�E�E *
(
�E�E* +
idCups
�E�E+ 1
)
�E�E1 2
;
�E�E2 3
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E 3
%management_fis_refTipoConsultasResult
�E�E 9
>
�E�E9 :%
ListadoTipoConsultaJson
�E�E; R
(
�E�ER S
)
�E�ES T
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E %
ListadoTipoConsultaJson
�E�E 6
(
�E�E6 7
)
�E�E7 8
;
�E�E8 9
}
�E�E 	
public
�E�E $
chatbot_ref_respuestas
�E�E %
TraerRespuestaId
�E�E& 6
(
�E�E6 7
int
�E�E7 :
?
�E�E: ;
idRespuesta
�E�E< G
)
�E�EG H
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E 
TraerRespuestaId
�E�E /
(
�E�E/ 0
idRespuesta
�E�E0 ;
)
�E�E; <
;
�E�E< =
}
�E�E 	
public
�E�E #
chatbot_ref_preguntas
�E�E $
TraerPreguntaId
�E�E% 4
(
�E�E4 5
int
�E�E5 8
?
�E�E8 9

idPregunta
�E�E: D
)
�E�ED E
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E 
TraerPreguntaId
�E�E .
(
�E�E. /

idPregunta
�E�E/ 9
)
�E�E9 :
;
�E�E: ;
}
�E�E 	
public
�E�E %
chatbot_ref_subprocesos
�E�E &
TraerSubProcesoId
�E�E' 8
(
�E�E8 9
int
�E�E9 <
?
�E�E< =
idSub
�E�E> C
)
�E�EC D
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E 
TraerSubProcesoId
�E�E 0
(
�E�E0 1
idSub
�E�E1 6
)
�E�E6 7
;
�E�E7 8
}
�E�E 	
public
�E�E "
chatbot_ref_procesos
�E�E #
TraerProcesoId
�E�E$ 2
(
�E�E2 3
int
�E�E3 6
?
�E�E6 7
	idProceso
�E�E8 A
)
�E�EA B
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E 
TraerProcesoId
�E�E -
(
�E�E- .
	idProceso
�E�E. 7
)
�E�E7 8
;
�E�E8 9
}
�E�E 	
public
�E�E #
chatbot_ref_proyectos
�E�E $
TraerProyectoId
�E�E% 4
(
�E�E4 5
int
�E�E5 8
?
�E�E8 9

idProyecto
�E�E: D
)
�E�ED E
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E 
TraerProyectoId
�E�E .
(
�E�E. /

idProyecto
�E�E/ 9
)
�E�E9 :
;
�E�E: ;
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E +
fis_rips_cargue_LoteConsultas
�E�E 1
>
�E�E1 2
ConsultaCUVFIS
�E�E3 A
(
�E�EA B
string
�E�EB H
codCuv
�E�EI O
,
�E�EO P
ref
�E�EQ T 
MessageResponseOBJ
�E�EU g
MsgRes
�E�Eh n
)
�E�En o
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E 
ConsultaCUVFIS
�E�E -
(
�E�E- .
codCuv
�E�E. 4
,
�E�E4 5
ref
�E�E6 9
MsgRes
�E�E: @
)
�E�E@ A
;
�E�EA B
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E 6
(management_fis_cargueRips_consultaResult
�E�E <
>
�E�E< =!
ListadoRipsConsulta
�E�E> Q
(
�E�EQ R
string
�E�ER X
codCuv
�E�EY _
)
�E�E_ `
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E !
ListadoRipsConsulta
�E�E 2
(
�E�E2 3
codCuv
�E�E3 9
)
�E�E9 :
;
�E�E: ;
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E =
/management_fis_cargueRips_hospitalizacionResult
�E�E C
>
�E�EC D(
ListadoRipsHospitalizacion
�E�EE _
(
�E�E_ `
string
�E�E` f
codCuv
�E�Eg m
)
�E�Em n
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E (
ListadoRipsHospitalizacion
�E�E 9
(
�E�E9 :
codCuv
�E�E: @
)
�E�E@ A
;
�E�EA B
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E :
,management_fis_cargueRips_medicamentosResult
�E�E @
>
�E�E@ A%
ListadoRipsMedicamentos
�E�EB Y
(
�E�EY Z
string
�E�EZ `
codCuv
�E�Ea g
)
�E�Eg h
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E %
ListadoRipsMedicamentos
�E�E 6
(
�E�E6 7
codCuv
�E�E7 =
)
�E�E= >
;
�E�E> ?
}
�E�E 	
public
�E�E 
List
�E�E 
<
�E�E <
.management_fis_cargueRips_otrosServiciosResult
�E�E B
>
�E�EB C'
ListadoRipsOtrosServicios
�E�ED ]
(
�E�E] ^
string
�E�E^ d
codCuv
�E�Ee k
)
�E�Ek l
{
�E�E 	
return
�E�E 
DACConsulta
�E�E 
.
�E�E '
ListadoRipsOtrosServicios
�E�E 8
(
�E�E8 9
codCuv
�E�E9 ?
)
�E�E? @
;
�E�E@ A
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F <
.management_fis_cargueRips_procedimientosResult
�F�F B
>
�F�FB C'
ListadoRipsProcedimientos
�F�FD ]
(
�F�F] ^
string
�F�F^ d
codCuv
�F�Fe k
)
�F�Fk l
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F '
ListadoRipsProcedimientos
�F�F 8
(
�F�F8 9
codCuv
�F�F9 ?
)
�F�F? @
;
�F�F@ A
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F :
,management_fis_cargueRips_recienNacidoResult
�F�F @
>
�F�F@ A%
ListadoRipsRecienNacido
�F�FB Y
(
�F�FY Z
string
�F�FZ `
codCuv
�F�Fa g
)
�F�Fg h
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F %
ListadoRipsRecienNacido
�F�F 6
(
�F�F6 7
codCuv
�F�F7 =
)
�F�F= >
;
�F�F> ?
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F 9
+management_fis_cargueRips_transaccionResult
�F�F ?
>
�F�F? @$
ListadoRipsTransaccion
�F�FA W
(
�F�FW X
string
�F�FX ^
codCuv
�F�F_ e
)
�F�Fe f
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F $
ListadoRipsTransaccion
�F�F 5
(
�F�F5 6
codCuv
�F�F6 <
)
�F�F< =
;
�F�F= >
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F 7
)management_fis_cargueRips_urgenciasResult
�F�F =
>
�F�F= >"
ListadoRipsUrgencias
�F�F? S
(
�F�FS T
string
�F�FT Z
codCuv
�F�F[ a
)
�F�Fa b
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F "
ListadoRipsUrgencias
�F�F 3
(
�F�F3 4
codCuv
�F�F4 :
)
�F�F: ;
;
�F�F; <
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F 6
(management_fis_cargueRips_usuariosResult
�F�F <
>
�F�F< =!
ListadoRipsUsuarios
�F�F> Q
(
�F�FQ R
string
�F�FR X
codCuv
�F�FY _
)
�F�F_ `
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F !
ListadoRipsUsuarios
�F�F 2
(
�F�F2 3
codCuv
�F�F3 9
)
�F�F9 :
;
�F�F: ;
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F 4
&Management_chatbot_ref_proyectosResult
�F�F :
>
�F�F: ;
ChatBotProyectos
�F�F< L
(
�F�FL M
)
�F�FM N
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F 
ChatBotProyectos
�F�F /
(
�F�F/ 0
)
�F�F0 1
;
�F�F1 2
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F 3
%Management_chatbot_ref_procesosResult
�F�F 9
>
�F�F9 :
ChatBotProcesos
�F�F; J
(
�F�FJ K
int
�F�FK N
?
�F�FN O

idProyecto
�F�FP Z
)
�F�FZ [
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F 
ChatBotProcesos
�F�F .
(
�F�F. /

idProyecto
�F�F/ 9
)
�F�F9 :
;
�F�F: ;
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F 6
(Management_chatbot_ref_subprocesosResult
�F�F <
>
�F�F< = 
ChatBotSubProcesos
�F�F> P
(
�F�FP Q
int
�F�FQ T
?
�F�FT U
	idProceso
�F�FV _
)
�F�F_ `
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F  
ChatBotSubProcesos
�F�F 1
(
�F�F1 2
	idProceso
�F�F2 ;
)
�F�F; <
;
�F�F< =
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F 4
&Management_chatbot_ref_preguntasResult
�F�F :
>
�F�F: ;
ChatBotPreguntas
�F�F< L
(
�F�FL M
int
�F�FM P
?
�F�FP Q
idSubProceso
�F�FR ^
)
�F�F^ _
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F 
ChatBotPreguntas
�F�F /
(
�F�F/ 0
idSubProceso
�F�F0 <
)
�F�F< =
;
�F�F= >
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F 5
'Management_chatbot_ref_respuestasResult
�F�F ;
>
�F�F; <
ChatBotRespuestas
�F�F= N
(
�F�FN O
int
�F�FO R
?
�F�FR S

idPregunta
�F�FT ^
)
�F�F^ _
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F 
ChatBotRespuestas
�F�F 0
(
�F�F0 1

idPregunta
�F�F1 ;
)
�F�F; <
;
�F�F< =
}
�F�F 	
public
�F�F 
int
�F�F $
ChatBotInsertarProceso
�F�F )
(
�F�F) *"
chatbot_ref_procesos
�F�F* >
obj
�F�F? B
)
�F�FB C
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F $
ChatBotInsertarProceso
�F�F 4
(
�F�F4 5
obj
�F�F5 8
)
�F�F8 9
;
�F�F9 :
}
�F�F 	
public
�F�F 
int
�F�F '
ChatBotInsertarSubProceso
�F�F ,
(
�F�F, -%
chatbot_ref_subprocesos
�F�F- D
obj
�F�FE H
)
�F�FH I
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F '
ChatBotInsertarSubProceso
�F�F 7
(
�F�F7 8
obj
�F�F8 ;
)
�F�F; <
;
�F�F< =
}
�F�F 	
public
�F�F 
int
�F�F &
ChatBotInsertarPreguntas
�F�F +
(
�F�F+ ,#
chatbot_ref_preguntas
�F�F, A
obj
�F�FB E
)
�F�FE F
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F &
ChatBotInsertarPreguntas
�F�F 6
(
�F�F6 7
obj
�F�F7 :
)
�F�F: ;
;
�F�F; <
}
�F�F 	
public
�F�F 
int
�F�F '
ChatBotInsertarRespuestas
�F�F ,
(
�F�F, -$
chatbot_ref_respuestas
�F�F- C
obj
�F�FD G
)
�F�FG H
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F '
ChatBotInsertarRespuestas
�F�F 7
(
�F�F7 8
obj
�F�F8 ;
)
�F�F; <
;
�F�F< =
}
�F�F 	
public
�F�F 
int
�F�F /
!ChatBotInsertarRespuestasArchivos
�F�F 4
(
�F�F4 5-
chatbot_ref_respuestas_archivos
�F�F5 T
obj
�F�FU X
)
�F�FX Y
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F /
!ChatBotInsertarRespuestasArchivos
�F�F ?
(
�F�F? @
obj
�F�F@ C
)
�F�FC D
;
�F�FD E
}
�F�F 	
public
�F�F 
List
�F�F 
<
�F�F >
0Management_chatbot_ref_respuestas_archivosResult
�F�F D
>
�F�FD E'
ChatBotRespuestasArchivos
�F�FF _
(
�F�F_ `
int
�F�F` c
?
�F�Fc d
idRespuesta
�F�Fe p
)
�F�Fp q
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F '
ChatBotRespuestasArchivos
�F�F 8
(
�F�F8 9
idRespuesta
�F�F9 D
)
�F�FD E
;
�F�FE F
}
�F�F 	
public
�F�F -
chatbot_ref_respuestas_archivos
�F�F .!
TraerArchivoChatBot
�F�F/ B
(
�F�FB C
int
�F�FC F
?
�F�FF G
	idArchivo
�F�FH Q
)
�F�FQ R
{
�F�F 	
return
�F�F 
DACConsulta
�F�F 
.
�F�F !
TraerArchivoChatBot
�F�F 2
(
�F�F2 3
	idArchivo
�F�F3 <
)
�F�F< =
;
�F�F= >
}
�F�F 	
public
�F�F 
int
�F�F (
GuardarCargueJsonConsultas
�F�F -
(
�F�F- .+
fis_rips_cargue_LoteConsultas
�F�F. K
lote
�F�FL P
,
�F�FP Q
List
�F�FR V
<
�F�FV W&
fis_rips_cargue_consulta
�F�FW o
>
�F�Fo p
lista
�F�Fq v
)
�F�Fv w
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F (
GuardarCargueJsonConsultas
�F�F 8
(
�F�F8 9
lote
�F�F9 =
,
�F�F= >
lista
�F�F? D
)
�F�FD E
;
�F�FE F
}
�F�F 	
public
�F�F 
int
�F�F .
 GuardarCargueJsonHospitalizacion
�F�F 3
(
�F�F3 4+
fis_rips_cargue_LoteConsultas
�F�F4 Q
lote
�F�FR V
,
�F�FV W
List
�F�FX \
<
�F�F\ ]-
fis_rips_cargue_hospitalizacion
�F�F] |
>
�F�F| }
lista�F�F~ �
)�F�F� �
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F .
 GuardarCargueJsonHospitalizacion
�F�F >
(
�F�F> ?
lote
�F�F? C
,
�F�FC D
lista
�F�FE J
)
�F�FJ K
;
�F�FK L
}
�F�F 	
public
�F�F 
int
�F�F +
GuardarCargueJsonMedicamentos
�F�F 0
(
�F�F0 1+
fis_rips_cargue_LoteConsultas
�F�F1 N
lote
�F�FO S
,
�F�FS T
List
�F�FU Y
<
�F�FY Z*
fis_rips_cargue_medicamentos
�F�FZ v
>
�F�Fv w
lista
�F�Fx }
)
�F�F} ~
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F +
GuardarCargueJsonMedicamentos
�F�F ;
(
�F�F; <
lote
�F�F< @
,
�F�F@ A
lista
�F�FB G
)
�F�FG H
;
�F�FH I
}
�F�F 	
public
�F�F 
int
�F�F -
GuardarCargueJsonotrosServicios
�F�F 2
(
�F�F2 3+
fis_rips_cargue_LoteConsultas
�F�F3 P
lote
�F�FQ U
,
�F�FU V
List
�F�FW [
<
�F�F[ \-
fis_rips_cargue_otros_servicios
�F�F\ {
>
�F�F{ |
lista�F�F} �
)�F�F� �
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F -
GuardarCargueJsonotrosServicios
�F�F =
(
�F�F= >
lote
�F�F> B
,
�F�FB C
lista
�F�FD I
)
�F�FI J
;
�F�FJ K
}
�F�F 	
public
�F�F 
int
�F�F -
GuardarCargueJsonProcedimientos
�F�F 2
(
�F�F2 3+
fis_rips_cargue_LoteConsultas
�F�F3 P
lote
�F�FQ U
,
�F�FU V
List
�F�FW [
<
�F�F[ \,
fis_rips_cargue_procedimientos
�F�F\ z
>
�F�Fz {
lista�F�F| �
)�F�F� �
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F -
GuardarCargueJsonProcedimientos
�F�F =
(
�F�F= >
lote
�F�F> B
,
�F�FB C
lista
�F�FD I
)
�F�FI J
;
�F�FJ K
}
�F�F 	
public
�F�F 
int
�F�F +
GuardarCargueJsonRecienNacido
�F�F 0
(
�F�F0 1+
fis_rips_cargue_LoteConsultas
�F�F1 N
lote
�F�FO S
,
�F�FS T
List
�F�FU Y
<
�F�FY Z*
fis_rips_cargue_reciennacido
�F�FZ v
>
�F�Fv w
lista
�F�Fx }
)
�F�F} ~
{
�F�F 	
return
�F�F 

DACInserta
�F�F 
.
�F�F +
GuardarCargueJsonRecienNacido
�F�F ;
(
�F�F; <
lote
�F�F< @
,
�F�F@ A
lista
�F�FB G
)
�F�FG H
;
�F�FH I
}
�F�F 	
public
�F�F 
int
�F�F *
GuardarCargueJsonTransaccion
�F�F /
(
�F�F/ 0+
fis_rips_cargue_LoteConsultas
�F�F0 M
lote
�F�FN R
,
�F�FR S
List
�F�FT X
<
�F�FX Y)
fis_rips_cargue_transaccion
�F�FY t
>
�F�Ft u
lista
�F�Fv {
)
�F�F{ |
{
�F�F 	
return
�G�G 

DACInserta
�G�G 
.
�G�G *
GuardarCargueJsonTransaccion
�G�G :
(
�G�G: ;
lote
�G�G; ?
,
�G�G? @
lista
�G�GA F
)
�G�GF G
;
�G�GG H
}
�G�G 	
public
�G�G 
int
�G�G (
GuardarCargueJsonUrgencias
�G�G -
(
�G�G- .+
fis_rips_cargue_LoteConsultas
�G�G. K
lote
�G�GL P
,
�G�GP Q
List
�G�GR V
<
�G�GV W'
fis_rips_cargue_urgencias
�G�GW p
>
�G�Gp q
lista
�G�Gr w
)
�G�Gw x
{
�G�G 	
return
�G�G 

DACInserta
�G�G 
.
�G�G (
GuardarCargueJsonUrgencias
�G�G 8
(
�G�G8 9
lote
�G�G9 =
,
�G�G= >
lista
�G�G? D
)
�G�GD E
;
�G�GE F
}
�G�G 	
public
�G�G 
int
�G�G '
GuardarCargueJsonUsuarios
�G�G ,
(
�G�G, -+
fis_rips_cargue_LoteConsultas
�G�G- J
lote
�G�GK O
,
�G�GO P
List
�G�GQ U
<
�G�GU V&
fis_rips_cargue_usuarios
�G�GV n
>
�G�Gn o
lista
�G�Gp u
)
�G�Gu v
{
�G�G 	
return
�G�G 

DACInserta
�G�G 
.
�G�G '
GuardarCargueJsonUsuarios
�G�G 7
(
�G�G7 8
lote
�G�G8 <
,
�G�G< =
lista
�G�G> C
)
�G�GC D
;
�G�GD E
}
�G�G 	
}
�G�G 
}�G�G 