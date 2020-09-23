pipeline {
	agent any
	
	stages {
		stage('Unit tests') {
			steps {
				catchError(buildResult: 'UNSTABLE', stageResult: 'UNSTABLE') {
					sh '''
						rm -f TestResults*.xml
						install-unity run ${WORKSPACE} -- -batchmode -runTests -testPlatform playmode -testCategory UnitTest
					'''
				}
				nunit testResultsPattern: 'TestResults*.xml'
			}
		}
		stage('Android build') {
			steps {
				withCredentials([file(credentialsId: 'VIKKEKEYSTORE', variable: 'VIKKEKS'), string(credentialsId: 'VIKKEKSPW', variable: 'VIKKEKSPW'), string(credentialsId: 'VIKKEKEY', variable: 'VIKKEKEY')]) {
				    sh 'install-unity run ${WORKSPACE} -- -batchmode -executeMethod JenkinsBuild.BuildAndroid ${WORKSPACE} --keystore ${VIKKEKS} --storepass ${VIKKEKSPW} --keypass ${VIKKEKEY} --keyalias vikkeupload -quit -logFile ${WORKSPACE}/BuildLog_Android_${BUILD_ID}.xml'
				}
				dir('Android') {
					archiveArtifacts 'VIKKE.aab'
				}
			}
		}
		stage('Generate Xcode project') {
			steps {
				sh 'install-unity run ${WORKSPACE} -- -batchmode -executeMethod JenkinsBuild.BuildiOS ${WORKSPACE} -quit -logFile ${WORKSPACE}/BuildLog_iOS_${BUILD_ID}.xml'
			}
		}
		stage('Build iOS app') {
			steps {
				sh '''
					cd ${WORKSPACE}/iOS
					xcodebuild -project Unity-iPhone.xcodeproj -scheme Unity-iPhone -sdk iphoneos -configuration Release clean -allowProvisioningUpdates
					xcodebuild -project Unity-iPhone.xcodeproj -scheme Unity-iPhone -sdk iphoneos -archivePath ${JOB_NAME}.xcarchive -configuration Release archive -allowProvisioningUpdates
				'''
				withCredentials([file(credentialsId: 'EXPORTPLIST', variable: 'EXPORTPLIST')]) {
					sh 'xcodebuild -exportArchive -archivePath ${WORKSPACE}/iOS/${JOB_NAME}.xcarchive -exportOptionsPlist ${EXPORTPLIST} -exportPath ${WORKSPACE}/iOS/ -allowProvisioningUpdates'
				}
				dir('iOS') {
					archiveArtifacts 'Unity-iPhone.ipa'
				}
			}
		}
		stage('Upload iOS app') {
			steps {
				withCredentials([string(credentialsId: 'ALTOOLPW', variable: 'ALTOOLPW')]) {
					sh 'xcrun altool --upload-app -f ${WORKSPACE}/iOS/Unity-iPhone.ipa -u ${APPLEID} -p ${ALTOOLPW}'
				}
			}
		}
	}
}
